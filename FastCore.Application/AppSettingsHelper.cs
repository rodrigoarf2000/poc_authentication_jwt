using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public static class AppSettingsHelper
    {
        #region Password Configuration Keys
        public static string CharacterSize { get; set; } = "AppConfiguration:PasswordRules:CharacterSize";
        public static string SpecialCharactersInPassword { get; set; } = "AppConfiguration:PasswordRules:SpecialCharactersInPassword";
        #endregion

        #region Jwt Configuration Keys
        public static string JwtIssuer { get; set; } = "Jwt:Issuer";
        public static string JwtAudience { get; set; } = "Jwt:Audience";
        public static string JwtExpiration { get; set; } = "Jwt:Expiration";
        public static string JwtSignature { get; set; } = "Jwt:Signature";
        #endregion
    }
}
