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
        [HttpPost]
        [Authorize]
        [Route("api/pdv/pay-bill")]
        public async Task<IActionResult> PayBill([FromBody] PaymentRequest request)
        {
            throw new NotImplementedException();
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