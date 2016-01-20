using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvasilva.Framework.Model
{
    public class Permission
    {

        public int Id { get; set; }

        
        public int MenuSort { get; set; }

        public string MenuName { get; set; }

        public Permission Parent { get; set; }
       
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }


        public string Action { get; set; }

        public string Controller { get; set; }

       
        public bool IsActive { get; set; }

        
        public bool IsMenu { get; set; }


        public Module Module { get; set; }

        public PermissionType Type { get; set; }

        public string ParametersLog { get; set; }

        public int Value { get; set; }

        private List<Permission> _children = new List<Permission>();

        public List<Permission> Children
        {
            get { return _children; }
            set { _children = value; }
        }


    }


    public class PermissionType
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

       
    }
}
