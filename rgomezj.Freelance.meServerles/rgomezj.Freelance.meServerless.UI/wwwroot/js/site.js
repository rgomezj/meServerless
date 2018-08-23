freelanceApp = {};

$(function () {

    freelanceApp.mainPageViewModel = function (common, $, ko) {
        debugger;
        isLoading = ko.observable(false);

        info = ko.mapping.fromJS({ skills: ko.observableArray(), companies: ko.observableArray(), generalInfo: ko.observable(), references: ko.observableArray(), technologies: ko.observableArray(), aptitudes: ko.observableArray() });

        var getInfo = function (applyBindings) {
            isLoading(true);
            $.getJSON(common.API.INFO, function (data) {
                ko.mapping.fromJS(data, mapping, info);
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

        /* Extending the mapping to include a property for all of the elements of the observable array */
        var mapping = {
            'freelance': {
                create: function (options) {
                    return new createFreelanceInfo(options.data);
                }
            }
        }

        var createFreelanceInfo = function (freelanceInfo) {
            var self = this;
            var result = ko.mapping.fromJS(freelanceInfo);
            result.hasChanged = ko.observable(false);
            return result;
        };
        /* END extending the mapping*/

        return {
            GetInfo: getInfo
        };

    }(common, $, ko);

    if (document.getElementById("ContainerApp") != null) {
        freelanceApp.mainPageViewModel.GetInfo(applyBindings);
    }

    function applyBindings() {
        if (document.getElementById("ContainerApp") != null) {
            if (!ko.dataFor(document.getElementById("ContainerApp"))) {
                ko.applyBindings(freelanceApp.mainPageViewModel, document.getElementById("ContainerApp"));
                ko.applyBindings(freelanceApp.mainPageViewModel, document.getElementById("ContainerApp2"));

                $("#testimonials").owlCarousel({
                    items: 1,
                    animateOut: 'slideOutDown',
                    animateIn: 'flipInX',
                    smartSpeed: 450,
                    autoplay: true,
                    autoplayTimeout: 4000,
                    loop: true,
                    autoplayHoverPause: true
                });
            }
        }
    }
});
