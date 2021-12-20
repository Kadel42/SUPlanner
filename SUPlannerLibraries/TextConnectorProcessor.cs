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

        //public static List<string> LoadFile(this string file)
        //{
        //    if (!File.Exists(file))
        //    {
        //        return new List<string>();
        //    }
            
        //    return File.ReadAllLines(file).ToList();
        //}

        public static List<string> LoadFileAll(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            List<string> allText = File.ReadAllText(file).Split("&|&").ToList();
            return allText;

        }

        public static void SaveToSpisFile(this List<SpisModel> models)
        {
            string spisLine = "";
            foreach (SpisModel p in models)
            {
                spisLine += ($"{ p.Id }&^&{ p.SpisZn }&^&{ p.Cislo }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumPridani }&^&{ p.LimitniDatum }&^&{ p.Typ }&^&{ p.Notes }&|&");
            }


            File.WriteAllText(GlobalConfig.spisFile.FullFilePath(), spisLine);
        }

        public static void SaveToPodkladFile(this List<PodkladModel> models)
        {
            string podklad = "";
            foreach (PodkladModel p in models)
            {
                podklad += ($"{ p.Id }&^&{ p.Cislo }&^&{ p.SpisId }&^&{ p.Podklad }&^&{ p.DatumPridani }&|&");
            }
            
            File.WriteAllText(GlobalConfig.podkladFile.FullFilePath(), podklad);
        }

        public static void SaveToStatFile(this List<StatistikaModel> models)
        {
            string statLine = "";
            foreach (StatistikaModel p in models)
            {
                statLine += ($"{ p.Id }&^&{ p.SpisZn }&^&{ p.CisloJednaci }&^&{ p.Typ }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumVydani }&|&");
            }

            File.WriteAllText(GlobalConfig.statistikaFile.FullFilePath(), statLine);
        }

        public static void SaveToUkonFile(this List<UkonModel> models)
        {
            string ukony = "";
            foreach (UkonModel p in models)
            {
                ukony += ($"{ p.Id }&^&{ p.SpisId }&^&{ p.CisloJednaci }&^&{ p.Typ }&^&{ p.Vydani }&|&");
            }

            File.WriteAllText(GlobalConfig.ukonFile.FullFilePath(), ukony);
        }

        public static void RemoveSpisFromFile(this List<SpisModel> models, int iD)
        {
            string spisLine = "";
            foreach(SpisModel p in models)
            {
                if (p.Id == iD)
                {
                    continue;
                }
                else
                {
                    spisLine += ($"{ p.Id }&^&{ p.SpisZn }&^&{ p.Cislo }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumPridani }&^&{ p.LimitniDatum }&^&{ p.Typ }&^&{ p.Notes }&|&");
                }
            }

            File.WriteAllText(GlobalConfig.spisFile.FullFilePath(), spisLine);
        }

        public static void RemovePodkladBySpisId (this List<PodkladModel> models, int spisId)
        {
            string podklad = "";
            foreach (PodkladModel p in models)
            {
                if (p.SpisId == spisId)
                {
                    continue;
                }
                else
                {
                    podklad += ($"{ p.Id }&^&{ p.Cislo }&^&{ p.SpisId }&^&{ p.Podklad }&^&{ p.DatumPridani }&|&");
                }

                

            }
            File.WriteAllText(GlobalConfig.podkladFile.FullFilePath(), podklad);
        }

        public static void RemovePodkladFromFile(this List<PodkladModel> models, int iD)
        {
            string podklad = "";
            foreach (PodkladModel p in models)
            {
                if (p.Id == iD)
                {
                    continue;
                }
                else
                {
                    podklad += ($"{ p.Id }&^&{ p.Cislo }&^&{ p.SpisId }&^&{ p.Podklad }&^&{ p.DatumPridani }&|&");
                }

                

            }
            File.WriteAllText(GlobalConfig.podkladFile.FullFilePath(), podklad);
        }

        public static void RemoveUkonFromFile(this List<UkonModel> models, int iD)
        {
            string ukon = "";
            foreach (UkonModel p in models)
            {
                if (p.Id == iD)
                {
                    continue;
                }
                else
                {
                    ukon += ($"{ p.Id }&^&{ p.SpisId }&^&{ p.CisloJednaci }&^&{ p.Typ }&^&{ p.Vydani }&|&");
                }
            }
            File.WriteAllText(GlobalConfig.ukonFile.FullFilePath(), ukon);
        }

        public static void RemoveStatistikaFromFile(this List<StatistikaModel> models, int iD)
        {
            string stat = "";
            foreach (StatistikaModel p in models)
            {
                if (p.Id == iD)
                {
                    continue;
                }
                else
                {
                    stat += ($"{ p.Id }&^&{ p.SpisZn }&^&{ p.CisloJednaci }&^&{ p.Typ }&^&{ p.Zadatel }&^&{ p.Vec }&^&{ p.DatumVydani }&|&");
                }
            }
            File.WriteAllText(GlobalConfig.statistikaFile.FullFilePath(), stat);
        }

        public static List<PodkladModel> ConvertToPodkladModels(this List<string> lines)
        {
            List<PodkladModel> output = new();
            if (lines.Count > 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
            
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                PodkladModel p = new();
                p.Id = int.Parse(cols[0]);
                p.Cislo = int.Parse(cols[1]);
                p.SpisId = int.Parse(cols[2]);
                p.Podklad = cols[3];
                p.DatumPridani = DateTime.Parse(cols[4]);

                output.Add(p);

            }
            return output;
        }

        public static List<SpisModel> ConvertToSpisModels(this List<string> lines)
        {
            List<SpisModel> output = new();
            if (lines.Count > 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
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
                if (DateTime.Compare(p.LimitniDatum, DateTime.Today) <= 0)
                {
                    p.IsLate = -1;
                }
                else if (DateTime.Compare(p.LimitniDatum, DateTime.Today.AddDays(5)) <= 0)
                {
                    p.IsLate = 0;
                }
                else
                {
                    p.IsLate = 1;
                }
                

                output.Add(p);
            }

            return output;
        }

        public static List<StatistikaModel> ConvertToStatModels(this List<string> lines)
        {
            List<StatistikaModel> output = new();
            if (lines.Count > 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                StatistikaModel p = new();
                p.Id = int.Parse(cols[0]);
                p.SpisZn = cols[1];
                p.CisloJednaci = cols[2];
                p.Typ = cols[3];
                p.Zadatel = cols[4];
                p.Vec = cols[5];
                p.DatumVydani = Convert.ToDateTime(cols[6]);
             

                output.Add(p);
            }

            return output;
        }

        public static List<UkonModel> ConvertToUkonModels(this List<string> lines)
        {
            List<UkonModel> output = new();
            if (lines.Count > 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
            foreach (string line in lines)
            {
                string[] cols = line.Split("&^&");

                UkonModel p = new();
                p.Id = int.Parse(cols[0]);
                p.SpisId = int.Parse(cols[1]);
                p.CisloJednaci = cols[2];
                p.Typ = cols[3];
                p.Vydani = Convert.ToDateTime(cols[4]);


                output.Add(p);
            }

            return output;
        }
    }
}
