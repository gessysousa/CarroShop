using CarroShop.Dados.Contexto;
using CarroShop.Modelos;
using CarroShop.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarroShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarrosController : ControllerBase
    {
        #region Campos
        private readonly CarroDbContexto contexto;
        #endregion

        #region Construtor
        public CarrosController(CarroDbContexto contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carros = await contexto.Carros.ToListAsync();
            return Ok(carros);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPorId([FromRoute] int id)
        {
            var carro = await contexto.Carros.FindAsync(id);

            if (id == 0)
            {
                return BadRequest();
            }

            if (carro == null)
            {
                return NotFound();
            }

            return Ok(carro);
        }

        [HttpPost]
        [Authorize(Roles = RolesUsuario.Admin)]
        public async Task<IActionResult> Post([FromBody] Carro carro)
        {
            contexto.Carros.Add(carro);
            await contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPorId), new { id = carro.Id }, carro);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RolesUsuario.Admin)]
        public async Task<IActionResult> Put([FromRoute] int id, Carro carro)
        {
            if(id != carro.Id)
            {
                return BadRequest();
            }

            if (!VerificarSeCarroExiste(carro.Id))
            {
                return NotFound();
            }

            contexto.Entry(carro).State = EntityState.Modified;

            await contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = RolesUsuario.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var carro = await contexto.Carros.FindAsync(id);

            if(carro == null)
            {
                return NotFound();
            }

            contexto.Carros.Remove(carro);

            await contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificarSeCarroExiste(int id)
        {
            return contexto.Carros.Any(Livro => Livro.Id == id);
        }

        #endregion
    }
}
