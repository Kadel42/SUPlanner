using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    class TextConnector : IDataConnector
    {
        public void CreatePodklad(PodkladModel podklad)
        {
            throw new NotImplementedException();
        }

        public void CreateSpis(SpisModel spis)
        {
            throw new NotImplementedException();
        }

        public List<PodkladModel> GetPodklad_All()
        {
            return GlobalConfig.podkladFile.FullFilePath().LoadFile().ConvertToPodkladModels();
        }

        public List<SpisModel> GetSpis_All()
        {
            return GlobalConfig.spisFile.FullFilePath().LoadFile().ConvertToSpisModels();
        }
    }
}
