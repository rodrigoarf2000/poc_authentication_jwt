using FastCore.Application.Extensions;
using NUnit.Framework;

namespace FastCore.Tests
{
    public class ValidatePasswordExtensionTest
    {
        /// <summary>
        /// Testa o método de extensão CheckValidPasswordLength com uma senha correta ou seja 
        /// que obedece os requisitos de segurança, deve retornar true. 
        /// </summary>
        [Test]
        public void ShouldTestPasswordAsOk()
        {
            var password = "leoM@de!r@s2021";
            var result = password.CheckValidPasswordLength();
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Testa o método de extensão CheckValidPasswordLength com uma senha incorreta ou seja 
        /// que não obedece os requisitos de segurança, deve retornar false. 
        /// </summary>
        [Test]
        public void ShouldTestPasswordNoOk()
        {
            var password = "M@de!r@s1";
            var result = password.CheckValidPasswordLength();
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Testa o método de extensão ContainsUppercaseLetters com uma senha correta ou seja 
        /// que não obedece os requisitos de segurança e deve retornar true se na senha tiver 
        /// ao menos um caracter maiúsculo. 
        /// </summary>
        [Test]
        public void ShouldTestPasswordWithLeastOneUppercaseCharacterAsOk()
        {
            var password = "leoM@de!r@s2021";
            var result = password.ContainsUpperCaseLetters();
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Testa o método de extensão ContainsUppercaseLetters com uma senha incorreta ou seja 
        /// que não obedece os requisitos de segurança e deve retornar false ja que a senha
        /// não tem um caracter maiúsculo.
        /// </summary>
        [Test]
        public void ShouldTestPasswordWithLeastOneUppercaseCharacterNoOk()
        {
            var password = "leom@de!r@s2021";
            var result = password.ContainsUpperCaseLetters();
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Testa o método de extensão ContainsLowercaseLetters com uma senha correta ou seja 
        /// que obedece os requisitos de segurança e deve retornar true se na senha tiver 
        /// ao menos um caracter minúsculo. 
        /// </summary>
        [Test]
        public void ShouldTestPasswordWithLeastOneLowercaseCharacterAsOk()
        {
            var password = "leoM@de!r@s2021";
            var result = password.ContainsLowerCaseLetters();
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Testa o método de extensão ContainsLowercaseLetters com uma senha incorreta ou seja 
        /// que não obedece os requisitos de segurança e deve retornar false se na senha não tiver 
        /// nenhum caracter minúsculo. 
        /// </summary>
        [Test]
        public void ShouldTestPasswordWithLeastOneLowercaseCharacterNoOk()
        {
            var password = "LEOM@DE!R@S2021";
            var result = password.ContainsLowerCaseLetters();
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Testa o método de extensão CheckSpecialCharacters com uma senha correta ou seja 
        /// que obedece os requisitos de segurança e deve retornar true se na senha existir
        /// um caracter especial como estes @#!_-
        /// </summary>
        [Test]
        public void ShouldTestSpecialCharacterInPasswordAsOk()
        {
            var password = "leom@de!r@s2021";
            var result = password.CheckSpecialCharacters();
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Testa o método de extensão CheckSpecialCharacters com uma senha incorreta ou seja 
        /// que não obedece os requisitos de segurança e deve retornar false se na senha não existir
        /// um caracter especial como estes @#!_-
        /// </summary>
        [Test]
        public void ShouldTestSpecialCharacterInPasswordNoOk()
        {
            var password = "leoMadeiras2021";
            var result = password.CheckSpecialCharacters();
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Testa o método de extensão CheckSequentialCharacters com uma senha correta ou seja 
        /// que obedece os requisitos de segurança e deve retornar true se na senha não existir
        /// a presença de caracteres duplicados mais de uma vez ou 
        /// seja caracteres em sequencial.
        /// </summary>
        [Test]
        public void ShouldTestSequentialCharactersInPasswordAsOk()
        {
            var password = "leoM@de!r@s2021";
            var result = password.CheckSequentialCharacters();
            Assert.IsFalse(result); // return false if not exists sequential characters. 
        }

        /// <summary>
        /// Testa o método de extensão CheckSequentialCharacters com uma senha incorreta ou seja 
        /// que não obedece os requisitos de segurança e deve retornar false se na senha existir
        /// a presença de caracteres duplicados mais de uma vez.
        /// </summary>
        [Test]
        public void ShouldTestSequentialCharactersInPasswordNoOk()
        {
            var password = "leee@de!r@s2021";
            var result = password.CheckSequentialCharacters();
            Assert.IsTrue(result); // return true if exists sequential characters.
        }

    }
}