using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FastCore.Application.Extensions
{
    /// <summary>
    /// Extensões de regras para tratamento de senhas.
    /// </summary>
    public static class ValidatePasswordExtension
    {
        #region Extension Methods
        /// <summary>
        /// Checa se a senha tem 15 caracteres.
        /// </summary>
        /// <param name="value">Senha a ser checada.</param>
        /// <returns>boleano</returns>
        public static bool CheckValidPasswordLength(this string value)
        {
            var result = (value.Length == 15) ? true : false;
            return result;
        }

        /// <summary>
        /// Checa se contem ao menos um caracter maiúsculo.
        /// </summary>
        /// <param name="value">Senha a ser checada.</param>
        /// <returns>boleano</returns>
        public static bool ContainsUpperCaseLetters(this string value)
        {
            var result = value.Any(char.IsUpper);
            return result;
        }

        /// <summary>
        /// Checa se contem ao menos um caracter minúsculo.
        /// </summary>
        /// <param name="value">Senha a ser checada.</param>
        /// <returns>boleano</returns>
        public static bool ContainsLowerCaseLetters(this string value)
        {
            var result = value.Any(char.IsLower);
            return result;
        }

        /// <summary>
        /// Checa se contem ao menos um caracter especial.
        /// </summary>
        /// <param name="value">Senha a ser checada.</param>
        /// <returns>boleano</returns>
        public static bool CheckSpecialCharacters(this string value)
        {
            bool result = false;
            var regex = new Regex(@"[^A-Za-z0-9]");
            var isMatch = regex.IsMatch(value);

            if (isMatch)
            {
                var chars = "@!#_-";
                var character = new List<char>();
                character.AddRange(chars);
                var exists = value.ToList().Where(letter => character.Contains(letter)).Any();
                result = (exists) ? true : false;
            }

            return result;
        }

        /// <summary>
        /// Checa se tem alguma sequencia de caracter iguais.
        /// </summary>
        /// <param name="value">Senha a ser checada.</param>
        /// <param name="sequenceLength">Quantidade de vezes que se repetira o caracter.</param>
        /// <returns>boleano</returns>
        public static bool CheckSequentialCharacters(this string value)
        {
            var result = Regex.IsMatch(value, @"(.)\1");
            return result;
        }
        #endregion
    }
}
