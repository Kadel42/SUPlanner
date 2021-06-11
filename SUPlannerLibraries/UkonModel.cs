using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    public class UkonModel
    {
        public int Id { get; set; }
        public int SpisId { get; set; }
        public string CisloJednaci { get; set; }
        public string Typ { get; set; }
        public DateTime Vydani { get; set; }
    }
}
