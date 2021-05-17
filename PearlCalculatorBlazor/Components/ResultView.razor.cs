using AntDesign;
using AntDesign.TableModels;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PearlCalculatorBlazor.Components
{
    public partial class ResultView
    {
        enum ShowResultMode
        {
            Amount, Trace
        }

        private ShowResultMode ShowMode { get; set; } = ShowResultMode.Amount;

        private int _pageIndex = 1;
        private int _pageSize = 50;

        private int _amountTotal => AmountResult is null ? 0 : AmountResult.Count;
        private int _pearlTotal => PearlTrace is null ? 0 : PearlTrace.Count;

        private List<TNTCalculationResult> AmountResult => Data.TNTResult;

        private List<EntityWrapper> PearlTrace { get; set; } = new List<EntityWrapper>();

        protected override void OnInitialized()
        {
            EventManager.Instance.AddListener<ButtonClickArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                ShowMode = ShowResultMode.Amount;
                StateHasChanged();
            });

            EventManager.Instance.AddListener<PearlSimulateArgs>("simulate", async (sender, args) =>
            {
                _pageIndex = 1;

                PearlTrace = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
                {
                    ref var p = ref args.Trace[index].Position;
                    return new EntityWrapper
                    {
                        XCoor = p.X,
                        YCoor = p.Y,
                        ZCoor = p.Z,
                        Tick = index
                    };
                }).ToList());

                ShowMode = ShowResultMode.Trace;
                StateHasChanged();
            });

            EventManager.Instance.AddListener<ButtonClickArgs>("resortResult", (sender, args) =>
            {
                if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

                AmountResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                StateHasChanged();
            });
        }

        private void OnAmountRowClick(RowData<TNTCalculationResult> res)
        {
            EventManager.Instance.PublishEvent(this, "tntAmountSetRTCount", new SetRTCountArgs("ResultView", res.Data.Red, res.Data.Blue));
        }

    }

    public class EntityWrapper
    {
        public double XCoor { get; set; }
        public double YCoor { get; set; }
        public double ZCoor { get; set; }
        public int Tick { get; set; }
    }
}
