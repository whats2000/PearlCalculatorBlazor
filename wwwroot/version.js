async function FetchVersionFromServer() {
    try {
        // Append a timestamp to the URL to prevent caching
        const timestamp = new Date().getTime();
        const response = await fetch(`/version.json?t=${timestamp}`);
        const data = await response.json();
        return data.version;
    } catch (error) {
        console.error('Error fetching version:', error);
        return null;
    }
}

async function ClearCacheAndReload() {
    if ('caches' in window) {
        // Clear all the cache storage
        const cacheNames = await caches.keys();
        await Promise.all(cacheNames.map(cacheName => caches.delete(cacheName)));
    }

    // Reload the page
    window.location.reload(true);
}