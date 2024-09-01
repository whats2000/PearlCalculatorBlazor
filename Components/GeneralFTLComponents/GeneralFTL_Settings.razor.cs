using PearlCalculatorBlazor.Localizer;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Settings
    {
        private List<ArrayPos> _pearlYMotionOptions;
        private List<ArrayPos> _selectList;
        
        private double NorthWestTntX
        {
            get => Data.NorthWestTNT.X;
            set
            {
                var northWestTnt = Data.NorthWestTNT;
                northWestTnt.X = value;
                Data.NorthWestTNT = northWestTnt;
            }
        }

        private double NorthWestTntY
        {
            get => Data.NorthWestTNT.Y;
            set
            {
                var northWestTnt = Data.NorthWestTNT;
                northWestTnt.Y = value;
                Data.NorthWestTNT = northWestTnt;
            }
        }

        private double NorthWestTntZ
        {
            get => Data.NorthWestTNT.Z;
            set
            {
                var northWestTnt = Data.NorthWestTNT;
                northWestTnt.Z = value;
                Data.NorthWestTNT = northWestTnt;
            }
        }

        private double NorthEastTntX
        {
            get => Data.NorthEastTNT.X;
            set
            {
                var northEastTnt = Data.NorthEastTNT;
                northEastTnt.X = value;
                Data.NorthEastTNT = northEastTnt;
            }
        }

        private double NorthEastTntY
        {
            get => Data.NorthEastTNT.Y;
            set
            {
                var northEastTnt = Data.NorthEastTNT;
                northEastTnt.Y = value;
                Data.NorthEastTNT = northEastTnt;
            }
        }

        private double NorthEastTntZ
        {
            get => Data.NorthEastTNT.Z;
            set
            {
                var northEastTnt = Data.NorthEastTNT;
                northEastTnt.Z = value;
                Data.NorthEastTNT = northEastTnt;
            }
        }

        private double SouthWestTntX
        {
            get => Data.SouthWestTNT.X;
            set
            {
                var southWestTnt = Data.SouthWestTNT;
                southWestTnt.X = value;
                Data.SouthWestTNT = southWestTnt;
            }
        }

        private double SouthWestTntY
        {
            get => Data.SouthWestTNT.Y;
            set
            {
                var southWestTnt = Data.SouthWestTNT;
                southWestTnt.Y = value;
                Data.SouthWestTNT = southWestTnt;
            }
        }

        private double SouthWestTntZ
        {
            get => Data.SouthWestTNT.Z;
            set
            {
                var southWestTnt = Data.SouthWestTNT;
                southWestTnt.Z = value;
                Data.SouthWestTNT = southWestTnt;
            }
        }

        private double SouthEastTntX
        {
            get => Data.SouthEastTNT.X;
            set
            {
                var southEastTnt = Data.SouthEastTNT;
                southEastTnt.X = value;
                Data.SouthEastTNT = southEastTnt;
            }
        }

        private double SouthEastTntY
        {
            get => Data.SouthEastTNT.Y;
            set
            {
                var southEastTnt = Data.SouthEastTNT;
                southEastTnt.Y = value;
                Data.SouthEastTNT = southEastTnt;
            }
        }


        private double SouthEastTntZ
        {
            get => Data.SouthEastTNT.Z;
            set
            {
                var southEastTnt = Data.SouthEastTNT;
                southEastTnt.Z = value;
                Data.SouthEastTNT = southEastTnt;
            }
        }
        
        private string PearlYMotionMode
        {
            get => Data.PearlYMotionCancellation ? "PerfectHorizontalProjection" : "NormalProjection";
            set => Data.PearlYMotionCancellation = value == "PerfectHorizontalProjection";
        }

        private double PearlYCoordinate
        {
            get => Data.Pearl.Position.Y;
            set => Data.Pearl.Position.Y = value;
        }

        private double PearlYMomentum
        {
            get => Data.Pearl.Motion.Y;
            set => Data.Pearl.Motion.Y = value;
        }
        
        private double PearlYPositionOriginal
        {
            get => Data.PearlYPositionOriginal;
            set => Data.PearlYPositionOriginal = value;
        }
        
        private double PearlYPositionAdjusted
        {
            get => Data.PearlYPositionAdjusted;
            set => Data.PearlYPositionAdjusted = value;
        }

        private string DefaultRedTNTPosition
        {
            get => Data.DefaultRedDuper.ToString();
            set => Data.DefaultRedDuper = Enum.Parse<Direction>(value);
        }

        private string DefaultBlueTNTPosition
        {
            get => Data.DefaultBlueDuper.ToString();
            set => Data.DefaultBlueDuper = Enum.Parse<Direction>(value);
        }

        private async void ResetToDefault_OnClick()
        {
            Data.Reset();
            await JSRuntime.InvokeVoidAsync("ResetStateInJs");
            StateHasChanged();
        }

        class Checkbox
        {
            public bool isChecked = true;

            public void ToggleChecked()
            {
                isChecked = !isChecked;
            }
        };

        readonly Checkbox NorthWestTntCheck = new();
        readonly Checkbox NorthEastTntCheck = new();
        readonly Checkbox SouthWestTntCheck = new();
        readonly Checkbox SouthEastTntCheck = new();

        readonly Checkbox PearlYCheck = new();

        class ArrayPos
        {
            public string ActiveKey { get; set; }
            public string DisplayName { get; set; }
        }


        protected override void OnInitialized()
        {
            // Initialize _selectList for TNT positions
            _selectList = new List<ArrayPos>
            {
                new ArrayPos { ActiveKey = "NorthWest", DisplayName = TranslateText.GetTranslateText("NorthWest")},
                new ArrayPos { ActiveKey = "NorthEast", DisplayName = TranslateText.GetTranslateText("NorthEast")},
                new ArrayPos { ActiveKey = "SouthWest", DisplayName = TranslateText.GetTranslateText("SouthWest")},
                new ArrayPos { ActiveKey = "SouthEast", DisplayName = TranslateText.GetTranslateText("SouthEast")}
            };

            // Initialize _pearlYMotionOptions for PearlYMotionMode enum
            _pearlYMotionOptions = new List<ArrayPos>
            {
                new ArrayPos { ActiveKey = "NormalProjection", DisplayName = TranslateText.GetTranslateText("NormalProjection")},
                new ArrayPos { ActiveKey = "PerfectHorizontalProjection", DisplayName = TranslateText.GetTranslateText("PerfectHorizontalProjection")}
            };

            TranslateText.OnLanguageChange += RefreshPage;
        }

        public void RefreshPage()
        {
            foreach (var pair in _selectList)
                pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

            // Update the display names for PearlYMotionMode options
            foreach (var pair in _pearlYMotionOptions)
                pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

            StateHasChanged();
        }
    }
}
