using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    public static class GlobalConfig
    {
        public const string podkladFile = "Podklady.csv";
        public const string spisFile = "Spisy.csv";
        public const string filePath = ".\\saveFiles";

        public static IDataConnector Connection { get; private set; }

        public static void InitializeConnections()
        {
            TextConnector text = new TextConnector();
            Connection = text;
        }

    }
}
