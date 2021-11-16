using CarroShop.Adaptadores;
using CarroShop.Dados.Repositorio.Interfaces;
using CarroShop.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarroShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Campos
        private readonly IJwtAuthGerenciador jwtAuthGerenciador;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        #endregion

        #region Construtor
        public AuthController(IJwtAuthGerenciador jwtAuthGerenciador, IUsuarioRepositorio usuarioRepositorio)
        {
            this.jwtAuthGerenciador = jwtAuthGerenciador;
            this.usuarioRepositorio = usuarioRepositorio;
        }
        #endregion

        #region Métodos
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] JwtUsuarioCredenciais jwtUsuarioCredenciais)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var usuarioAtual = await usuarioRepositorio.RecuperarPorCredenciais(jwtUsuarioCredenciais.Email, jwtUsuarioCredenciais.Senha);

                if(usuarioAtual == null)
                {
                    return NotFound();
                }

                return Ok(jwtAuthGerenciador.GerarToken(usuarioAtual.ParaJwtCredenciais()));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Mensagem = e.Message
                });
            }
        }
        #endregion
    }
}
