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
            Amount, Trace, Momentum
        }

        private ShowResultMode ShowMode { get; set; } = ShowResultMode.Amount;

        private int _pageIndex = 1;
        private int _pageSize = 50;

        private int AmountTotal => AmountResult is null ? 0 : AmountResult.Count;
        private int PearlTotal => PearlTrace is null ? 0 : PearlTrace.Count;
        private int MotionTotal => PearlMotion is null ? 0 : PearlMotion.Count;


        private List<TNTCalculationResult> AmountResult { get; set; } = new();

        private List<EntityWrapper> PearlTrace { get; set; } = new();
        private List<EntityWrapper> PearlMotion { get; set; } = new();



        protected override void OnInitialized()
        {
            EventManager.Instance.AddListener<ButtonClickArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                AmountResult = Data.TNTResult;

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
                AmountResult = Data.TNTResult;

                if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

                AmountResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                StateHasChanged();
            });

            EventManager.Instance.AddListener<CalculateTNTAmuontArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                AmountResult = args.Results;

                if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

                AmountResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                StateHasChanged();
            });

            EventManager.Instance.AddListener<PearlSimulateArgs>("trace", async (sender, args) =>
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

            EventManager.Instance.AddListener<PearlSimulateArgs>("motion", async (sender, args) =>
            {
                _pageIndex = 1;

                PearlTrace = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
                {
                    ref var p = ref args.Trace[index].Motion;
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
