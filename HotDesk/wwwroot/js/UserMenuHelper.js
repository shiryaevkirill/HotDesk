
$(document).ready(function () {

    $("#workspaces").click(function (e) {

        $.ajax({
            url: "/User/WorkspacesView",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)

            }
        });


        $.ajax({
            url: "/User/Search",
            type: 'Get',
            success: function (response) {
                $("#SearchView").html(response)
            }
        });

        $("#result").width('60%');

        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");

    });

    $("#result").on("click", ".apply-workspace", function (e) {
        model = { "WorkspaceId": this.id}
        $.ajax({
            url: "/User/Apply",
            type: 'POST',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: 'application/json',
            success: function (response) {
                $.ajax({
                    url: "/User/WorkspacesView",
                    type: 'Get',
                    success: function (response) {
                        $("#result").html(response)
                    }
                });

            }
        });
    });


    $("#SearchView").on("click", ".add-device-btn", function (e) {
        var devices = $(".devices-section ul");

        devices.append('<li class="' + this.id + '">' + $(this).text() + ' <span class="cross remove-device" id="' + this.id + '"></span></li>');

        $(this).css("display", "none");

    });

    $("#SearchView").on("click", ".remove-device", function (e) {
        $("." + this.id).remove();
        $("li#" + this.id).css("display", "block");
    });


    $("#SearchView").on("click", ".search-btn", function (e) {

            var devicesType = "";
            $(".devices-section ul li").each(function () {

                devicesType += ($(this).attr("class") + ";");

            });


            var model = { StartDate: $("#StartDate").val(), DevicesType: devicesType };

            $.ajax({
                url: "/User/UseSearch",
                type: 'Post',
                data: JSON.stringify(model),
                contentType: 'application/json',
                success: function (response) {
                    $("#result").html(response);
                },
                error: function (data) {
                    console.log(JSON.stringify(data));
                    console.log('test 1 =' + data.responseText);
                    alert('error');
                }
            });
    });


    $("#my-workspace").click(function (e) {

        $.ajax({
            url: "/User/MyWorkspacesView",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)

            }
        });

        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");

    });
});