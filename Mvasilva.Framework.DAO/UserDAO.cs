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
   public class UserDAO
    {

       public static User Login(User objUser)
       {
           SqlDatabase db = ConfigDAO.CreateDB();
           User objReturn = null;
           using (DbConnection connection = db.CreateConnection())
           {
               connection.Open();
               try
               {
                   DbCommand dbCommand = db.GetStoredProcCommand("PR_GER_UsuarioLogin_SEL");
                   dbCommand.CommandTimeout = ConfigDAO.CommmandTimeout;
                   db.AddInParameter(dbCommand, "@nom_Email", DbType.String, objUser.Email);
                   db.AddInParameter(dbCommand, "@nom_Senha", DbType.String, objUser.Password);
                   using (DataSet dsRetorno = db.ExecuteDataSet(dbCommand))
                   {
                       objReturn = FromDataRow(dsRetorno.Tables[0].Rows[0]);
                   }
               }
               catch
               {
                   objReturn.Id = -1;
               }
               finally
               {
                   connection.Close();
               }
           }
           return objReturn;
       }


       private static User FromDataRow(DataRow dr)
       {
           User objUser = new User();

           objUser.Id = Convert.ToInt32(dr["cod_Usuario"]);

           objUser.Email = dr["nom_Email"].ToString();
           objUser.Name = dr["nom_Usuario"].ToString();

           return objUser;
       }



    }
}
