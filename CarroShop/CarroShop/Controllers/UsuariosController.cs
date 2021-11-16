using CarroShop.Dados.Repositorio.Interfaces;
using CarroShop.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarroShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        #region Campos
        private readonly IUsuarioRepositorio usuarioRepositorio;
        #endregion

        #region Construtor
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio; 
        }
        #endregion

        #region Metodos

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await usuarioRepositorio.RecuperarTodosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                if(usuario == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (!usuario.EhValido())
                {
                    return BadRequest();
                }

                var affectedRows = await usuarioRepositorio.AdicionarAsync(usuario);

                if(affectedRows == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    CodigoStatus = StatusCodes.Status500InternalServerError,
                    Mensagem = e.Message
                });
            }
        }
        #endregion
    }
}
