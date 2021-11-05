using FastCore.Application;
using FastCore.Application.EntitiesVm;
using FastCore.Application.Extensions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FastCore.Tests
{
    public class AuthenticationApplicationTest
    {
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = GetConfiguration();
        }

        private static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            return configuration;
        }

        /// <summary>
        /// Testa o método de geração de token obedecendo o fluxo correto.
        /// Deve retornar um token valido.
        /// </summary>
        [Test]
        public async Task ShouldGenerateTokenAsOkAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);            
            var entity = new UserAutheticateVm   { Username = "leo", Password = "leoM@de!r@s2021" };
            var result = await authenticateApplication.GetToken(entity);
            Assert.NotNull(result.Token);
        }

        /// <summary>
        /// Testa o método de geração de token obedecendo o fluxo correto, mas passando um usuario com a senha invalida.
        /// Deve retornar null para propriedade token.
        /// </summary>
        [Test]
        public async Task ShouldGenerateTokenNoOkAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);
            var entity = new UserAutheticateVm { Username = "leo", Password = "Madeirasleo2023" };
            var result = await authenticateApplication.GetToken(entity);
            Assert.IsNull(result.Token);
        }

        /// <summary>
        /// Testa a validação de senha com uma senha correta ou seja que segue as regras de seguraça.
        /// Deve retornar verdadeiro.
        /// </summary>
        [Test]
        public async Task ShouldTestValidationPasswordAsOkAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);
            var entity = new UserValidatePasswordVm { Password = "leoM@de!r@s2021" };
            var result = await authenticateApplication.ValidatePassword(entity.Password);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Testa a validação de senha com uma senha incorreta ou seja que não segue as regras de seguraça.
        /// Deve retornar falso.
        /// </summary>
        [Test]
        public async Task ShouldTestValidationPasswordNoOkAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);
            var entity = new UserValidatePasswordVm { Password = "madeirasleo" };
            var result = await authenticateApplication.ValidatePassword(entity.Password);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Testa a geração de nova senha.
        /// Deve retornar uma senha.
        /// </summary>
        [Test]
        public async Task ShouldTestGenerateNewPasswordAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);
            var password = await authenticateApplication.GeneratePasswordAsync();
            Assert.IsNotNull(password);
        }

        /// <summary>
        /// Testa a geração de nova senha e valida se a senha gerada segue 
        /// as regras segurança.
        /// </summary>
        [Test]
        public async Task ShouldTestGenerateNewPasswordAndValidateIfOkAsync()
        {
            IAuthenticateApplication authenticateApplication = new AuthenticateApplication(_configuration);
            var password = await authenticateApplication.GeneratePasswordAsync();
            var result = await authenticateApplication.ValidatePassword(password);
            Assert.IsTrue(result);
        }
    }
}