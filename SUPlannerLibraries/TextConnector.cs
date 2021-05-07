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
            model.Typ = "Zahájeni";
            spisy.Add(model);
            spisy.SaveToSpisFile();

            //if (tournaments.Count > 0)
            //{
            //    currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            //}

            //model.Id = currentId;

            //model.SaveRoundsToFile();

            //tournaments.Add(model);

            //tournaments.SaveToTournamentFile();

            //TournamentLogic.UpdateTournamentResults(model);
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
