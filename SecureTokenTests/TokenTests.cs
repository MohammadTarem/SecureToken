using System;
using System.Linq;
using Xunit;
using SecureToken;
using SecureTokenTests.Helpers;

namespace SecureTokenTests
{
    public class TokenTests
    {

        [Fact]
        public void Token_Must_Have_Two_Parts_Seperated_With_Dot()
        {
            Certificate certificate = Generator.GetNewCertificate();

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());


            Assert.Equal(2, token.Split(".").Length);

        }

        [Fact]
        public void Tampering_Cipher_Must_Return_Null_Certificate()
        {

            Certificate certificate = Generator.GetNewCertificate();
            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var cipher = token.Split(".").First();
            var signature = token.Split(".").Last();

            var tamperdCipher1 = cipher.Insert(0, "A");
            var tamperdCipher2 =  new String(cipher.SkipLast(1).ToArray());

            var tamperdToken1 = $"{tamperdCipher1}.{signature}";
            var tamperdToken2 = $"{tamperdCipher2}.{signature}";

            var cert1 = Token.CreateCertificate(tamperdToken1, Generator.GetTokenOptions());
            var cert2 = Token.CreateCertificate(tamperdToken2, Generator.GetTokenOptions());


            Assert.Null(cert1);
            Assert.Null(cert2);

        }

        [Fact]
        public void Tampering_Signature_Must_Return_Null_Certificate()
        {

            Certificate certificate = Generator.GetNewCertificate();
            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var cipher = token.Split(".").First();
            var signature = token.Split(".").Last();

            var tamperdSignature1 = signature.Insert(0, "B");
            var tamperdSignature2 = new String(signature.SkipLast(1).ToArray());

            var tamperdToken1 = $"{cipher}.{tamperdSignature1}";
            var tamperdToken2 = $"{cipher}.{tamperdSignature2}";

            var cert1 = Token.CreateCertificate(tamperdToken1, Generator.GetTokenOptions());
            var cert2 = Token.CreateCertificate(tamperdToken2, Generator.GetTokenOptions());

            Assert.Null(cert1);
            Assert.Null(cert2);

        }

        [Fact]
        public void Issue_Token_With_Null_Identifier_Must_Returns_Empty_Identifier()
        {
            Certificate certificate = new Certificate(null, "123", null, DateTime.Now, DateTime.Now);

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var cert = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.True( cert.Identifier == "");

        }

        [Fact]
        public void Issue_Token_With_Null_Issuer_Must_Returns_Empty_Issuer()
        {
            Certificate certificate = new Certificate("123", null, null, DateTime.Now, DateTime.Now);

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var cert = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.True(cert.Issuer == "");

        }

        [Fact]
        public void Issue_Token_With_Null_Claims_Must_Returns_Empty_Claims()
        {
            Certificate certificate = new Certificate("123", "456", null, DateTime.Now, DateTime.Now);

            var token = Token.CreateToken(certificate, Generator.GetTokenOptions());
            var cert = Token.CreateCertificate(token, Generator.GetTokenOptions());

            Assert.Empty(cert.Claims);

        }

    }
}
