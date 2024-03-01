$(document).ready(function () {
    var previousServiceValue;

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
                previousServiceValue = serviceSelect.val();
                serviceSelect.empty();

                $.each(data, function (index, service) {
                    serviceSelect.append($('<option/>', {
                        value: service.id,
                        text: service.name
                    }));
                });

                serviceSelect.val(previousServiceValue);

                if (previousServiceValue !== serviceSelect.val()) {
                    $("#Services_Name").trigger("change");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });

    $("#Services_Name").change(function () {
        var selectedService = $(this).val();
        var selectedTypeService = $('#TypeServices_Name').val(); 
        if (selectedTypeService !== "") {
            var descriptionInput = $('#Services_Description');
            descriptionInput.val('');
            descriptionInput.css('height', 'auto');
            descriptionInput.height(descriptionInput.prop('scrollHeight'));
        }

        $.ajax({
            url: '/Services/GetServiceDescription',
            type: 'GET',
            data: { serviceId: selectedService },
            success: function (data) {
                var descriptionInput = $('#Services_Description');
                descriptionInput.val(data.description);
                descriptionInput.css('height', 'auto');
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
                var typeServiceSelect = $('#TypeServices_Name');
                typeServiceSelect.val(data.typeServiceId);
                typeServiceSelect.trigger("change");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        $.ajax({
            url: '/EmployeeServices/GetEmployees',
            type: 'GET',
            data: { serviceId: selectedService },
            success: function (data) {
                data.sort(function (a, b) {
                    return a.name.localeCompare(b.name);
                });

                var employeeSelect = $('#EmployeeServices_EmployeesId');
                employeeSelect.empty();

                $.each(data, function (index, employee) {
                    employeeSelect.append($('<option/>', {
                        value: employee.id,
                        text: employee.name
                    }));
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });
    var usedDates = {};
    $("select[name='Schedules.Date'] > option").each(function () {
        if (usedDates[this.text]) {
            $(this).remove();
        } else {
            usedDates[this.text] = this.value;
        }
    });


});
