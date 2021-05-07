using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    class PodkladModel
    {
        public int Id { get; set; }
        public int SpisId { get; set; }
        public string Podklad { get; set; }
        public DateTime DatumPridani { get; set; }
    }
}
