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

        public static void SaveToSpisFile(this List<SpisModel> models)
        {
            List<string> lines = new();
            foreach (SpisModel p in models)
            {
                lines.Add($"{ p.Id }&^&{ p.SpisZn }&^&{ p.Cislo }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumPridani }&^&{ p.LimitniDatum }&^&{ p.Typ }&^&{ p.Notes }");
            }


            File.WriteAllLines(GlobalConfig.spisFile.FullFilePath(), lines);
        }

        public static void RemoveSpisFromFile(this List<SpisModel> models, int iD)
        {
            List<string> lines = new();
            foreach(SpisModel p in models)
            {
                if (p.Id == iD)
                {
                    continue;
                }
                else
                {
                    lines.Add($"{ p.Id }&^&{ p.SpisZn }&^&{ p.Cislo }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumPridani }&^&{ p.LimitniDatum }&^&{ p.Typ }&^&{ p.Notes }");
                }
            }

            File.WriteAllLines(GlobalConfig.spisFile.FullFilePath(), lines);
        }

        public static List<PodkladModel> ConvertToPodkladModels(this List<string> lines)
        {
            List<PodkladModel> output = new();
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                PodkladModel p = new();
                p.Id = int.Parse(cols[0]);
                p.SpisId = int.Parse(cols[1]);
                p.Podklad = cols[2];
                p.DatumPridani = DateTime.Parse(cols[3]);

                output.Add(p);

            }
            return output;
        }

        public static List<SpisModel> ConvertToSpisModels(this List<string> lines)
        {
            List<SpisModel> output = new();
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                SpisModel p = new();
                p.Id = int.Parse(cols[0]);
                p.SpisZn = cols[1];
                p.Cislo = int.Parse(cols[2]);
                p.Zadatel = cols[3];
                p.Vec = cols[4];
                p.DatumPridani = Convert.ToDateTime(cols[5]);
                p.LimitniDatum = Convert.ToDateTime(cols[6]);
                p.Typ = cols[7];
                p.Notes = cols[8];

                output.Add(p);
            }

            return output;
        }
    }
}
