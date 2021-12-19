using Microsoft.AspNetCore.Mvc;
using Nebulosa.Bussines.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HistoriaUserController : ControllerBase
    {

        private readonly IUnity _unitOfWork;
        private readonly IPostService _postService;

        public HistoriaUserController( IPostService postService)
        {
            _postService = postService;
        }

        //[HttpGet]
        //[Route("Remove/{id:int}")]
        //public IActionResult Remove(int id)
        //{
        //    try
        //    {
        //        _unitOfWork.HistoriaUser.RemoveHistoryById(id);

        //        return StatusCode(200, _unitOfWork.Complete());

        //    }
        //    catch (Exception err)
        //    {
        //        return StatusCode(500, err.Message);
        //    }
        //    finally
        //    {
        //        _unitOfWork.Dispose();
        //    }
        //}

        [Route("Get")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return StatusCode(200, _postService.saludo());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
