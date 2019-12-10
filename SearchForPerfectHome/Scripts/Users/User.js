$(document).ready(function () {
    $("#university-name").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#base-url").val() + "Users/AutoCompleteUniversity",
                type: "POST",
                dataType: "json",
                data: {
                    keyword: request.term
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item,
                            value: item
                        };
                    }))
                },
                error: function () {
                    alert('something went wrong !');
                }
            })
        },
        minLength: 2,
        open: function () {
            $('.ui-autocomplete').css('width', '18.5%');
        }
    });

    $(".user-details-btn").click(function (e) {

        e.preventDefault();
        var userid = $(this).closest('tr').find("td").find('input[type=hidden]')[0].value;

        $('#user-details-modal').load("/Users/GetUserDetailsByUserId/?userid=" + userid);
        $('#user-details-modal').modal('show');
    })
})