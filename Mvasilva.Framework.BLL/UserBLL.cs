using Mvasilva.Framework.DAO;
using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL
{
    public class UserBLL
    {
        public static User Login(User objUser)
        {
            return UserDAO.Login(objUser);
        }

    }
}
