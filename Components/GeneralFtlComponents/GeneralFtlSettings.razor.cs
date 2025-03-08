﻿using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorBlazor.Components.GeneralFtlComponents;

public partial class GeneralFtlSettings
{
    private readonly Checkbox _northEastTntCheck = new();
    private readonly Checkbox _northWestTntCheck = new();
    private readonly Checkbox _pearlYCheck = new();
    private readonly Checkbox _southEastTntCheck = new();
    private readonly Checkbox _southWestTntCheck = new();
    private List<ArrayPos> _pearlYMotionOptions;
    private List<ArrayPos> _selectList;
    private List<ArrayPos> _selectedGameVersion;

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

    private string DefaultRedTntPosition
    {
        get => Data.DefaultRedDuper.ToString();
        set => Data.DefaultRedDuper = Enum.Parse<Direction>(value);
    }

    private string DefaultBlueTntPosition
    {
        get => Data.DefaultBlueDuper.ToString();
        set => Data.DefaultBlueDuper = Enum.Parse<Direction>(value);
    }

    private string SelectedGameVersion {
        get => GameVersionUtils.ToString(Data.GameVersion);
        set => Data.GameVersion = GameVersionUtils.TryParse(value);
    }

    private async void ResetToDefaultOnClick()
    {
        Data.Reset();
        SettingsManager.SelectedCannon.SyncWithData();
        await JsRuntime.InvokeVoidAsync("ResetStateInJs");
        StateHasChanged();
    }


    protected override void OnInitialized()
    {
        // Initialize _selectList for TNT positions
        _selectList = new List<ArrayPos>
        {
            new() { ActiveKey = "NorthWest", DisplayName = TranslateText.GetTranslateText("NorthWest") },
            new() { ActiveKey = "NorthEast", DisplayName = TranslateText.GetTranslateText("NorthEast") },
            new() { ActiveKey = "SouthWest", DisplayName = TranslateText.GetTranslateText("SouthWest") },
            new() { ActiveKey = "SouthEast", DisplayName = TranslateText.GetTranslateText("SouthEast") }
        };

        // Initialize _pearlYMotionOptions for PearlYMotionMode enum
        _pearlYMotionOptions = new List<ArrayPos>
        {
            new() { ActiveKey = "NormalProjection", DisplayName = TranslateText.GetTranslateText("NormalProjection") },
            new()
            {
                ActiveKey = "PerfectHorizontalProjection",
                DisplayName = TranslateText.GetTranslateText("PerfectHorizontalProjection")
            }
        };

        _selectedGameVersion = new List<ArrayPos> {
            new() { ActiveKey = "1.11-1.21.1", DisplayName = TranslateText.GetTranslateText("1.11-1.21.1") },
            new() { ActiveKey = "1.21.2+", DisplayName = TranslateText.GetTranslateText("1.21.2+") }
        };

        TranslateText.OnLanguageChange += RefreshPage;
        
        EventManager.Instance.AddListener<BaseEventArgs>("dataChanged", (_, _) => { RefreshPage(); });
    }

    private void RefreshPage()
    {
        foreach (var pair in _selectList)
            pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

        // Update the display names for PearlYMotionMode options
        foreach (var pair in _pearlYMotionOptions)
            pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

        foreach (var pair in _selectedGameVersion)
            pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

        StateHasChanged();
    }

    private class Checkbox
    {
        public bool IsChecked = true;

        public void ToggleChecked()
        {
            IsChecked = !IsChecked;
        }
    }

    private class ArrayPos
    {
        public string ActiveKey { get; set; }
        public string DisplayName { get; set; }
    }
}