﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPlannerLibraries
{
    interface IDataConnector
    {
        void CreateSpis(SpisModel spis);
        void CreatePodklad(PodkladModel podklad);

        List<SpisModel> GetSpis_All();
        List<PodkladModel> GetPodklad_All();
    }
}
