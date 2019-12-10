
$(document).ready(function () {

    $('.update-btn').click(function (e) {

        var imageId = $(this).closest('tr').find("td").find('input[type=hidden]')[1].value;
        window.location.href = $("#base-url").val() + "Property/GetPropertyInfoByImageId?imageId=" + imageId;

        //var ajaxurl = $("#base-url").val() + "Property/GetPropertyInfo";
        //$.ajax({
        //    url: ajaxurl,
        //    type: 'POST',
        //    data: { imageId: imageId },
        //    success: function (data) {
        //        //debugger;
        //        //$('#dvUserdetails').html(data);
        //    },
        //    error: function (data, status, jqXHR) {
        //        alert("Error");
        //    }
        //});

    });

    //delcaring global variable to hold primary key value.
    var proptyId;
    $('.delete-property-btn').click(function () {
        proptyId = $(this).attr('id');
        $('#deletePropertyModal').modal('show');
    });

    $('.property-delete-confirm').click(function () {
        if (proptyId != '') {
            $.ajax({
                url: $("#base-url").val() + "Property/DeleteProperty",
                data: { 'propertyId': proptyId },
                type: 'POST',
                success: function (data) {
                    if (data) {
                        //now re-using the boostrap modal popup to show success message.
                        //dynamically we will change background colour 
                        if ($('.property-modal-header').hasClass('alert-danger')) {
                            $('.property-modal-header').removeClass('alert-danger').addClass('alert-success');
                            //hide ok button as it is not necessary
                            $('.property-delete-confirm').css('display', 'none');
                        }
                        //$('.success-message').html('Record deleted successfully');
                        window.location.reload(true);
                    }
                }, error: function (err) {
                    if (!$('.property-modal-header').hasClass('alert-danger')) {
                        $('.property-modal-header').removeClass('alert-success').addClass('alert-danger');
                        $('.property-delete-confirm').css('display', 'none');
                    }
                    $('.property-success-message').html(err.statusText);
                }
            });
        }
    });

    //function to reset bootstrap modal popups
    $("#deletePropertyModal").on("hidden.bs.modal", function () {
        $(".property-modal-header").removeClass(' ').addClass('alert-danger');
        $('.property-delete-confirm').css('display', 'inline-block');
        $('.property-success-message').html('').html('Are you sure you wish to delete this record ?');
    });

})

