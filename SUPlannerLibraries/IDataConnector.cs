using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    public interface IDataConnector
    {
        void CreateSpis(SpisModel spis);
        void CreatePodklad(PodkladModel podklad);
        void CreateUkon(UkonModel ukon);

        void CreateStatistika(StatistikaModel statistika);

        List<SpisModel> GetSpis_All();
        List<PodkladModel> GetPodklad_All();
    }
}
