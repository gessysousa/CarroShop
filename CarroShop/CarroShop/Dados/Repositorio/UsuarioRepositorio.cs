using CarroShop.Dados.Contexto;
using CarroShop.Dados.Repositorio.Interfaces;
using CarroShop.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarroShop.Dados.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        #region Campos
        private readonly CarroDbContexto contexto;
        #endregion

        #region Construtor
        public UsuarioRepositorio(CarroDbContexto contexto)
        {
            this.contexto = contexto;
        }
        #endregion

        #region Metodos
        public async Task<int> AdicionarAsync(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            
            return await contexto.SaveChangesAsync();
        }

        public async Task<Usuario> RecuperarPorCredenciais(string email, string senha)
        {
            return await contexto.Usuarios.SingleOrDefaultAsync(usuario => usuario.Email == email && usuario.Senha == senha);
        }

        public async Task<IEnumerable<Usuario>> RecuperarTodosAsync()
        {
            return await contexto.Usuarios.ToListAsync();
        }

        public async Task<bool> VerificarUsuarioAsync(string email, string senha)
        {
            var usuario = await contexto.Usuarios.SingleOrDefaultAsync(usuario => usuario.Email == email && usuario.Senha == senha);
            return usuario == null;
        }
        #endregion
        
    }
}
