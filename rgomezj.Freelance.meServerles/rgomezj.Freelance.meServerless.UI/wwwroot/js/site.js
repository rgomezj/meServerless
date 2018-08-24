freelanceApp = {};

$(function () {

    freelanceApp.mainPageViewModel = function (common, $, ko) {
        debugger;
        isLoading = ko.observable(false);

        _self = this;

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

        /* Extending the mapping to include a property for all of the elements of the observable array */
        var mapping = {
            'freelance': {
                create: function (options) {
                    return new createFreelanceInfo(options.data);
                }
            }
        }

        var renderedHandlerTechnologies = function (elements, data) {
            if ($('#client-logos').children().length === _self.info.technologies().length) {
                $("#client-logos").owlCarousel({
                    items: 5,
                    autoplay: true,
                    autoplayTimeout: 3000,
                    loop: true,
                    autoplayHoverPause: true,
                    dots: false,
                    margin: 60,
                    responsive: {
                        0: {
                            items: 2,
                            margin: 30
                        },
                        768: {
                            items: 5,
                            margin: 30
                        },
                        992: {
                            items: 5,
                            margin: 60
                        }
                    }
                });
            }
        }

        var renderedHandlerReferences = function (elements, data) {
            if ($('#testimonials').children().length === _self.info.references().length) {
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
        
        return {
            GetInfo: getInfo,
            RenderedHandlerTechnologies: renderedHandlerTechnologies,
            RenderedHandlerReferences: renderedHandlerReferences
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
            }
        }
    }
});

ko.bindingHandlers.counterUp = {
    init: function (elem) {
        $(elem).counterUp({
            delay: 20,
            time: 1500
        });
    }
}
