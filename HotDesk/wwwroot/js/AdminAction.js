$(document).ready(function () {

    $("#add-employee").click(function (e) {
        console.log("suka");
        $.ajax({
            url: "/Admin/AddEmployee",
            type: 'Get',
            success: function (response) {
                $("#action").html(response)
            }
        });
    });

});
