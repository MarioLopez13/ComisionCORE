using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComisionApi.Data;
using ComisionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComisionApi.Services
{
    public class ComisionService
    {
        private readonly AppDbContext _context;

        public ComisionService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calcula las comisiones para los vendedores dentro del rango de fechas especificado.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del rango.</param>
        /// <param name="fechaFin">Fecha de fin del rango.</param>
        /// <returns>Lista de informes de comisiones agrupados por vendedor.</returns>
        public async Task<IEnumerable<ComisionReport>> CalcularComisiones(DateTime fechaInicio, DateTime fechaFin)
        {
            // Filtra las ventas dentro del rango de fechas
            var ventasFiltradas = await _context.Ventas
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                .ToListAsync();

            // Agrupa las ventas por vendedor y calcula el monto total vendido por cada uno
            var ventasPorVendedor = ventasFiltradas
                .GroupBy(v => v.Vendedor)
                .Select(g => new
                {
                    Vendedor = g.Key,
                    MontoTotal = g.Sum(v => v.Monto)
                })
                .ToList();

            var reportes = new List<ComisionReport>();

            foreach (var ventaGrupo in ventasPorVendedor)
            {
                // Encuentra la regla de comisión aplicable
                var reglaAplicable = _context.ReglasComision
                    .OrderByDescending(r => r.Amount)
                    .FirstOrDefault(r => r.Amount <= ventaGrupo.MontoTotal);

                if (reglaAplicable != null)
                {
                    // Calcula la comisión
                    var comision = ventaGrupo.MontoTotal * reglaAplicable.Rule;

                    reportes.Add(new ComisionReport
                    {
                        Vendedor = ventaGrupo.Vendedor,
                        MontoTotal = ventaGrupo.MontoTotal,
                        Comision = comision
                    });
                }
            }

            return reportes;
        }
    }

    // Clase para representar el informe de comisiones
    public class ComisionReport
    {
        public string Vendedor { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public decimal Comision { get; set; }
    }
}