using Microsoft.AspNetCore.Identity;
using Nebulosa.Bussines.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Nebulosa.Entities.Entities;
using Nebulosa.Entities.DTO;
using System.Threading.Tasks;
using System.Linq;
using Nebulosa.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Nebulosa.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        public static IHostEnvironment _hostEnvironment;
        private readonly IUnity _unitOfWork;
        public NebulaDbContext _context;
        public static IValidateImgUserService _validateImgUser;
        public static IPostService _postService;

        public UsuarioService(
            IPostService postService,
            IHostEnvironment hostEnvironment,
            NebulaDbContext context, 
            UserManager<Usuario> userManager, 
            RoleManager<Rol> roleManager,
             IValidateImgUserService validateImgUser,
            IUnity unitOfWork) 
        {
            _postService = postService;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _context = context;
            _validateImgUser = validateImgUser;
        }

        public async Task<dynamic> GetUser(int idUsuario)
        {
            var roles = new List<int>();

            var user = await _userManager.FindByIdAsync(idUsuario.ToString());

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                roles.Add(role.Id);
            }

            var info = new
            {
                user.Id,
                user.UserName,
                user.Nombre,
                user.Apellido,
                user.Email,
                user.EmailConfirmed,
                Role = roles,
            };

            return info;
        }


        public async Task<dynamic> getAllUser()
        {
            return await _context.Usuario.Select(u => new
            {
                u.Id,
                u.Nombre,
                u.Apellido,
                u.UserName,
                u.Email,
                Sexo = u.Sexo.Nombre
            }).ToListAsync();
        }



        public async Task<dynamic> actualizarImgProfile(UserImgDTO imgUser)
        {

            //buscamos primero el  usuario
            var user = await _userManager.FindByIdAsync(imgUser.IdUsuario.ToString());

            //buscamos la imagen y la eliminamos
            string path = UsuarioService._hostEnvironment.ContentRootPath + "/images/" + user.ImgAvatar;
            if (path != null)
            {
                File.Delete(path);
            }

            var newImgProfile = setImg(imgUser);

            user.ImgAvatar = newImgProfile;

            await _userManager.UpdateSecurityStampAsync(user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) { await Task.FromResult(result.Errors.ToList()); }
            return 1;

        }


        public  dynamic setImg(UserImgDTO imgUser)
        {
            string uniqueFileName = null;

            if (imgUser.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imgUser.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imgUser.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }





        public Task<dynamic> Me()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> Update(UsuarioUpdateDTO usuario)
        {
            //Verificar si el nuevo nombre de usuario existe en la DB
            var user = await _userManager.FindByNameAsync(usuario.UserName);

            var userUpdate = await _userManager.FindByEmailAsync(usuario.Email);
           // var count = _unitOfWork.Usuario.getAllUser(userUpdate.UserName);


            if (user != null) {
                //verificamos los Id
                if (user.Id != usuario.Id)
                {
                    return new { error = "Este UserName ya existe" };
                }
            };

            userUpdate.UserName = usuario.UserName;
            userUpdate.Nombre = usuario.Nombre;
            userUpdate.Apellido = usuario.Apellido;
            userUpdate.IdSexo = usuario.IdSexo;
            userUpdate.FechaModiificacion = DateTime.UtcNow.AddMinutes(-240);

            await _userManager.UpdateSecurityStampAsync(userUpdate);

            var result = await _userManager.UpdateAsync(userUpdate);

            if (!result.Succeeded) { await Task .FromResult(result.Errors.ToList()); }

            return 1;

        }


        public async Task<dynamic> BuscadorUsuarios(string busqueda)
        {
            return await _context.Usuario.Where(
              u => u.Nombre.StartsWith(busqueda)
             || u.Apellido.StartsWith(busqueda) ||
              u.UserName.StartsWith(busqueda)).Select(user => new
              {
                  user.Id,
                  NombreCompleto = user.Nombre + " " + user.Apellido,
                  user.UserName,
                  user.UserUrl,
                  ImgAvatar = UsuarioService.validateImg(user.ImgAvatar, user)
              }).ToListAsync();
        }



        public async Task<dynamic> GetUserByUrl(string url)
        {

            return await _context.Usuario.Where(u => u.UserUrl == url).Select(u => new
            {

                u.Id,
                u.Nombre,
                u.UserName,
                u.Apellido,
                ImgAvatar = UsuarioService.validateImg(u.ImgAvatar , u),
                ComentariosCount = u.Comentario.Count(p => p.IdUsuario == u.Id),
                PostCount = u.Post.Count(p => p.IdUsuario == u.Id),
                LikeTotal = u.Post.Select(p => p.Likes.Count())
            }).ToListAsync();

        }


        public static dynamic validateImg(string ImgAvatar, Usuario user)
        {


            if (ImgAvatar != "null" || ImgAvatar != null)
            {
                var validate = UsuarioService._validateImgUser.ValidateImgAvatar(ImgAvatar);

                if (validate == true)
                {
                    return "assets/avatar/" + ImgAvatar;
                }
                else
                {
                    string path = UsuarioService._hostEnvironment.ContentRootPath + "/images/" + ImgAvatar;

                    byte[] b = File.ReadAllBytes(path);

                    var imagen = "data:image/png;base64," + Convert.ToBase64String(b);
                    return imagen;
                }
            }

            return null;
     
        }


    }
}
