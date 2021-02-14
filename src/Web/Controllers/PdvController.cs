using Desafio.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdvController : ControllerBase
    {
        private readonly IPdvServices _pdvServices;

        public PdvController(IPdvServices pdvServices)
        {
            _pdvServices = pdvServices;
        }

        [HttpPost]
        [Authorize]
        [Route("api/pdv/pay-bill")]
        public IActionResult PayBill([FromBody] PaymentRequest request)
        {
            try
            {
                var pdvResult = _pdvServices.PayBill(request.AmountPaid, request.TotalPayable);
                if (pdvResult.IsValid)
                {
                    return Ok(new PaymentResponse(pdvResult.BankNotesToReturn, pdvResult.BankCoinsToReturn));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/pdv/history")]
        public IActionResult History()
        {
            try
            {
                var results = _pdvServices.GetPdvHistories(new DateTime(2020, 01, 01), DateTime.Now);
                if (results != null && results.Count > 0)
                {
                    return Ok(new PdvHistoryResponse(results));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Gerente")]
        [Route("api/pdv/{pdvID}/close")]
        public async Task<IActionResult> Close(Guid pdvID)
        {
            throw new NotImplementedException();
        }
    }
}