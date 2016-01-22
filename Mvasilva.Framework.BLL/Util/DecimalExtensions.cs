using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL.Util
{
    public static class DecimalExtensions
    {
        public static string ElosFormat(this decimal _valor, int _tipoFormato)
        {
            string strFormat = "{0:N0}";

            switch (_tipoFormato)
            {
                case 1:
                    strFormat = "{0:N0}";
                    break;
                case 2:
                    strFormat = "{0:N2}";
                    break;
                case 3:
                    strFormat = "{0:P1}";
                    break;
            }



            return string.Format(strFormat, _valor);
        }


    }
}
