using Mvasilva.Framework.DAO;
using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL
{
    public class PermitionBLL
    {

        public static Permission SelectByUser(User objUser, Permission objPermition)
        {
            return PermitionDAO.SelectByUser(objUser, objPermition);
        }

    }
}
