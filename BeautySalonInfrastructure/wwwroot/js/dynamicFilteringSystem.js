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
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });
});
