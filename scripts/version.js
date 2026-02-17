async function FetchVersionFromServer() {
    try {
        const basePath = window.location.pathname.replace(/\/$/, "");
        const timestamp = new Date().getTime();
        const response = await fetch(`${basePath}/version.json?t=${timestamp}`);
        const data = await response.json();
        return {
            version: data.version,
            updateNotes: data.updateNotes
        };
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