using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.Util
{
    public static class DataTableExtension
    {

        public static string ExportToFile(this DataTable _table, string _folder, string _fileName)
        {

            string strFileFullPath = Path.Combine(_folder, string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, _fileName));

            if (File.Exists(strFileFullPath))
            {
                File.Delete(strFileFullPath);
            }

            using (TextWriter sw = File.CreateText(strFileFullPath))
            {

                sw.WriteLine(string.Join("|", (from c in _table.Columns.Cast<DataColumn>() select c.ColumnName)));


                foreach (DataRow row in _table.Rows)
                {
                    sw.WriteLine(string.Join("|", row.ItemArray));
                }

            }



            return strFileFullPath;
        }
    }
}
