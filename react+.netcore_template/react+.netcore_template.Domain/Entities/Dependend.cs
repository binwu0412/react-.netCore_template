using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Domain.Entities
{
    public class Dependend
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
