$('#contact-form-submit').on('click', function () {

    if ($("#contact-form").valid()) {
        var name = $("#FromName").val();
        var email = $("#From").val();
        var subject = $("#Subject").val();
        var message = $("#Message").val();
        var firstName = name;

        if (firstName.indexOf(' ') >= 0) {
            firstName = name.split(' ').slice(0, -1).join(' ');
        }
        $.ajax({
            url: "./Index",
            type: "POST",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: $('form').serialize(),
            success: function (response) {
                console.log(response);
                if (response.success) {
                    $('#submitMessage').html("<div class='alert alert-success'>");
                    $('#submitMessage > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                        .append("</button>");
                    $('#submitMessage > .alert-success')
                        .append("<strong>Your message has been sent. </strong>");
                    $('#submitMessage > .alert-success')
                        .append('</div>');
                    $('#submitMessage').show();
                    $('#submitMessage').delay(5000).fadeOut();
                    //clear all fields
                    $('#contact-form').trigger("reset");
                    grecaptcha.reset();
                }
                else {
                    $('#submitMessage').html("<div class='alert alert-danger'>");
                    $('#submitMessage > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
                        .append("</button>");
                    $('#submitMessage > .alert-danger').append("<strong>" + response.errorMessage + "</strong>");
                    $('#submitMessage > .alert-danger').append('</div>');
                    $('#submitMessage').show();
                    $('#submitMessage').delay(5000).fadeOut();
                    //clear all fields
                    $('#contact-form').trigger("reset");
                    grecaptcha.reset();
                }
            }
        })
    }
});