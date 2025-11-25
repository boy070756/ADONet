using Microsoft.AspNetCore.Mvc;

namespace APIEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("GetValue")]
        public IActionResult GetValue()
        {
            return Ok(new { success = true, message = "hello world" });
        }
    }
}
