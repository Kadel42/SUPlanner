using System;
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
            List<SpisModel> spisy = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            int currentId = 1;

            if (spisy.Count > 0)
            {
                currentId = spisy.OrderByDescending(x => x.Id).First().Id + 1;
            }
            if (model.Id == 0)
            {
                model.Id = currentId;
            }
            if (DateTime.Compare(model.LimitniDatum, DateTime.Today) <= 0)
            {
                model.IsLate = -1;
            }
            else if (DateTime.Compare(model.LimitniDatum, DateTime.Today.AddDays(5)) <= 0)
            {
                model.IsLate = 0;
            }
            else
            {
                model.IsLate = 1;
            }
            
            
            if (model.Notes == null)
            {
                model.Notes = "";
            }
            
            
            spisy.Add(model);
            spisy.SaveToSpisFile();

            
        }

        public void CreateStatistika(StatistikaModel model)
        {
            List<StatistikaModel> statistikas = GlobalConfig.statistikaFile.FullFilePath().LoadFileAll().ConvertToStatModels();
            int currentId = 1;

            if (statistikas.Count > 0)
            {
                currentId = statistikas.OrderByDescending(x => x.Id).First().Id + 1;
            }
            if (model.Id == 0)
            {
                model.Id = currentId;
            }

            statistikas.Add(model);
            statistikas.SaveToStatFile();

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
            return GlobalConfig.podkladFile.FullFilePath().LoadFileAll().ConvertToPodkladModels();
        }

        public List<SpisModel> GetSpis_All()
        {
            return GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
        }

        public void CreateUkon(UkonModel ukon)
        {
            List<UkonModel> ukony = GlobalConfig.ukonFile.FullFilePath().LoadFileAll().ConvertToUkonModels();
            int currentId = 1;

            if (ukony.Count > 0)
            {
                currentId = ukony.OrderByDescending(x => x.Id).First().Id + 1;
            }
            if (ukon.Id == 0)
            {
                ukon.Id = currentId;
            }

            ukony.Add(ukon);
            ukony.SaveToUkonFile();
        }
    }
}
