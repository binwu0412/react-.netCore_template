using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            Dependends = new List<Dependend>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }


        public List<Dependend> Dependends { get; set; }
    }
}
