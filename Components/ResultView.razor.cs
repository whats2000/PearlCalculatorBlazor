using AntDesign.TableModels;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using PearlCalculatorBlazor.Localizer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralData = PearlCalculatorLib.General.Data;
using ManuallyData = PearlCalculatorLib.Manually.Data;
using System;
using AntDesign;

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

        private async Task NoticeWithIcon(NotificationType type, string msg)
        {
            await Notice.Open(new NotificationConfig()
            {
                Message = "Notification",
                Description = msg,
                NotificationType = type
            });
        }

        protected override void OnInitialized()
        {
            EventManager.Instance.AddListener<ButtonClickArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                AmountResult = GeneralData.TNTResult;

                ShowMode = ShowResultMode.Amount;

                ShowDirectionResult(GeneralData.Pearl.Position, GeneralData.Destination);

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

                ShowDirectionResult(GeneralData.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

                ShowMode = ShowResultMode.Trace;

                StateHasChanged();
            });

            EventManager.Instance.AddListener<ButtonClickArgs>("resortResult", async (sender, args) =>
            {
                if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

                try 
                { 
                    AmountResult.SortByWeightedDistance(new(GeneralData.TNTWeight, GeneralData.MaxCalculateTNT, GeneralData.MaxCalculateDistance)); 
                }
                catch(Exception e)
                {
                    await NoticeWithIcon(NotificationType.Error, e.ToString());
                }

                StateHasChanged();
            });

            EventManager.Instance.AddListener<CalculateTNTAmuontArgs>("calculate", (sender, args) =>
            {
                _pageIndex = 1;

                AmountResult = args.Results;

                ShowMode = ShowResultMode.Amount;

                ShowDirectionResult(ManuallyData.Pearl.Position, ManuallyData.Destination.ToSpace3D());

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

                ShowDirectionResult(ManuallyData.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

                ShowMode = ShowResultMode.Trace;

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

                ShowDirectionResult(ManuallyData.Pearl.Position, new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

                ShowMode = ShowResultMode.Momentum;

                StateHasChanged();
            });

            TranslateText.OnLanguageChange += RefreshPage;
        }

        private void OnAmountRowClick(RowData<TNTCalculationResult> res)
        {
            EventManager.Instance.PublishEvent(this, "tntAmountSetRTCount", new SetRTCountArgs("ResultView", res.Data.Red, res.Data.Blue));
        }

        public void RefreshPage()
        {
            StateHasChanged();
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
