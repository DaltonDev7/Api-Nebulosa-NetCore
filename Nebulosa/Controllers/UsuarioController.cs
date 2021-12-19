using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly IUsuarioService _usuarioService;
        private readonly IUnity _unityOfWork;

        public UsuarioController(
           
             UserManager<Usuario> userManager,
             RoleManager<Rol> roleManager,
             IUsuarioService usuarioService,
             IUnity unityOfWork
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
            _unityOfWork = unityOfWork;
        }


        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var mensaje = "Helloo";
                return StatusCode(200, mensaje);

            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }


        [Route("GetUserById/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetUserData(int id)
        {
            try
            {
                var info = await _usuarioService.GetUser(id);

                return StatusCode(200, info);
            }
            catch (Exception e )
            {

                return StatusCode(500, e.Message);
            }
        }



        [Route("GetAllUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var info = await _usuarioService.getAllUser();

                return StatusCode(200, info);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [Route("removeById/{id:int}")]
        [HttpGet]
        public IActionResult removeById(int id)
        {
            try
            {
                _unityOfWork.Usuario.removeUserById(id);

                return StatusCode(200, _unityOfWork.Complete());
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }



        [Route("CreateRol")]
        [HttpPost]
        public async Task<IActionResult> CreateRol([FromBody] RolesRegisterDTO rolesRegisterDTO)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(rolesRegisterDTO.Name);

                if (role == null) {
                    var x = new Rol { Name = rolesRegisterDTO.Name };

                    var result = await _roleManager.CreateAsync(x);

                    if (result.Succeeded) { return StatusCode(201, new { result = "Rol Created" }); }
                }

                return StatusCode(400, new { result = "This rol cannot be created!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

                var user = await _userManager.FindByEmailAsync(email.Value);

                var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.Password);

                if (!result.Succeeded) { return StatusCode(400, result.Errors.ToList()); }

                return StatusCode(204, new { result = "Password Changed" });
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UsuarioUpdateDTO usuario)
        {
            try
            {
                await _usuarioService.Update(usuario);

                return StatusCode(201, _unityOfWork.Complete());
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [Route("BuscadorUsuarios")]
        [HttpPost]
        public async Task<IActionResult> BuscadorUsuarios(BusquedaUserDTO data)
        {
            try
            {
                var result = await _usuarioService.BuscadorUsuarios(data.busqueda);

                return StatusCode(201, result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [Route("GetUserByUrl")]
        [HttpPost]
        public async Task<IActionResult> GetUserByUrl(UserFilterDTO data)
        {
            try
            {
                var result = await _usuarioService.GetUserByUrl(data.UserUrl);

                return StatusCode(201, result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [Route("UpdateImgProfile")]
        [HttpPost]
        public  async Task<IActionResult> UpdateImgProfile([FromForm] UserImgDTO data)
        {
            try
            {
                await _usuarioService.actualizarImgProfile(data);

                return StatusCode(201, _unityOfWork.Complete());
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }










    }
}
