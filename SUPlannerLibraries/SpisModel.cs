using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    class SpisModel
    {
        public int Id { get; set; }
        public string Zadatel { get; set; }
        public string Vec { get; set; }
        public DateTime DatumPridani { get; set; }
        public DateTime LimitniDatum { get; set; }
        public string Typ { get; set; }
        public string Notes { get; set; }
        
    }
}
