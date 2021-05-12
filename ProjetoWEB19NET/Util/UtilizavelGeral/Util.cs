using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoWEB19NET.Util.UtilizavelGeral
{
    /// <summary>
    /// Classe util.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 
        /// </summary>
        public class SigningConfigurations
        {
            /// <summary>
            /// Gets the key.
            /// </summary>
            /// <value>
            /// The key.
            /// </value>
            public SecurityKey Key { get; }

            /// <summary>
            /// Gets the signing credentials.
            /// </summary>
            /// <value>
            /// The signing credentials.
            /// </value>
            public SigningCredentials SigningCredentials { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="SigningConfigurations"/> class.
            /// </summary>
            /// <param name="pKey">The p key.</param>
            public SigningConfigurations(string pKey)
            {
                using (var provider = new RSACryptoServiceProvider(2048))
                {
                    Key = new RsaSecurityKey(provider.ExportParameters(true));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pKey));
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class TokenConfigurations
        {

            /// <summary>
            /// Gets or sets the audience.
            /// </summary>
            /// <value>
            /// The audience.
            /// </value>
            public string Audience { get; set; }

            /// <summary>
            /// Gets or sets the issuer.
            /// </summary>
            /// <value>
            /// The issuer.
            /// </value>
            public string Issuer { get; set; }

            /// <summary>
            /// Gets or sets the seconds.
            /// </summary>
            /// <value>
            /// The seconds.
            /// </value>
            public int Seconds { get; set; }

            /// <summary>
            /// Gets or sets the authority.
            /// </summary>
            /// <value>
            /// The authority.
            /// </value>
            public string Authority { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [validate audience].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [validate audience]; otherwise, <c>false</c>.
            /// </value>
            public bool ValidateAudience { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [valid issuer].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [valid issuer]; otherwise, <c>false</c>.
            /// </value>
            public bool ValidIssuer { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [save token].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [save token]; otherwise, <c>false</c>.
            /// </value>
            public bool SaveToken { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [validate actor].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [validate actor]; otherwise, <c>false</c>.
            /// </value>
            public bool ValidateActor { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [validate lifetime].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [validate lifetime]; otherwise, <c>false</c>.
            /// </value>
            public bool ValidateLifetime { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [validate issuer signing key].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [validate issuer signing key]; otherwise, <c>false</c>.
            /// </value>
            public bool ValidateIssuerSigningKey { get; set; }

            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>
            /// The key.
            /// </value>
            public string Key { get; set; }
        }

        /// <summary>
        /// Gets the claims identity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static ClaimsIdentity GetClaimsIdentity(string user, string email)
        {
            return new ClaimsIdentity
            (
                new GenericIdentity(email),
                new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.UniqueName, user)
                }
            );
        }
    }
}
