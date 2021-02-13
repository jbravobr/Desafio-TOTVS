using Desafio.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Models.Request;

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
                    return Ok(pdvResult);
                }

                return BadRequest();
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