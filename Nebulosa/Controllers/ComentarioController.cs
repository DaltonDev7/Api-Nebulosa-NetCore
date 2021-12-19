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
    public class ComentarioController : Controller
    {

        private readonly IUnity _unitOfWork;

        public ComentarioController(IUnity unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Comentario comentario)
        {
            try
            {
                comentario.FechaCreacion = DateTime.UtcNow.AddMinutes(-240);
                _unitOfWork.Comentario.Add(comentario);

                return StatusCode(201, _unitOfWork.Complete());

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

        [HttpGet]
        [Route("RemoveById/{idComentario}")]
        public IActionResult Remove(int Idcomentario)
        {
            try
            {

                _unitOfWork.Comentario.RemoveComentarioById(Idcomentario);

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



        [HttpGet]
        [Route("GetComentariosByIdPost/{idComentario}")]
        public async Task<IActionResult> GetComentariosByIdPost(int Idcomentario)
        {
            try
            {

              var comentarios =  await _unitOfWork.Comentario.GetComentarioByIdPost(Idcomentario);

                return StatusCode(200, comentarios);

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

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Comentario comentario)
        {
            try
            {
                _unitOfWork.Comentario.Update(comentario);

                return StatusCode(204, _unitOfWork.Complete());

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
