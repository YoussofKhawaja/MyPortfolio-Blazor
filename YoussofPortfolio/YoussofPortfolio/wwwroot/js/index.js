function scrollToElement(elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
    }
}

function getDeviceName() {
    if (/android/i.test(navigator.userAgent)) {
        return 'android';
    }
    if (/iphone/i.test(navigator.userAgent)) {
        return 'iphone';
    }
    //desktop
    if (/windows/i.test(navigator.userAgent)) {
        return 'windows';
    }
    //linux
    if (/linux/i.test(navigator.userAgent)) {
        return 'linux';
    }
    return 'unknown';
}

function isEmbedLoaded() {
    return new Promise(function (resolve, reject) {
        var embed = document.getElementById('embedContent');

        if (embed) {
            embed.onload = function () {
                console.log("Resume is ready.");
                resolve(true);
            };
            embed.onerror = function () {
                console.log("Error loading resume.");
                reject(false);
            };
        } else {
            console.log("Embed element not found.");
            reject(false);
        }
    });
}