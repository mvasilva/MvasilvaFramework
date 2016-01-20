using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.Model
{
    public class Module
    {

        private List<Permission> _permitions = new List<Permission>();

        public List<Permission> Permitions
        {
            get { return _permitions; }
            set { _permitions = value; }
        }


        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public bool IsActive { get; set; }

    }
}
