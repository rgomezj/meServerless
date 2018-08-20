freelanceApp = {};

$(function () {

    freelanceApp.mainPageViewModel = function (common, $, ko) {
        debugger;
        isLoading = ko.observable(false);

        info = ko.mapping.fromJS({ skills: ko.observableArray(), companies: ko.observableArray(), generalInfo: ko.observable(), references: ko.observableArray(), technologies: ko.observableArray(), aptitudes: ko.observableArray() });

        var getInfo = function (applyBindings) {
            isLoading(true);
            $.getJSON(common.API.INFO, function (data) {
                ko.mapping.fromJS(data, info);
                isLoading(false);
                applyBindings();
            })
                .done(function () {
                    isLoading(false);
                })
                .fail(function (jqxhr, textStatus, error) {
                    isLoading(false);
                });
        }

        return {
            GetInfo: getInfo
        };

    }(common, $, ko);

    if (document.getElementById("Container") != null) {
        freelanceApp.mainPageViewModel.GetInfo(applyBindings);
    }

    function applyBindings() {
        if (document.getElementById("Container") != null) {
            if (!ko.dataFor(document.getElementById("Container"))) {
                ko.applyBindings(freelanceApp.mainPageViewModel, document.getElementById("Container"));
            }
        }
    }
});
