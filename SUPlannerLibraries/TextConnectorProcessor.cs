using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ GlobalConfig.filePath }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }
        public static List<PodkladModel> ConvertToPodkladModels(this List<string> lines)
        {
            List<PodkladModel> output = new List<PodkladModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                PodkladModel p = new PodkladModel();
                p.Id = int.Parse(cols[0]);

            }
            return output;
        }

        public static List<SpisModel> ConvertToSpisModels(this List<string> lines)
        {
            List<SpisModel> output = new List<SpisModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                SpisModel p = new SpisModel();
                p.Id = int.Parse(cols[0]);
                p.Cislo = int.Parse(cols[1]);
                p.Zadatel = cols[2];
                p.Vec = cols[3];
                p.DatumPridani = Convert.ToDateTime(cols[4]);
                p.LimitniDatum = Convert.ToDateTime(cols[5]);
                p.Typ = cols[6];
                p.Notes = cols[7];

                output.Add(p);
            }

            return output;
        }
    }
}
