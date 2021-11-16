using CarroShop.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarroShop.Dados.Contexto
{
    public class CarroDbContexto : DbContext
    {
        #region Construtor
        public CarroDbContexto(DbContextOptions<CarroDbContexto> options) : base (options)
        {

        }
        #endregion

        #region Propriedades
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion
    }
}
