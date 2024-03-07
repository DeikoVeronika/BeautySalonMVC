//$(document).ready(function () {
//    var previousServiceValue;

//    $("select[name='EmployeeServices.EmployeesId']").prop('disabled', true);
//    $("select[name='Schedules.Date']").prop('disabled', true);
//    $("select[name='Schedules.StartTime']").prop('disabled', true);
//$("#TypeServices_Name").change(function () {
//        const selectedTypeService = $(this).val();

//        var usedDates = {};
//        var usedStartTimes = {};

//        $.ajax({
//            url: '/Services/GetServices',
//            type: 'GET',
//            data: { typeServiceId: selectedTypeService },
//            success: function (data) {
//                var self = this;

//                data.sort(function (a, b) {
//                    return a.name.localeCompare(b.name);
//                });

//                var serviceSelect = self.find('#Services_Name');
//                previousServiceValue = serviceSelect.val();
//                serviceSelect.empty();

//                $.each(data, function (index, service) {
//                    serviceSelect.append($('<option/>', {
//                        value: service.id,
//                        text: service.name
//                    }));
//                });

//                serviceSelect.val(previousServiceValue);

//                if (previousServiceValue !== serviceSelect.val()) {
//                    $("#Services_Name").trigger("change");
//                }
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });
//    });

//    $("#Services_Name").change(function () {
//        var selectedService = $(this).val();
//        var selectedTypeService = $('#TypeServices_Name').val();

//        $("select[name='EmployeeServices.EmployeesId']").prop('disabled', selectedService === "");
//        $("select[name='Schedules.Date']").prop('disabled', selectedService === "");
//        $("select[name='Schedules.StartTime']").prop('disabled', selectedService === "");

//        $("select[name='EmployeeServices.EmployeesId']").empty();
//        $("select[name='Schedules.Date']").empty();
//        $("select[name='Schedules.StartTime']").empty();

//        if (selectedTypeService !== "") {
//            var descriptionInput = $('#Services_Description');
//            descriptionInput.val('');
//            descriptionInput.css('height', 'auto');
//            descriptionInput.height(descriptionInput.prop('scrollHeight'));
//        }

//        $.ajax({
//            url: '/Services/GetServiceDescription',
//            type: 'GET',
//            data: { serviceId: selectedService },
//            success: function (data) {
//                var descriptionInput = $('#Services_Description');
//                descriptionInput.val(data.description);
//                descriptionInput.css('height', 'auto');
//                descriptionInput.height(descriptionInput.prop('scrollHeight'));
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });

//        $.ajax({
//            url: '/Services/GetTypeService',
//            type: 'GET',
//            data: { serviceId: selectedService },
//            success: function (data) {
//                var typeServiceSelect = $('#TypeServices_Name');
//                typeServiceSelect.val(data.typeServiceId);
//                typeServiceSelect.trigger("change");
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });

//        $.ajax({
//            url: '/EmployeeServices/GetEmployees',
//            type: 'GET',
//            data: { serviceId: selectedService },
//            success: function (data) {
//                data.sort(function (a, b) {
//                    return a.name.localeCompare(b.name);
//                });

//                var employeeSelect = $('#EmployeeServices_EmployeesId');
//                employeeSelect.empty();
//                employeeSelect.append($('<option/>', {
//                    value: "",
//                    text: "Оберіть майстра",
//                    disabled: "disabled"
//                }));

//                $.each(data, function (index, employee) {
//                    employeeSelect.append($('<option/>', {
//                        value: employee.id,
//                        text: employee.name
//                    }));
//                });
//                employeeSelect.val("");

//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });
//    });
//    var usedDates = {};
//    $("select[name='Schedules.Date'] > option").each(function () {
//        if (usedDates[this.text]) {
//            $(this).remove();
//        } else {
//            usedDates[this.text] = this.value;
//        }
//    });

//    $("#EmployeeServices_EmployeesId").change(function () {
//        var selectedEmployee = $(this).val();

//        var usedDates = {};
//        var usedStartTimes = {};

//        $('#Schedules_Date').val("");
//        $('#Schedules_StartTime').empty();

//        $.ajax({
//            url: '/Schedules/GetDates',
//            type: 'GET',
//            data: { employeeId: selectedEmployee },
//            success: function (data) {
//                var dateSelect = $('#Schedules_Date');
//                dateSelect.empty();

//                dateSelect.append($('<option/>', {
//                    value: "",
//                    text: "Оберіть дату",
//                    disabled: "disabled"
//                }));

//                $.each(data, function (index, date) {
//                    dateSelect.append($('<option/>', {
//                        value: date,
//                        text: date
//                    }));
//                });

