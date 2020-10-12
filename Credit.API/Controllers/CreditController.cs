using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Application.Credit.Common.Handler;
using Application.Credit.Dtos;
using Credit.API.Filters.ValidateModel;
using Application.Credit.Business.Credit;
using System.Collections.Generic;

namespace Credit.API.Controllers
{
    /// <summary>
    /// Controlador encargado de realizar las operaciones de las cuotas
    /// </summary>
    [Route("api/[controller]/v1")]
    [ApiController]
    [ProducesResponseType(typeof(HttpResponseException), 500)]
    [ProducesResponseType(typeof(HttpResponseDto<string>), 400)]
    [ProducesResponseType(typeof(HttpResponseDto<string>), 401)]
    [ProducesResponseType(typeof(HttpResponseDto<string>), 404)]
    [ValidateModel]
    public class CreditController : ControllerBase
    {
        #region Constructor
        private readonly ICreditBusiness _creditBusiness;
        public CreditController(ICreditBusiness creditBusiness)
        {
            _creditBusiness = creditBusiness;
        }
        #endregion
        /// <summary>
        /// Método a través del cual se crear un nuevo registro de crédito.
        /// </summary>
        /// <param name="credit">Representa los valores del crédito que desea ser creado.</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(HttpResponseDto<bool>), 200)]
        public async Task<IActionResult> Create([FromBody]CreditDataDto credit)
        {
            (HttpStatusCode statusCode, string message, bool response) =
                await _creditBusiness.Create(credit);
            if (statusCode != HttpStatusCode.NoContent && Response != null)
            {
                Response.StatusCode = (int)statusCode;
            }
            return ServiceAnswer<bool>.Response(statusCode, message, response);
        }
        /// <summary>
        /// Método a través del cual se obtienen los datos de un crédito específico.
        /// </summary>
        /// <param name="idCredit">Representa el identificador único de un crédito específico.</param>
        /// <returns></returns>
        [HttpGet("GetById/{idCredit}")]
        [ProducesResponseType(typeof(HttpResponseDto<CreditDataDto>), 200)]
        public async Task<IActionResult> GetById(long idCredit)
        {
            (HttpStatusCode statusCode, string message, CreditDataDto response) =
                await _creditBusiness.GetById(idCredit);
            if (statusCode != HttpStatusCode.NoContent && Response != null)
            {
                Response.StatusCode = (int)statusCode;
            }
            return ServiceAnswer<CreditDataDto>.Response(statusCode, message, response);
        }
        /// <summary>
        /// Método a través del cual se obtienen los datos de un listado de créditos para un cliente específico.
        /// </summary>
        /// <param name="idClient">Representa el identificador único de un cliente específico.</param>
        /// <returns></returns>
        [HttpGet("GetByIdClient/{idClient}")]
        [ProducesResponseType(typeof(HttpResponseDto<List<CreditDataDto>>), 200)]
        public async Task<IActionResult> GetByIdClient(long idClient)
        {
            (HttpStatusCode statusCode, string message, List<CreditDataDto> response) =
                await _creditBusiness.GetByIdClient(idClient);
            if (statusCode != HttpStatusCode.NoContent && Response != null)
            {
                Response.StatusCode = (int)statusCode;
            }
            return ServiceAnswer<List<CreditDataDto>>.Response(statusCode, message, response);
        }
    }
}
