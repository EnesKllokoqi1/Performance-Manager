using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Manager
{
    internal class Employeess
    {
        public int Id { get; set; }
        public char Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return $"Id({Id}) Gender({Gender}) Firstname({FirstName}) Lastname({LastName}) Salary({Salary})";

        }
    }
}