//                dateSelect.val("");
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });
//    });


//    var usedStartTimes = {};
//    $("select[name='Schedules.StartTime'] > option").each(function () {
//        if (usedStartTimes[this.text]) {
//            $(this).remove();
//        } else {
//            usedStartTimes[this.text] = this.value;
//        }
//    });

//    $("#Schedules_Date").change(function () {
//        var selectedEmployee = $("#EmployeeServices_EmployeesId").val();
//        var selectedDate = $(this).val();

//        var usedStartTimes = {};

//        $('#Schedules_StartTime').empty();

//        $.ajax({
//            url: '/Schedules/GetStartTimes',
//            type: 'GET',
//            data: {
//                employeeId: selectedEmployee,
//                selectedDate: selectedDate
//            },
//            success: function (data) {
//                var startTimeSelect = $('#Schedules_StartTime');

//                startTimeSelect.append($('<option/>', {
//                    value: "",
//                    text: "Оберіть час",
//                    disabled: "disabled"
//                }));

//                $.each(data, function (index, startTime) {
//                    startTimeSelect.append($('<option/>', {
//                        value: startTime,
//                        text: startTime
//                    }));
//                });

//                startTimeSelect.val("");
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                console.log(textStatus, errorThrown);
//            }
//        });
//    });
//});


$(document).ready(function () {

    // Початкове завантаження
    $.getJSON("/Reservations/GetTypeServices", function (data) {
        var items = "<option class='disable-option' value='' disabled selected>Оберіть категорію</option>";
        $.each(data, function (i, typeService) {
            items += "<option value='" + typeService.value + "'>" + typeService.text + "</option>";
        });
        $("#TypeServicesId").html(items);
        $("#ServicesId").prop("disabled", true);
        $("#EmployeeServicesId").prop("disabled", true);
        $("#SchedulesDate").prop("disabled", true);
        $("#SchedulesStartTime").prop("disabled", true);
    });

    $("#TypeServicesId").change(function () {
        var typeId = $(this).val();
        $.getJSON("/Reservations/GetServices", { typeId: typeId }, function (data) {
            var items = "<option class='disable-option' value='' disabled selected>Оберіть послугу</option>";
            $.each(data, function (i, service) {
                items += "<option value='" + service.value + "'>" + service.text + "</option>";
            });
            $("#ServicesId").html(items);
            $("#ServicesId").prop("disabled", false);

            $("#EmployeeServicesId").html('');
            $("#EmployeeServicesId").prop("disabled", true);

            $("#SchedulesDate").html('');
            $("#SchedulesDate").prop("disabled", true);

            $("#SchedulesStartTime").html('');
            $("#SchedulesStartTime").prop("disabled", true);
        });
    });

    $("#ServicesId").change(function () {
        var serviceId = $(this).val();
        $.getJSON("/Reservations/GetEmployees", { serviceId: serviceId }, function (data) {
            var items = "<option class='disable-option' value='' disabled selected>Оберіть працівника</option>";
            $.each(data, function (i, employee) {
                items += "<option value='" + employee.value + "'>" + employee.text + "</option>";
            });
            $("#EmployeeServicesId").html(items);
            $("#EmployeeServicesId").prop("disabled", false);

            $("#SchedulesDate").html('');
            $("#SchedulesDate").prop("disabled", true);

            $("#SchedulesStartTime").html('');
            $("#SchedulesStartTime").prop("disabled", true);
        });
    });

    $("#EmployeeServicesId").change(function () {
        var employeeId = $(this).val();
        $.getJSON("/Reservations/GetDates", { employeeId: employeeId }, function (data) {
            var items = "<option class='disable-option' value='' disabled selected>Оберіть дату</option>";
            $.each(data, function (i, date) {
                items += "<option value='" + date.value + "'>" + date.text + "</option>";
            });
            $("#SchedulesDate").html(items);
            $("#SchedulesDate").prop("disabled", false);

            $("#SchedulesStartTime").html('');
            $("#SchedulesStartTime").prop("disabled", true);
        });
    });

    $("#SchedulesDate").change(function () {
        var dateId = $(this).val();
        $.getJSON("/Reservations/GetTimes", { dateId: dateId }, function (data) {
            var items = "<option class='disable-option' value='' disabled selected>Оберіть час</option>";
            $.each(data, function (i, time) {
                items += "<option value='" + time.value + "'>" + time.text + "</option>";
            });
            $("#SchedulesStartTime").html(items);
            $("#SchedulesStartTime").prop("disabled", false);
        });
    });


}); 