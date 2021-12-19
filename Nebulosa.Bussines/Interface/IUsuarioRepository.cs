using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IUsuarioRepository
    {
        dynamic GenerateUserUrl();

        void removeUserById(int id);
        dynamic getAllUser(string user);
    }
}
