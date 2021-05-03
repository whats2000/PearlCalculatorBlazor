var downloadSettingsLink;

function ImportSettings() {

    return document.getElementById("import-settings-handle").click();
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