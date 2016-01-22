using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL.Util
{
    public static class StringExtensions
    {

        public static string RemoveSpecialCharactersFileName(this string str)
        {

            string strRetorno = new string(str.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

            strRetorno = Regex.Replace(strRetorno, @"[@#$%¨&*.;:,/=²³£¢¬§ªº°|\\<>!?]", string.Empty, RegexOptions.Compiled);

            return strRetorno;
        }

        public static string ToFileName(this string str)
        {
            string strRetorno = str.RemoveSpecialCharactersFileName();

            strRetorno = strRetorno.Substring(0, strRetorno.Length > 30 ? 29 : strRetorno.Length);

            return strRetorno;
        }

        public static List<int> ConvertSelectedStrToList(this string str)
        {
            return string.IsNullOrEmpty(str) ? new List<int>() : str.Split(',').Select(Int32.Parse).ToList();
        }


        public static string RemoveSpecialCharactersSMS(this string str)
        {

            string strRetorno = new string(str.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

            strRetorno = Regex.Replace(strRetorno, @"[¨²³£¢¬§ªº°]", string.Empty, RegexOptions.Compiled);

            return strRetorno;
        }

    }
}
