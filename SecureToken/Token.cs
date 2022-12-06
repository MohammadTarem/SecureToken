using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureToken
{
    public static class Token
    {
        public static string CreateToken(Certificate certificate, SecureTokenOptions options)
        {

            var jsonString = SerializeCertificate(certificate);
            var plainBytes = Encoding.UTF8.GetBytes(jsonString);
            var cipherBytes = options.Encryptor.Encrypt(plainBytes);
            var signature = options.Signer.Hash(plainBytes);

            return $"{Base64UrlEncoder.Encode(cipherBytes)}.{Base64UrlEncoder.Encode(signature)}";
        }

        public static Certificate CreateCertificate(string base64UrlEncoded, SecureTokenOptions options)
        {
            var token = base64UrlEncoded.Split(".");
            if (token.Length != 2)
            {
                return null;
            }

            try
            {
                var cipherBytes = Base64UrlEncoder.DecodeBytes(token[0]);
                var signatureBytes = Base64UrlEncoder.DecodeBytes(token[1]);
                var plainBytes = options.Encryptor.Decrypt(cipherBytes);
                var expectedSignatureBytes = options.Signer.Hash(plainBytes);

                if (!expectedSignatureBytes.SequenceEqual(signatureBytes))
                {
                    return null;
                }

                return DeserializeCertificate(Encoding.UTF8.GetString(plainBytes));
            }
            catch
            {
                return null;

            }

        }

        private static string SerializeCertificate(Certificate certificate)
        {
            dynamic jObject = new JObject();
            jObject.Salt = Convert.ToBase64String(SecureRandomBytes.Generate(new Random().Next(10, 30)));
            jObject.Issuer = certificate.Issuer ?? "";
            jObject.Identifier = certificate.Identifier ?? "";
            jObject.Claims = JsonConvert.SerializeObject(certificate.Claims);
            jObject.ValidFrom = certificate.ValidFrom;
            jObject.ExpiresAt = certificate.ExpiresAt;
            return JsonConvert.SerializeObject(jObject);

        }

        private static Certificate DeserializeCertificate(string strCert)
        {
            try
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(strCert);
                var issuer = (string)jObject["Issuer"];
                var identifier = (string)jObject["Identifier"];
                var claims = (string)jObject["Claims"];
                var validFrom = (DateTime)jObject["ValidFrom"];
                var expiresAt = (DateTime)jObject["ExpiresAt"];

                if (issuer == null || identifier == null || claims == null)
                {
                    return null;
                }

                return new Certificate
                (
                    identifier, issuer,
                    JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(claims),
                    validFrom, expiresAt
                );

            }
            catch
            {
                return null;
            }

        }

    }
}
