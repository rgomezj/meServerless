window.common = (function () {
    var common = {};

    var PROGRESSMESSAGE = "Please wait until the operation is completed.....";

    common.API = {};
    common.MESSSAGES = {};
    common.PAGETITLES = {};
    
    /*API URL Constants*/
    common.API.INFO = "";
    common.API.SENDEMAIL = "";
    common.PAGETITLES.INDEX = "Roger Gomez";
    
    common.ShowProgressMessage = function (message) {
        $(function () {
            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                }
            });

            if (message) {
                $("#paragraph-dialog-message").html(message);
            }

            $(".ui-dialog-titlebar-close").css("display", "none"); // hide the close button. User can still hit ESC
        });
    }

    common.setApiInfo = function () {
        common.getApiBase
        common.API.INFO = common.getApiBase() + "FreelanceInfo";
        common.API.SENDEMAIL = common.getApiBase() + "SendEmail";
    }

    common.getApiBase = function () {
        if (common.isLocal()) {
            return "http://localhost:7071/api/";
        }
        else {
            return "https://mefreelancefunctionapp.azurewebsites.net/api/";
        }
    }

    common.isLocal = function () {
        return window.document.location.href.toUpperCase().indexOf("LOCALHOST") > -1;
    }

    common.HideProgressMessage = function () {
        $("#paragraph-dialog-message").html(PROGRESSMESSAGE);
        $("#dialog-message").dialog("close");
    }
    return common;
})();

common.setApiInfo();