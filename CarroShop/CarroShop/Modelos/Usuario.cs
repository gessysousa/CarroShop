using System;
using System.ComponentModel.DataAnnotations;

namespace CarroShop.Modelos
{
    public class Usuario
    {
        #region Propriedades
        public Guid Id { get; private set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Role { get; set; }
        #endregion

        #region Construtor
        public Usuario()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Metodos
        public bool EhValido()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Senha))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Role))
            {
                return false;
            }

            return true;
        }
        #endregion

    }
}
