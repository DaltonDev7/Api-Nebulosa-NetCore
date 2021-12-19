using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{

 
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {

        [Route("Get")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return StatusCode(200, "Heloooo probando :), ruta protegida");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




    }
}
