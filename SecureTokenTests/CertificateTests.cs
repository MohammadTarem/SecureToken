using System;
using Xunit;
using SecureToken;
using System.Linq;
using SecureTokenTests.Helpers;

namespace SecureTokenTests
{
    public class CertificateTests
    {
        
        [Fact]
        public void Must_Serialize_And_Deserialize_Identifier()
        {


            Certificate certificate = Generator.GetNewCertificate();

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var result = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.Equal(certificate.Identifier, result.Identifier);
            
        }

        [Fact]
        public void Must_Serialize_And_Deserialize_Issuer()
        {

            Certificate certificate = Generator.GetNewCertificate();

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var result = Token.CreateCertificate(token, Generator.GetTokenOptions());
            
            Assert.Equal(certificate.Issuer, result.Issuer);
        }

        [Fact]
        public void Must_Serialize_And_Deserialize_Claims()
        {

            Certificate certificate = Generator.GetNewCertificate();

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var result = Token.CreateCertificate(token, Generator.GetTokenOptions());




            Assert.True
            (
                certificate.Claims.SequenceEqual(result.Claims)

            );

        }

        [Fact]
        public void Must_Serialize_And_Deserialize_Date()
        {


            Certificate certificate = Generator.GetNewCertificate();

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var result = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.Equal(certificate.ValidFrom, result.ValidFrom);
            Assert.Equal(certificate.ExpiresAt, result.ExpiresAt);

        }

        [Fact]
        public void Encrypting_One_Certificate_Twice_Must_Return_Distinct_Tokens()
        {
            Certificate certificate = Generator.GetNewCertificate();

            var token1 = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var token2 = Token.CreateToken(certificate, Generator.GetTokenOptions());

            Assert.NotEqual(token1, token2);

        }

        [Fact]
        public void Expired_Date_Must_Result_In_Invalid_Token()
        {
            Certificate certificate = Generator.GetNewCertificateWithDate
            (
                DateTime.Now.AddSeconds(-10),
                DateTime.Now.AddSeconds(-5)
            );

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var result = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.False(result.IsValid);

        }


    }
}
