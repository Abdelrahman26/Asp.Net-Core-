using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.UserManager
{
    public interface ICustomUserManager
    {
        public List<CustomeUser> UserRefreshToken();
    }
}
