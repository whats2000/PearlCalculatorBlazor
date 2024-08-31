var importSettingsHandle;
var downloadSettingsLink;

function ImportSettings() {

    if (!importSettingsHandle) {
        importSettingsHandle = document.getElementById("import-settings-handle");
    }
    return importSettingsHandle.click();
}

function ExportSettings(json) {
    if (!downloadSettingsLink) {
        downloadSettingsLink = document.getElementById("download-settings-link");
    }

    var blob = new Blob([json]);
    downloadSettingsLink.href = URL.createObjectURL(blob);
    downloadSettingsLink.download = "pearl calculator settings.json";
    downloadSettingsLink.click();
}

function ResetStateInJs() {
    importSettingsHandle = null;
    downloadSettingsLink = null;
}
