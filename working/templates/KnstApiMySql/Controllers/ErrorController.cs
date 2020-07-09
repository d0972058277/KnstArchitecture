using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnstApiMySql.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Error")]
        public IActionResult Error() => Problem();

        [HttpGet("Test")]
        public void Test() =>
            throw new Exception("Test");
    }
}