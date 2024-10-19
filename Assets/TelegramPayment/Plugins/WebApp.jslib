const webAppLibrary = {

    // Class definition

    $webApp: {

        // Function to load Telegram WebApp script
        loadTelegramWebAppScript: function() {
            var script = document.createElement('script');
            script.src = "https://telegram.org/js/telegram-web-app.js";
            script.onload = function() {
                console.log("Telegram WebApp script loaded successfully.");
            };
            script.onerror = function() {
                console.error("Failed to load Telegram WebApp script.");
            };
            document.head.appendChild(script);
        },



        // Check if Telegram WebApp is available
        isTelegramApp: function() {
            return typeof Telegram !== 'undefined' && Telegram.WebApp !== null;
        },

        // Check if the Telegram WebApp version is at least the provided version
        isVersionAtLeast: function(version) {
            if (!webApp.isTelegramApp()) {
                return false;
            }

            return Telegram.WebApp.isVersionAtLeast(version);
        },

        // Check if Telegram WebApp is expanded
        isExpanded: function() {
            if (!webApp.isTelegramApp()) {
                return false;
            }

            return Telegram.WebApp.isExpanded;
        },

        // Get the current version of Telegram WebApp
        getVersion: function() {
            if (!webApp.isTelegramApp()) {
                return "";
            }

            return Telegram.WebApp.version;
        },

        // Expand the Telegram WebApp
        expand: function() {
            if (!webApp.isTelegramApp()) {
                return;
            }

            Telegram.WebApp.expand();
        },

        // Close the Telegram WebApp
        close: function() {
            if (!webApp.isTelegramApp()) {
                return;
            }

            Telegram.WebApp.close();
        },

        // Open an external link using Telegram WebApp
        openExternalLink: function(url, tryInstantView) {
            if (!webApp.isTelegramApp()) {
                return;
            }
            var link = UTF8ToString(url);
             console.log('Linkk Extend',link)
            let options = tryInstantView ? { try_instant_view: true } : {};
            Telegram.WebApp.openLink(link, options);
        },

        // Open a Telegram link using Telegram WebApp
        openTelegramLink: function(url) {
            if (!webApp.isTelegramApp()) {
                return;
            }
            var link = UTF8ToString(url);
            console.log('Linkk',link)
            Telegram.WebApp.openTelegramLink(link);
        },

        onTriggerHapticFeedback: function(status){
            if (!webApp.isTelegramApp()) {
                return;
            }
                        var statusHaptic = UTF8ToString(status);
            const eventType = 'web_app_trigger_haptic_feedback';
            const eventData = {
                type: 'impact',
                impact_style: statusHaptic
            };

        Telegram.WebView.postEvent(eventType, false, eventData);
        },

        onResize: function(){
            window.dispatchEvent(
            new CustomEvent("SendMessageToReactResize", { detail: "" })
            );
        },

        // Open an invoice using Telegram WebApp and handle callbacks
        openInvoice: function(link, successCallbackPtr, errorCallbackPtr) {
            if (!webApp.isTelegramApp()) {
                return;
            }

            var invoiceLink = UTF8ToString(link);

            Telegram.WebApp.openInvoice(invoiceLink, function(status) {
                console.log("Status action invoice: ",status)
                unityInstance.sendMessage('TelegramInvoiceController', 'HandlePaymentStatus', status);
                Telegram.WebApp.onEvent('invoiceClosed', function(event) {
                    var statusPtr = allocate(intArrayFromString(event.status), ALLOC_NORMAL);

                    if (event.status === "paid") {
                        var paymentJson = JSON.stringify(event);
                        var paymentPtr = allocate(intArrayFromString(paymentJson), ALLOC_NORMAL);

                        dynCall('vii', successCallbackPtr, [statusPtr, paymentPtr]);

                        _free(paymentPtr);
                    } else {
                        dynCall('vi', errorCallbackPtr, [statusPtr]);
                    }

                    _free(statusPtr);
                });
            });
        }
    },

    // External C# calls
    LoadTelegramWebAppScript: function() {
        webApp.loadTelegramWebAppScript();
    },

    OnTriggerHapticFeedback: function(status){
        webApp.onTriggerHapticFeedback(status);
    },

    OnResize: function(){
        webApp.onResize();
    },
    
    IsTelegramApp: function() {
        return webApp.isTelegramApp();
    },

    IsSupportedVersionActive: function(version) {
        return webApp.isVersionAtLeast(version);
    },

    IsExpanded: function() {
        return webApp.isExpanded();
    },

    GetCurrentVersion: function() {
        return webApp.getVersion();
    },

    Expand: function() {
        webApp.expand();
    },

    Close: function() {
        webApp.close();
    },

    OpenInvoice: function(link, successCallbackPtr, errorCallbackPtr) {
        webApp.openInvoice(link, successCallbackPtr, errorCallbackPtr);
    },

    OpenExternalLink: function(url, tryInstantView) {
        webApp.openExternalLink(url, tryInstantView);
    },

    OpenTelegramLink: function(url) {
        webApp.openTelegramLink(url);
    }
};

// Ensure the dependencies are added and merged into the WebGL build
autoAddDeps(webAppLibrary, '$webApp');
mergeInto(LibraryManager.library, webAppLibrary);
