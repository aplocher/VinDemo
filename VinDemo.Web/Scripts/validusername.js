$(function () {

    jQuery.validator.addMethod('validusername', function (value, element, params) {
        var id = $("#modalContent").data('id');
        var isValid = false;

        $.ajax({
            method: 'post',
            url: $("#tableContainer").data("isusernameavailurl"),
            data: { "id": id, "username": value },
            async: false,
            success: function (data) {
                isValid = data;
            },
            error: function () {
                isValid = false;
            }
        });

        return isValid;
    }, '');

    //jQuery.validator.unobtrusive.adapters.add('validusername', ['memberid'], function (options) {
    jQuery.validator.unobtrusive.adapters.add('validusername', function (options) {

        options.rules['validusername'] = {
            //memberid': options.params.memberid
        };
        options.messages['validusername'] = options.message;
    });

}(jQuery));