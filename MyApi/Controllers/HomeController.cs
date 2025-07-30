using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [Route("services/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //URL:  /services/home
        //Default Route: HTTPGET -- services/home 

        //URL : /services/home/get
        //URL MVC: /services/home/get
        [HttpGet(template: "get")]
        public string Get()
        {
            return "Resource not found.";
            //Default Status code - 200 OK 
        }

        //URL : /services/home
        //URL MVC: /services/home/fetch
        [HttpGet]
        public string Fetch()
        {
            try
            {
                int a = 0, b = 0, c = 0;
                c = a / b;
            }
            catch (DivideByZeroException dec)
            {

                return dec.Message;
            }
            return "Hello from Fetch method in HomeController!";
        }
        //URL:  services/home/status/0
        //URL:  services/home/status/10
        [HttpGet("status/{id}")]
        public IActionResult Result(int id=0)  //Model Binder will read and inject the value into the method
        {
            var status = (id==0)? false : true; 
            if(status)
            {
                return Ok("Success"); //200-OK
            } else
            {
                try
                {
                    int a = 0, b = 0, c = 0;
                    c = a / b;
                } catch (DivideByZeroException dec)
                {
                    return Problem(dec.Message, null, StatusCodes.Status500InternalServerError, "Error"); 
                }
                return BadRequest("Something missing"); //500 - BadRequest
            }
        }

        [HttpPost(template: "")]
        //URL :  /services/home/?id=11&name=11
        //public IActionResult POst(int id, string name) {  }

        public IActionResult Post(TestClass tc)
        {
            var obj = new { Id = tc.Id, Name = tc.Name };
            if (obj.Id == 1) return BadRequest(obj);

            return Ok(obj);  //JSON string
        }
        //URL: /services/home/19
        [HttpPut(template: "products/{id}")]
        public IActionResult Put1(int id, TestClass tc)
        {
            return Ok();
        }
        [HttpPut(template: "category/{id}")]
        public IActionResult Put2(int id, TestClass tc)
        {
            return Ok();
        }
        [HttpDelete(template: "{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
    public class TestClass
    {
        public int Id { get; set;  }
        public string Name { get; set; }

    }
}
