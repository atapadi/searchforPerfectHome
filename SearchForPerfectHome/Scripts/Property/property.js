
$(document).ready(function () {
    $(".prop-details-btn").click(function (e) {

        e.preventDefault();
        var propertyId = $(this).closest('tr').find("td").find('input[type=hidden]')[0].value;

        $('#property-details-modal').load("/Property/GetPropertyInfoByPropertyId/?propertyId=" + propertyId);
        $('#property-details-modal').modal('show');

    })
})
