using Microsoft.AspNetCore.Mvc;
using WebAppDemoApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppDemoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Calculator : ControllerBase
    {
        private readonly ILogger<Calculator> logger;

        public Calculator(ILogger<Calculator> logger)
        {
            this.logger = logger;

        }
        // GET api/<Calculator>/5

    }
}
