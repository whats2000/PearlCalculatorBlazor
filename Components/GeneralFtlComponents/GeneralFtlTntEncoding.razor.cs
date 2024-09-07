using System;
using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorBlazor.Components.GeneralFtlComponents;

public partial class GeneralFtlTntEncoding : ComponentBase
{
    private const string PublishKey = "GeneralFtlTntEncoding";

    private string RedTntInput { get; set; } = string.Join(", ", Data.RedTNTConfiguration);
    private string BlueTntInput { get; set; } = string.Join(", ", Data.BlueTNTConfiguration);

    private Direction Direction
    {
        get => Data.Direction;
        set => Data.Direction = value;
    }

    private uint RedTnt
    {
        get => (uint)Data.RedTNT;
        set => Data.RedTNT = (int)value;
    }

    private uint BlueTnt
    {
        get => (uint)Data.BlueTNT;
        set => Data.BlueTNT = (int)value;
    }

    private void ValidateRedTntInput()
    {
        if (TryParseTntConfiguration(RedTntInput, out var redTntValues))
        {
            Data.RedTNTConfiguration = redTntValues;
        }
        else
        {
            RedTntInput = string.Join(", ", Data.RedTNTConfiguration);
            Notice.Open(new NotificationConfig()
            {
                Message = TranslateText.GetTranslateText("InvalidRedTntConfiguration"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
        }
    }

    private void ValidateBlueTntInput()
    {
        if (TryParseTntConfiguration(BlueTntInput, out var blueTntValues))
        {
            Data.BlueTNTConfiguration = blueTntValues;
        }
        else
        {
            BlueTntInput = string.Join(", ", Data.BlueTNTConfiguration);
            Notice.Open(new NotificationConfig()
            {
                Message = TranslateText.GetTranslateText("InvalidBlueTntConfiguration"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
        }
    }

    private bool TryParseTntConfiguration(string input, out List<int> tntValues)
    {
        tntValues = new List<int>();

        if (string.IsNullOrWhiteSpace(input)) return true;

        var parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            if (!int.TryParse(part.Trim(), out var value))
                return false;
            
            if (value <= 0)
                return false;
            
            tntValues.Add(value);
        }
        return true;
    }

    private void PearlSimulate()
    {
        var pearlTraceList = Calculation.CalculatePearlTrace((int)RedTnt, (int)BlueTnt, 100, Direction);
        EventManager.Instance.PublishEvent(this, "simulate",
            new PearlSimulateArgs(PublishKey, Data.Pearl, pearlTraceList));
    }

    private void CalculateTntEncoding()
    {
        if (Data.RedTNTConfiguration.Count == 0 || Data.BlueTNTConfiguration.Count == 0)
        {
            Notice.Open(new NotificationConfig()
            {
                Message = TranslateText.GetTranslateText("RedAndBlueTntConfigurationsMissing"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
            return;
        }

        EventManager.Instance.PublishEvent(this, "calculateTntEncoding", new BaseEventArgs(PublishKey));
    }

    protected override void OnInitialized()
    {
        TranslateText.OnLanguageChange += RefreshPage;
    }

    private void RefreshPage()
    {
        StateHasChanged();
    }
}