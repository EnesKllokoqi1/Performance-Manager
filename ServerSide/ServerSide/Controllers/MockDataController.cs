using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
          await  Task.Delay(3400);
            return Content($"The JSON mock data  : \n\n{{\r\n  \"id\": 12345,\r\n  \"name\": \"John Doe\",\r\n  \"email\": \"johndoe@example.com\",\r\n  \"isActive\": true,\r\n  \"roles\": [\"admin\", \"editor\"],\r\n  \"profile\": {{\r\n    \"age\": 30,\r\n    \"address\": {{\r\n      \"street\": \"123 Main St\",\r\n      \"city\": \"Springfield\",\r\n      \"zip\": \"12345\"\r\n    }},\r\n    \"phoneNumbers\": [\r\n      {{\r\n        \"type\": \"home\",\r\n        \"number\": \"555-1234\"\r\n      }},\r\n      {{\r\n        \"type\": \"mobile\",\r\n        \"number\": \"555-5678\"\r\n      }}\r\n    ]\r\n  }},\r\n  \"createdAt\": \"2025-06-20T14:30:00Z\",\r\n  \"tags\": [\"random\", \"json\", \"example\"]\r\n}}");
        }
    }
}
