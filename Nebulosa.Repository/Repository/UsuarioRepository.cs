using Microsoft.EntityFrameworkCore;
using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Repository.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario> , IUsuarioRepository
    {
        public NebulaDbContext _context { get { return context; } }

        public UsuarioRepository(NebulaDbContext context) : base(context)
        {
        }


  
        public dynamic GenerateUserUrl()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var random = new Random();
            //DateTime currentDate = new DateTime();
            //var date = currentDate.ToString();


            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;

        }

        public void removeUserById(int id)
        {
            var user = Get(id);

            if (user != null)
            {
                Remove(user);
            }
            
        }




        public dynamic getAllUser(string user)
        {
            return _context.Usuario.Where(u => u.UserName == user).Count();
        }
    }
}
