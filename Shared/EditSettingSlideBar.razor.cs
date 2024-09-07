using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.Settings;

namespace PearlCalculatorBlazor.Shared;

public partial class EditSettingSlideBar : ComponentBase
{
    private int _newCannonCounter = 1;
    private Dictionary<int, string> _tempCannonNames = new();

    private async Task CreateNewCannon()
    {
        var newCannon = new CannonSettings
        {
            CannonName = "New Cannon " + _newCannonCounter++,
            MaxTNT = Data.MaxTNT,
            DefaultRedDirection = Data.DefaultRedDuper,
            DefaultBlueDirection = Data.DefaultBlueDuper,
            NorthWestTNT = Data.NorthWestTNT,
            NorthEastTNT = Data.NorthEastTNT,
            SouthWestTNT = Data.SouthWestTNT,
            SouthEastTNT = Data.SouthEastTNT,
            Offset = Data.PearlOffset,
            Pearl = Data.Pearl.DeepClone(),
            RedTNTConfiguration = Data.RedTNTConfiguration,
            BlueTNTConfiguration = Data.BlueTNTConfiguration,
            PearlYMotionCancellation = Data.PearlYMotionCancellation,
            PearlYPositionOriginal = Data.PearlYPositionOriginal,
            PearlYPositionAdjusted = Data.PearlYPositionAdjusted
        };

        var success = SettingsManager.AddSettings(newCannon);
        if (!success)
            await Notice.Open(new NotificationConfig
            {
                Message = "Notification",
                Description = TranslateText.GetTranslateText("FailedToCreateNewCannon"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
        _tempCannonNames[SettingsManager.SettingsList.Count - 1] = newCannon.CannonName;

        StateHasChanged();
    }

    private async Task CopyCannon(int index)
    {
        var i = index;
        var newCannon = SettingsManager.SettingsList[index].DeepClone();
        var success = SettingsManager.AddSettings(newCannon);
        if (!success)
            await Notice.Open(new NotificationConfig
            {
                Message = "Notification",
                Description = TranslateText.GetTranslateText("FailedToCopyCannon"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
        _tempCannonNames[SettingsManager.SettingsList.Count - 1] = newCannon.CannonName;

        StateHasChanged();
    }

    private void RemoveCannon(int index)
    {
        if (SettingsManager.SettingsList.Count <= 1) return;
        SettingsManager.RemoveSettings(index);
        SettingsManager.SelectCannon(1);
        _tempCannonNames.Remove(index);
        StateHasChanged();
    }

    private void RenameCannon(string newName, int index)
    {
        var oldName = SettingsManager.SettingsList[index].CannonName;

        if (string.IsNullOrWhiteSpace(newName))
        {
            Notice.Open(new NotificationConfig
            {
                Message = "Notification",
                Description = TranslateText.GetTranslateText("CannonNameCannotBeEmpty"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
            _tempCannonNames[index] = oldName;
            return;
        }

        if (newName == oldName) return;

        if (SettingsManager.SettingsList.Any(c => c.CannonName == newName))
        {
            Notice.Open(new NotificationConfig
            {
                Message = "Notification",
                Description = TranslateText.GetTranslateText("CannonNameAlreadyExist"),
                Duration = 3,
                NotificationType = NotificationType.Error
            });
            _tempCannonNames[index] = oldName;
            return;
        }

        SettingsManager.SettingsList[index].CannonName = newName;
    }

    protected override void OnInitialized()
    {
        TranslateText.OnLanguageChange += RefreshPage;
        EventManager.Instance.AddListener<BaseEventArgs>("importSettings", (_, _) => { RefreshPage(); });
        foreach (var (cannon, index) in SettingsManager.SettingsList.Select((value, index) => (value, index)))
            _tempCannonNames[index] = cannon.CannonName;
    }

    private void RefreshPage()
    {
        _tempCannonNames.Clear();
        foreach (var (cannon, index) in SettingsManager.SettingsList.Select((value, index) => (value, index)))
            _tempCannonNames[index] = cannon.CannonName;
        StateHasChanged();
    }
}