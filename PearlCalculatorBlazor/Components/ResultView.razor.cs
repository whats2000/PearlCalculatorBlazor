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

        private Table<TNTCalculationResult> _amountTable;
        private Table<EntityWrapper> _pearlTable;
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

                _amountTable.DataSource = AmountResult;
                if (ShowMode != ShowResultMode.Amount) ShowMode = ShowResultMode.Amount;
                _amountTable.ReloadData();
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

                _pearlTable.DataSource = PearlTrace;
                if (ShowMode != ShowResultMode.Trace) ShowMode = ShowResultMode.Trace;
                _pearlTable.ReloadData();
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
