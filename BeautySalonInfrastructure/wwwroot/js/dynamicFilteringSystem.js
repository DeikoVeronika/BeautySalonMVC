$(document).ready(function () {
    $("#TypeServices_Name").change(function () {
        var selectedTypeService = $(this).val();

        $.ajax({
            url: '/Services/GetServices',
            type: 'GET',
            data: { typeServiceId: selectedTypeService },
            success: function (data) {
                data.sort(function (a, b) {
                    return a.name.localeCompare(b.name);
                });

                var serviceSelect = $('#Services_Name');
                serviceSelect.empty();

                $.each(data, function (index, service) {
                    serviceSelect.append($('<option/>', {
                        value: service.id,
                        text: service.name
                    }));
                });

                $("#Services_Name").trigger("change");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });

    $("#Services_Name").change(function () {
        var selectedService = $(this).val();

        $.ajax({
            url: '/Services/GetServiceDescription',
            type: 'GET',
            data: { serviceId: selectedService },
            success: function (data) {
                var descriptionInput = $('#Services_Description');
                descriptionInput.val(data.description);
                descriptionInput.css('height', 'auto'); // reset the height
                descriptionInput.height(descriptionInput.prop('scrollHeight'));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        $.ajax({
            url: '/Services/GetTypeService',
            type: 'GET',
            data: { serviceId: selectedService },
            success: function (data) {
                console.log(data); // Added this line to log the returned data
                var typeServiceSelect = $('#TypeServices_Name');
                typeServiceSelect.val(data.typeServiceId);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });
});
