using CarroShop.Dados.Contexto;
using CarroShop.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CarroShop.Dados.Gerador
{
    public class GeradorDados
    {
        public static void InicializarDados(IServiceProvider provedorServico)
        {
            using (var contexto = new CarroDbContexto(provedorServico.GetRequiredService<DbContextOptions<CarroDbContexto>>()))
            {
                if (contexto.Carros.Any())
                {
                    return;
                }
                contexto.Carros.AddRange
                    (
                        new Carro
                        {
                            Id = 1,
                            Marca = "Chevrolet",
                            Modelo = "Ônix",
                            Ano = 2012
                        },
                        new Carro
                        {
                            Id = 2,
                            Marca = "Jeep",
                            Modelo = "Renegade",
                            Ano = 2021
                        },
                        new Carro
                        {
                            Id = 3,
                            Marca = "Hyundai",
                            Modelo = "HB20",
                            Ano = 2019
                        }
                    );
                contexto.SaveChanges();
            }
        }
    }
}
