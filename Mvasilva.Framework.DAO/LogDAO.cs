using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Mvasilva.Framework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.DAO
{
   public class LogDAO
    {

       public static void Insert(User objUser, Permission objPermition)
       {
           SqlDatabase db = ConfigDAO.CreateDB();


           using (DbConnection connection = db.CreateConnection())
           {
               
               try
               {
                   connection.Open();
                   DbCommand dbCommand = db.GetStoredProcCommand("PR_GER_ELOSLog_INS");

                   db.AddInParameter(dbCommand, "@cod_Usuario", SqlDbType.Int, objUser.Id);
                   db.AddInParameter(dbCommand, "@cod_Modulo", SqlDbType.Int, objPermition.Module.Id);
                   db.AddInParameter(dbCommand, "@nom_Action", SqlDbType.VarChar, objPermition.Action);
                   db.AddInParameter(dbCommand, "@nom_Controller", SqlDbType.VarChar, objPermition.Controller);
                   db.AddInParameter(dbCommand, "@nom_ParametroLog", SqlDbType.VarChar, objPermition.ParametersLog);


                   dbCommand.CommandTimeout = ConfigDAO.CommmandTimeout;

                   db.ExecuteNonQuery(dbCommand);
               }
               catch { }
               finally
               {
                   connection.Close();
               }
           }

       }
    }
}
