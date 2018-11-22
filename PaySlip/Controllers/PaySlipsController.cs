using Entity;
using Microsoft.AspNetCore.Mvc;
using PaySlip.LoggerService;
using Service;
using System.Threading.Tasks;

namespace PaySlip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySlipsController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private readonly IPaySlipService _paySlipService;

        public PaySlipsController(
            ILoggerService loggerService,
            IPaySlipService paySlipService)
        {
            _loggerService = loggerService;
            _paySlipService = paySlipService;
        }

        /// <summary>
        /// Health check
        /// </summary>
        /// <returns><see cref="{message = "pay slip api is running"}"/></returns>
        [Route("healthCheck", Name = "HealthCheck")]
        [HttpGet]
        public ActionResult<object> HealthCheck()
        {
            return new { message = "pay slip api is running" };
        }

        /// <summary>
        /// Create pay slip for employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns><see cref="{PaySlip}"/></returns>
        [HttpPost]
        public async Task<ActionResult<Entity.PaySlip>> CreatePaySlipAsync(Employee employee)
        {
            _loggerService.Info($"Begin Create Pay Slip Async.");
            _loggerService.Debug($"Begin Create Pay Slip Async. payload: {employee}");

            var paySlip = await _paySlipService.CreatePaySlipAsync(employee);

            _loggerService.Info($"End Create Pay Slip Async.");
            _loggerService.Debug($"End Create Pay Slip Async. payload: {paySlip}");

            return paySlip;
        }
    }
}