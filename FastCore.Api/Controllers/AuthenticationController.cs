using FastCore.Application;
using FastCore.Application.EntitiesVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FastCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticateApplication _authenticateApplication;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticateApplication authenticateApplication)
        {
            _logger = logger;
            _authenticateApplication = authenticateApplication;
        }

        /// <summary>
        /// Obtem token de autenticação para os serviços.
        /// </summary>
        /// <param name="entity">Entidade com dados acesso.</param>
        /// <returns>Obtem o token de autenticação.</returns>
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken([FromBody] UserAutheticateVm entity)
        {
            var result = await _authenticateApplication.GetToken(entity);
            return Ok(result);
        }

        /// <summary>
        /// Valida se a senha atende as regras de segurança.
        /// </summary>
        /// <param name="entity">Entidade com a senha a ser validada.</param>
        /// <returns>Verdadeiro se senha Ok, ou Falso para senha não Ok.</returns>
        [Authorize]
        [HttpPost("ValidatePassword")]
        public async Task<IActionResult> ValidatePassword([FromBody] UserValidatePasswordVm entity)
        {
            var result = await _authenticateApplication.ValidatePassword(entity.Password);
            return Ok(result);
        }

        /// <summary>
        /// Gera uma senha seguindo os padrões de segurança.
        /// </summary>
        /// <returns>Senha gerada seguindo os padrões de segurança.</returns>
        [Authorize]
        [HttpGet("GeneratePassword")]
        public async Task<IActionResult> GeneratePassword()
        {
            var result = await _authenticateApplication.GeneratePasswordAsync();
            return Ok(result);
        }
    }
}
