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
    public class PermitionDAO
    {


        public static Permission SelectByUser(User objUser, Permission objPermition)
        {
            SqlDatabase db = ConfigDAO.CreateDB();
            IDataReader dr = null;
            using (DbConnection connection = db.CreateConnection())
            {
               
                try
                {
                    connection.Open();
                    DbCommand dbCommand = db.GetStoredProcCommand("PR_GER_Permissao_SEL_ByUsuario");
                    dbCommand.CommandTimeout = ConfigDAO.CommmandTimeout;
                    db.AddInParameter(dbCommand, "@cod_Usuario", SqlDbType.Int, objUser.Id);
                    db.AddInParameter(dbCommand, "@nom_Action", SqlDbType.VarChar, objPermition.Action);
                    db.AddInParameter(dbCommand, "@nom_Controller", SqlDbType.VarChar, objPermition.Controller);
                    db.AddInParameter(dbCommand, "@cod_Modulo", SqlDbType.Int, objPermition.Module.Id);
                    dr = db.ExecuteReader(dbCommand);
                    if (dr.Read())
                    {
                        objPermition.Value = dr.GetInt32(0);
                        objPermition.Module = new Module { Id = dr.GetInt32(1) };
                        objPermition.Name = Convert.IsDBNull(dr[2]) ? string.Empty : dr.GetString(2);
                        objPermition.Description = Convert.IsDBNull(dr[3]) ? string.Empty : dr.GetString(3);
                    }
                }
                catch
                {
                    objPermition.Value = 0;
                    objPermition.Module = new Module { Id = 0 };
                }
                finally
                {
                    if (dr != null && !dr.IsClosed)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                    connection.Close();
                }
            }
            return objPermition;
        }

    }
}
