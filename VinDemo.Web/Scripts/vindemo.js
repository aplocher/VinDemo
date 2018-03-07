(function($) {
    var defaultOptions = {
        errorClass: "is-invalid",
        validClass: "is-valid",
        highlight: function(element, errorClass, validClass) {
            $(element)
                .addClass(errorClass)
                .removeClass(validClass);
        },
        unhighlight: function(element, errorClass, validClass) {
            $(element)
                .removeClass(errorClass)
                .addClass(validClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };
})(jQuery);