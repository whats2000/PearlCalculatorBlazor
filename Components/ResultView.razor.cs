using AntDesign.TableModels;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using System;
using System.Collections.Generic;
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

        private static string ResultDirection = string.Empty;

        private static string ResultAngle = string.Empty;

        public static void ShowDirectionResult(Space3D pearlPos, Space3D destination)
        {
            var angle = pearlPos.WorldAngle(destination);
            if (angle == 370)
                return;
            ResultDirection = pearlPos.Direction(angle).ToString();
            ResultAngle = angle.ToString();
        }

        private static double AccurateWorldAngle(Space3D position1, Space3D position2)
        {
            Space3D distance = new Space3D();
            distance.X = position2.X - position1.X;
            distance.Z = position2.Z - position1.Z;
            double phi;
            if (distance.X == 0 && distance.Z == 0)
                return 370;
            phi = Math.Atan(Math.Abs(distance.X / distance.Z));
            if (distance.X > 0)
            {
                if (distance.Z > 0)
                    return -(Math.Atan(distance.X / distance.Z) * 180 / Math.PI);
                else if (distance.Z == 0)
                    return -90;
                else
                    return -(180 - Math.Atan(-distance.X / distance.Z) * 180 / Math.PI);
            }
            else if (distance.X < 0)
            {
                if (distance.Z > 0)
                    return Math.Atan(-distance.X / distance.Z) * 180 / Math.PI;
                else if (distance.Z == 0)
                    return 90;
                else
                    return 180 - Math.Atan(distance.X / distance.Z) * 180 / Math.PI;
            }
            else
            {
                if (distance.Z > 0)
                    return 0;
                else
                    return 180;
            }
            throw new ArgumentOutOfRangeException();
        }

        private static void AccurateDirectionResult(Space3D pearlPos, Space3D destination)
        {
            var angle = AccurateWorldAngle(pearlPos, destination);
            if (angle == 370)
                return;
            ResultDirection = pearlPos.Direction(angle).ToString();
            ResultAngle = angle.ToString();
        }

        protected override void OnInitialized()
        {
            EventManager.Instance.AddListener<ButtonClickArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                AmountResult = Data.TNTResult;

                ShowMode = ShowResultMode.Amount;

                ShowDirectionResult(Data.Pearl.Position, Data.Destination);

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

                AccurateDirectionResult(Data.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));
                
                ShowMode = ShowResultMode.Trace;
                
                StateHasChanged();
            });

            EventManager.Instance.AddListener<ButtonClickArgs>("resortResult", (sender, args) =>
            {
                if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

                AmountResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));

                ShowDirectionResult(PearlCalculatorLib.Manually.Data.Pearl.Position, PearlCalculatorLib.Manually.Data.Destination.ToSpace3D());
                
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

                AccurateDirectionResult(PearlCalculatorLib.Manually.Data.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

                StateHasChanged();
            });

            EventManager.Instance.AddListener<PearlSimulateArgs>("motion", async (sender, args) =>
            {
                _pageIndex = 1;

                PearlMotion = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
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

                ShowMode = ShowResultMode.Momentum;

                AccurateDirectionResult(PearlCalculatorLib.Manually.Data.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

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
