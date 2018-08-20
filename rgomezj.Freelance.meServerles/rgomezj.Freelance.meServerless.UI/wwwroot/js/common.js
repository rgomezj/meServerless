window.common = (function () {
    var common = {};

    var PROGRESSMESSAGE = "Please wait until the operation is completed.....";

    common.API = {};
    common.MESSSAGES = {};
    common.PAGETITLES = {};
    
    /*API URL Constants*/
    common.API.INFO = "http://localhost:7071/api/FreelanceInfo";
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

    common.HideProgressMessage = function () {
        $("#paragraph-dialog-message").html(PROGRESSMESSAGE);
        $("#dialog-message").dialog("close");
    }
    return common;
})();