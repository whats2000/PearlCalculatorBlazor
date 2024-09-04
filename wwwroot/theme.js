function AddElementToBody(url) {
    let link = document.getElementById('dynamic-theme');
    if (!link) {
        link = document.createElement('link');
        link.id = 'dynamic-theme';
        link.rel = 'stylesheet';
        document.head.appendChild(link);
    }
    link.href = url;
}

function GetBaseUrl() {
    return window.location.origin + window.location.pathname;
}