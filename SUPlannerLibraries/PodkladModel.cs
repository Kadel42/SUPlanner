using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    public class PodkladModel     // TODO - add property Cislo so user can chose his own number (right now datagrid shows id)
    {
        public int Id { get; set; }
        public int SpisId { get; set; }
        public string Podklad { get; set; }
        public DateTime DatumPridani { get; set; }
    }
}
