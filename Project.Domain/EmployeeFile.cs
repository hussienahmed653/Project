using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    public class EmployeeFile
    {
        public int EmployeeID { get; set; }
        public Employee employee { get; set; }
        public string Path { get; set; }
    }
}
