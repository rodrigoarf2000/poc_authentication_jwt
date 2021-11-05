using FastCore.Application.EntitiesVm;
using FastCore.Application.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public interface IAuthenticateApplication
    {
        Task<UserAutheticateVm> GetToken(UserAutheticateVm entity);
        Task<bool> ValidatePassword(string password);
        Task<string> GeneratePasswordAsync();
    }

    public class AuthenticateApplication : IAuthenticateApplication
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticateApplication(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtem o token de acesso se atenticação do usuário for válida.
        /// </summary>
        /// <param name="entity">Usuário e senha de acesso.</param>
        /// <returns>Objeto com dados de acesso permitido.</returns>
        public async Task<UserAutheticateVm> GetToken(UserAutheticateVm entity)
        {
            if (entity == null) { throw new ArgumentException($"Usuário e senha não pode ser vazio.", "entity"); }

            if (entity.Username.Equals("leo") && entity.Password.Equals("leoM@de!r@s2021"))
            {
                var JwtIssuer = _configuration[AppSettingsHelper.JwtIssuer];
                var JwtAudience = _configuration[AppSettingsHelper.JwtAudience];
                var JwtExpires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration[AppSettingsHelper.JwtExpiration]));
                var JwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[AppSettingsHelper.JwtSignature]));

                var credentials = new SigningCredentials(JwtSecurityKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(issuer: JwtIssuer, audience: JwtAudience, expires: JwtExpires, signingCredentials: credentials);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(jwtSecurityToken);

                var result = new UserAutheticateVm
                {
                    Username = entity.Username,
                    Token = token,
                    ExpiresIn = JwtExpires,
                    Message = $"Tokem gerado com sucesso, válido até {JwtExpires}"
                };

                return await Task.FromResult(result);
            }

            return new UserAutheticateVm { Message = $"Não foi possivel autenticar o usuário {entity.Username}." };
        }

        /// <summary>
        /// Verifica se a senha informada atende as regras de segurança.
        /// </summary>
        /// <param name="password">Senha a ser validada.</param>
        /// <returns>Verdadeiro se a senha estiver em conformidade, False se não estiver em conformidade.</returns>
        public async Task<bool> ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) { throw new ArgumentException($"A senha esta em branco, não é possivel valida-la.", "password"); }

            if (password.CheckValidPasswordLength() &&
                password.ContainsUpperCaseLetters() &&
                password.ContainsLowerCaseLetters() &&
                password.CheckSpecialCharacters() &&
                !password.CheckSequentialCharacters())
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Gera uma senha seguindo os padrões de segurança.
        /// </summary>
        /// <returns>Senha gerada seuindo os padrões de segurança.</returns>
        public async Task<string> GeneratePasswordAsync()
        {
            var specialCharactersInPassword = _configuration[AppSettingsHelper.SpecialCharactersInPassword];
            if (string.IsNullOrEmpty(specialCharactersInPassword))
            { throw new ArgumentNullException("O parâmetro SpecialCharactersInPassword do arquivo de configuração não deve ser vazio.", "SpecialCharactersInPassword"); }

            if (_configuration[AppSettingsHelper.CharacterSize] == null || Convert.ToInt32(_configuration[AppSettingsHelper.CharacterSize]) == 0)
            { throw new ArgumentNullException("O parâmetro CharacterSize do arquivo de configuração não deve ser vazio, informe o tamanho de caracteres para geração de senha.", "CharacterSize"); }

            var characterSize = Convert.ToInt32(_configuration[AppSettingsHelper.CharacterSize]);
            var alphaNumericCharacters = $"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789{specialCharactersInPassword}";
            var result = GetRandomString(alphaNumericCharacters, characterSize);
            var validated = await ValidatePassword(result);
            var password = (validated) ? await Task.FromResult(result) : await GeneratePasswordAsync(); // if password ok return, else use recursion, tested!. 
            return password;
        }

        /// <summary>
        /// Gera string randomica.
        /// </summary>
        /// <param name="characterSet"></param>
        /// <returns>String</returns>
        private string GetRandomString(IEnumerable<char> characterSet, int lenghtChars)
        {
            if (characterSet == null) { throw new ArgumentNullException("O parâmetro characterSet não deve ser vazio.", "characterSet"); }
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0) { throw new ArgumentException("O parâmetro characterSet não deve ser vazio.", "characterSet"); }

            var length = lenghtChars;
            var bytes = new byte[length * 8];

            new RNGCryptoServiceProvider().GetBytes(bytes);
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                password[i] = characterArray[value % (uint)characterArray.Length];
            }

            var result = new string(password);
            return result;
        }
        #endregion
    }
}
