using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Advanced
    {
        private double PearlOffSetX 
        {
            get => Data.PearlOffset.X;
            set => Data.PearlOffset.X = value;
        }

        private double PearlOffSetZ
        {
            get => Data.PearlOffset.Z;
            set => Data.PearlOffset.Z = value;
        }

        private double TNTWeight
        {
            get => Data.TNTWeight;
            set => Data.TNTWeight = (int)value;
        }

        private void CalculateTNTAmount()
        {
            Data.TNTResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
            EventManager.Instance.PublishEvent(this, "calculate", new ButtonClickArgs("GFTL_General"));
        }
    }
}
