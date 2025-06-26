using ComisionApi.Data;
using ComisionApi.Models;

namespace ComisionApi.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Usuarios
            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario { Nombre = "Perico P" },
                    new Usuario { Nombre = "Zoila Baca" },
                    new Usuario { Nombre = "Aquiles C" },
                    new Usuario { Nombre = "Johny M" }
                };

                context.Usuarios.AddRange(usuarios);
                context.SaveChanges();
            }

            // Reglas de comisión
            if (!context.ReglasComision.Any())
            {
                var reglas = new List<ReglaComision>
            {
                new ReglaComision { Rule = 0.06m, Amount = 600 },
                new ReglaComision { Rule = 0.08m, Amount = 500 },
                new ReglaComision { Rule = 0.10m, Amount = 800 },
                new ReglaComision { Rule = 0.15m, Amount = 1000 }
            };


                context.ReglasComision.AddRange(reglas);
                context.SaveChanges();
            }

            // Ventas
            if (!context.Ventas.Any())
            {
                var ventas = new List<Venta>
                {
                    new Venta { FechaVenta = DateTime.Parse("2023-05-21"), Vendedor = "Perico P", Monto = 400 },
                    new Venta { FechaVenta = DateTime.Parse("2023-05-29"), Vendedor = "Zoila Baca", Monto = 600 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-03"), Vendedor = "Zoila Baca", Monto = 200 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-09"), Vendedor = "Perico P", Monto = 300 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-11"), Vendedor = "Aquiles C", Monto = 900 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-14"), Vendedor = "Perico P", Monto = 500 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-26"), Vendedor = "Johny M", Monto = 400 },
                    new Venta { FechaVenta = DateTime.Parse("2023-06-30"), Vendedor = "Johny M", Monto = 600 }
                };

                context.Ventas.AddRange(ventas);
                context.SaveChanges();
            }
        }
    }
}