using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.Model
{
    public class FileReport
    {
        public string FileName { get; set; }

        public MemoryStream MemoryStream { get; set; }

        public string ContentType { get; set; }
    }
}
