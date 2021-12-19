using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthenticateService _authService;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUsuarioService _usuarioService;
        private readonly RoleManager<Rol> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ImgConfigDTO imgConfig = new ImgConfigDTO();
        private readonly IUnity _unitOfWork;



        public AuthenticationController
            (

            IUnity unitOfWork,
            IHostingEnvironment hostingEnvironment,
               RoleManager<Rol> roleManager,
               IAuthenticateService authService,
               UserManager<Usuario> userManager,
               IUsuarioService usuarioService,
               SignInManager<Usuario> signInManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _usuarioService = usuarioService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            //verificando usuario, aver si existe.
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user == null) { return StatusCode(501, new { result = "Este usuario no existe" }); }

            //Verificando, contrase;a.
            var result = await _signInManager.PasswordSignInAsync(user, userDTO.Password, isPersistent: false, lockoutOnFailure: true);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretSECRETITOxd"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var userData = new
            {

            };


            var utcNow = DateTime.UtcNow.AddMinutes(-240);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };


            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                var rolesClaims = new List<Claim>
                {
                    new Claim("RoleName", userRole),
                };

                claims.AddRange(rolesClaims);

            }



            if (result.Succeeded)
            {

                var jwt = new JwtSecurityToken(
                  signingCredentials: signingCredentials,
                  claims: claims,
                  notBefore: utcNow,
                  expires: DateTime.Now.AddHours(12),
                  issuer: "https://localhost:4200",
                  audience: "https://localhost:4200"
                  );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
                HttpContext.Session.SetString("Token", tokenString);
                HttpContext.Response.Headers.Add("Authorization", tokenString);

                return StatusCode(201, new { Token = tokenString });
            }

            return StatusCode(400, new { result = "Error, tu contraseña o usuario es invalido" });
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterDTO registerDTO)
        {
            try
            {

                var finduser = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (finduser != null) { return StatusCode(400, String.Format("Este correo ya esta en uso")); }


                string uniqueFileName;


                if (string.IsNullOrEmpty(registerDTO.ImgSpace))
                {
                    uniqueFileName = _authService.SaveImage(registerDTO);
                }
                else
                {
                    uniqueFileName = registerDTO.ImgSpace;
                }

                var userUrl = _unitOfWork.Usuario.GenerateUserUrl();
              

                var user = new Usuario
                {
                    Nombre = registerDTO.Nombre,
                    Apellido = registerDTO.Apellido,
                    UserUrl = userUrl,
                    IdSexo = registerDTO.IdSexo,
                    ImgAvatar = uniqueFileName,
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                    EmailConfirmed = true,
                    FechaCreacion = DateTime.UtcNow.AddMinutes(-240)

                };
                registerDTO.Role = new List<string> { "1" };

                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                // await _userManager.AddToRoleAsync(user, "ADMINISTRADOR");

                switch (user.Email)
                {
                    case "leo2@gmail.com":
                    case "leo3@gmail.com":
                    case "edelyn@gmail.com":
                    case "emely@gmail.com":
                    case "erick@gmail.com":
                    case "ericka@gmail.com":
                    case "diego@gmail.com":
                    case "dalton@gmail.com":
                    case "leiram@gmail.com":
                    case "lesley@gmail.com":
                    case "edwin@gmail.com":
                        await _userManager.AddToRoleAsync(user, "USUARIO");
                        await _userManager.AddToRoleAsync(user, "ADMINISTRADOR");
                        break;

                    default:
                        await _userManager.AddToRoleAsync(user, "USUARIO");
                      break;
                }


                if (!result.Succeeded) { return StatusCode(400, result.Errors.ToList()); }


                return StatusCode(201, new { result = "Nuevo Usuario Registrado" });
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }



        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("Token");
                return StatusCode(200, new { result = "Succesfully LogOut", status = true });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message, status = false });
            }

        }



        [Route("Me")]
        [HttpGet]
        public async Task<IActionResult> Me()
        {

            try
            {


                var roles = new List<RolDTO>();
                if (!User.Identity.IsAuthenticated) { return StatusCode(400, new { result = "User is not authenticated" }); }

                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var userRole in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(userRole);

                    roles.Add(new RolDTO { Id = role.Id, Nombre = role.Name });
                }

                string imgAvatar;

                switch (user.ImgAvatar)
                {
                    case "profile-0.jpg":
                    case "profile-1.jpg":
                    case "profile-2.jpg":
                    case "profile-3.jpg":
                    case "profile-4.jpg":
                    case "profile-5.jpg":
                    case "profile-6.jpg":
                    case "profile-7.jpg":
                        imgConfig.imgUserDefault = user.ImgAvatar;
                    break;

                    default:
                        string path = _hostingEnvironment.ContentRootPath + "/images/" + user.ImgAvatar;
                        imgConfig.imgUserPersonalizado = System.IO.File.ReadAllBytes(path);
                     break;

                }

                dynamic imgFormated;
                bool fotoDefault = false;

                //revisamos si el usuario tiene una imagen personalizada.
                if (imgConfig.imgUserPersonalizado == null)
                {
                    imgFormated = imgConfig.imgUserDefault;
                    fotoDefault = true;
                }
                else
                {
                    imgFormated = "data:image/png;base64," + Convert.ToBase64String(imgConfig.imgUserPersonalizado);
                    fotoDefault = false;
                }

                //var ComentariosCount =  user.Comentario.Count();
               // var PostCount = user.Post.Count();

                var info = new
                {
                    user.Id,
                    user.UserName,
                    user.Nombre,
                    user.Apellido,
                    user.Email,
                    user.IdSexo,
                   // ComentariosCount = user.Comentario.Count(c => c.IdUsuario == user.Id),
                    ImageDefault = fotoDefault,
                    ImgAvatar = imgFormated,
                    Roles = roles.OrderBy(rol => rol.Nombre).ToList()
                };

                return StatusCode(200, info);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
