﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    class TextConnector : IDataConnector
    {
        

        public void CreateSpis(SpisModel model)
        {
            List<SpisModel> spisy = GlobalConfig.spisFile.FullFilePath().LoadFile().ConvertToSpisModels();
            int currentId = 1;

            if (spisy.Count > 0)
            {
                currentId = spisy.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            model.Notes = "";
            
            spisy.Add(model);
            spisy.SaveToSpisFile();

            
        }

        public void CreatePodklad(PodkladModel model)
        {
            List<PodkladModel> podklady = GlobalConfig.podkladFile.FullFilePath().LoadFileAll().ConvertToPodkladModels();
            int currentId = 1;

            if (podklady.Count > 0)
            {
                currentId = podklady.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            podklady.Add(model);
            podklady.SaveToPodkladFile();
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
