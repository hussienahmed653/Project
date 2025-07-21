using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    public class CategoryFile
    {
        public int CategoryID { get; set; }
        public Categories categories { get; set; }
        public string Path { get; set; }
    }
}
