# Secure Token

This library provides an easy way (without bleeding) for encrypting claims with signiture and securing server APIs. 

The diffrence between this mechanism and other systems is in the freedom of choosing a custom encryption and hashing algorithms.

The library abstracts the process of encrypting/signing and enables the client to use any algorithms simply by implementing  IEncryption (symmetric or asymmetric) and ISigner.


A custom authentication handler has been included to enable client to issue and consume SecureTokens for authorizing users.


## How to Issue a Token

```
SecureTokenOptions tokenOptions = new SecureTokenOptions
{
    Encryptor = new AesEncryptor(SecureRandomBytes.Generate(32), SecureRandomBytes.Generate(16)),
    Signer = new SHA512Signer(SecureRandomBytes.Generate(64))
};

Certificate cert = new Certificate("identifier",
                                               "issuer",
                                               claims,
                                               DateTime.Now,
                                               TimeSpan.FromSeconds(10));

// Create Token
var token = SecureToken.Token.CreateToken(cert, tokenOptions);


// Depart Token to Certificate
var cert = SecureToken.Token.CreateCertificate(token, tokenOptions);

if(cert == null)
{
    // Not a valid token
}

if(cert.IsValid)
{
    // Expired token
}


```

## AesEncryptor and SHA512Signer
These two classes implement IEncryption and ISigner respectively. Any custom implementation could be used instead.

## PBKDF2 Signer
For longer and more secure signature this signer has been added with at least 10000 iterations and 128 byes length.

## RsaEncryptor
This class implements IEncryption and enable asymmetric encryption based on RSA algorithm. Minimum key size is 4098 bits.

## Authetication Handler
The library extends AuthenticationBuilder class and provides easy inegration with Asp.netcore dependency injection.


```
// using SecureToken.Authentication;
services.AddAuthentication(SecureTokenDefaults.AuthenticationScheme)
.AddSecureTokenAuthentication
(config =>
    {
        config.Encryptor = new AesEncryptor(SecureRandomBytes.Generate(32),
                                            SecureRandomBytes.Generate(16));

        config.Signer = new SHA512Signer(SecureRandomBytes.Generate(64));
    }
, "Token"); // Default is Authorization

// Inject as base64 string using environment variables
// services.AddAuthentication(SecureTokenDefaults.AuthenticationScheme)
// .AddSecureTokenAuthentication
// (config =>
//    {
//        config.Encryptor = new AesEncryptor(base64Key,
//                                            base64InitialVector);

//        config.Signer = new SHA512Signer(base64HashKey);
//    }
//, "Token");

// RSA Encryption
// config.Encryptor = new RsaEncryptor(rsa.ToXmlString(true));

// PBKDF2 signer
// config.Signer = new PBKDF2Signer(SecureRandomBytes.Generate(64), 128, 10000);


```

## Issue Authorization Token
The authentication handler adds TokenOptions to Asp.netcore dependency injection collection.

```
var token = AuthorizationToken.Issue(name, claims, DateTime.Now, TimeSpan.FromSeconds(300), TokenOptions);

// Use [Authorize] modifier before an API to enforce authorization. 

```








