using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Nebulosa.Services.AuthenticationJWT
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IHostEnvironment _hostEnvironment;


        public AuthenticationService
            (
             UserManager<Usuario> userManager,
             IHostEnvironment hostEnvironment
            )
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public string RandomString(int length)
        {
            RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider();
            byte[] tokenBuffer = new byte[length];
            cryptRNG.GetBytes(tokenBuffer);
            var password = Convert.ToBase64String(tokenBuffer);
            return password;
        }


        public async Task<dynamic> RegistrarUsuario(RegisterDTO registerDTO)
        {
            //string uniqueFileName = SaveImage(registerDTO);

            //var user = new Usuario
            //{
            //    Nombre = registerDTO.Nombre,
            //    Apellido = registerDTO.Apellido,
            //    IdSexo = registerDTO.IdSexo,
            //    ImgAvatar = uniqueFileName,
            //    UserName = registerDTO.Username,
            //    Email = registerDTO.Email,
            //    EmailConfirmed = true,
            //    FechaCreacion = DateTime.UtcNow.AddMinutes(-240)

            //};
            //registerDTO.Role = new List<string> { "1" };

            //var result = await _userManager.CreateAsync(user, registerDTO.Password);
            //// await _userManager.AddToRoleAsync(user, "ADMINISTRADOR");
            //await _userManager.AddToRoleAsync(user, "USUARIO");

            //if (!result.Succeeded) return new { errors = result.Errors.ToList() };

            return new { result = "Nuevo Usuario Registrado"  };

        }


        public string SaveImage(RegisterDTO registerDTO)
        {

            string uniqueFileName = null;

            if (registerDTO.ImgAvatar != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + registerDTO.ImgAvatar.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    registerDTO.ImgAvatar.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }



        public string SaveImagePost(PostDTO postDTO)
        {

            string uniqueFileName = null;

            if (postDTO.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images/post");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + postDTO.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    postDTO.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }


    }
}
