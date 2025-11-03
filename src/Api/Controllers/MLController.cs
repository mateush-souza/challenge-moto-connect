using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using challenge_moto_connect.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace challenge_moto_connect.Api.Controllers
{
    /// <summary>
    /// Controller para serviços de Machine Learning (ML.NET).
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class MLController : ControllerBase
    {
        private readonly MLService _mlService;

        /// <summary>
        /// Construtor do controller de ML.NET.
        /// </summary>
        /// <param name="mlService">Serviço de Machine Learning.</param>
        public MLController(MLService mlService)
        {
            _mlService = mlService;
        }

        /// <summary>
        /// Realiza a predição de necessidade de manutenção para um veículo.
        /// </summary>
        /// <param name="input">Dados de entrada do veículo para predição.</param>
        /// <returns>Resultado da predição.</returns>
        /// <response code="200">Retorna a predição com sucesso.</response>
        /// <response code="401">Não autorizado.</response>
        [HttpPost("predict-maintenance")]
        [ProducesResponseType(typeof(MaintenancePrediction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult PredictMaintenance([FromBody] VehicleMaintenanceInput input)
        {
            var prediction = _mlService.PredictMaintenance(input);
            return Ok(prediction);
        }
    }
}
