using Microsoft.AspNetCore.Mvc;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SexoController : ControllerBase
    {

        private readonly IUnity _unitOfWork;

        public SexoController(IUnity unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Sexo sexo)
        {

            try
            {
                _unitOfWork.Sexo.Add(sexo);
                return StatusCode(200, _unitOfWork.Complete());

            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }


    }
}
