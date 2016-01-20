using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.DAO
{
   public class ConfigDAO
    {

        public static int CommmandTimeout = 180;
        public static int CommmandTimeoutDTS = 1800000;

        public static SqlDatabase CreateDB()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();

            return factory.CreateDefault() as SqlDatabase;
        }

    }
}
