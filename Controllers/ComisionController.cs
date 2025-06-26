using Microsoft.AspNetCore.Mvc;
using ComisionApi.Services;

namespace ComisionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComisionController : ControllerBase
    {
        private readonly ComisionService _comisionService;

        public ComisionController(ComisionService comisionService)
        {
            _comisionService = comisionService;
        }

        /// <summary>
        /// Endpoint para filtrar comisiones por rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del rango.</param>
        /// <param name="fechaFin">Fecha de fin del rango.</param>
        /// <returns>Lista de informes de comisiones.</returns>
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<ComisionReport>>> FiltrarComisiones(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var comisiones = await _comisionService.CalcularComisiones(fechaInicio, fechaFin);
            return Ok(comisiones);
        }
    }
}