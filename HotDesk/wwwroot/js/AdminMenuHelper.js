
$(document).ready(function () {

    $("#employees").click(function (e) {

        $.ajax({
            url: "/Admin/EmployeesEditor",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)

            }
        });


        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");

    });

    $("#result").on("click", "#add-employee", function (e) {
        $.ajax({
            url: "/Admin/AddEmployee",
            type: 'Get',
            success: function (response) {
                $("#action").html(response)

                $("#add-employee-form").removeData("validator");
                $("#add-employee-form").removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse("#add-employee-form");
            }
        });
    });

    $("#action").on("click", ".close-cross", function (e) {
        $('#action').empty();
    });

    $("#action").on("click", "#add-employee", function (e) {


        if ($("#add-employee-form").valid()) {
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();
            var model = {
                Name: $("#Name").val(), Surname: $("#Surname").val(), Login: $("#Login").val(), Password: $("#Password").val(), Role: $("#Role").val(),
            };

            $.ajax({
                url: "/Admin/AddEmployee",
                type: 'POST',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: 'application/json',
                headers:
                {
                    "RequestVerificationToken": token
                },
                success: function (response) {
                    if (response == true) {
                        $('#action').empty();

                        $.ajax({
                            url: "/Admin/EmployeesEditor",
                            type: 'Get',
                            success: function (response) {
                                $("#result").html(response)
                            }
                        });
                    }
                    else
                        $("#action").html(response.view)
                }
            });
        }
    });

    $("#result").on("click", ".delete-employee", function (e) {
        var id = this.id;
        $.ajax({
            url: "/Admin/ConfirmDelete",
            type: 'GET',
            success: function (response) {
                $("#action").html(response)

                var agree_btn = $("#action").find(".delete-agree");
                $(agree_btn).attr('id', id);
                $(agree_btn).attr('class', "delete-employee-agree");
            }
        });
    });

    $("#action").on("click", ".delete-fail", function (e) {
        $('#action').empty();
    });


    $("#action").on("click", ".delete-employee-agree", function (e) {
        model = { "Id": this.id, "Table": "Employee" }
        $.ajax({
            url: "/Admin/Delete",
            type: 'POST',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: 'application/json',
            success: function (response) {
                $('#action').empty();

                $.ajax({
                    url: "/Admin/EmployeesEditor",
                    type: 'Get',
                    success: function (response) {
                        $("#result").html(response)
                    }
                });
            },
            error: function (data) {
                console.log(JSON.stringify(data));
                console.log('test 1 =' + data.responseText);
                alert('error');
            }
        });
    });


    $("#roles").click(function (e) {

        $.ajax({
            url: "/Admin/RolesEditor",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)
            }
        });

        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");
    });

    $("#result").on("click", "#add-role-view", function (e) {
        $.ajax({
            url: "/Admin/AddRole",
            type: 'Get',
            success: function (response) {
                $("#action").html(response)

                $("#add-role-form").removeData("validator");
                $("#add-role-form").removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse("#add-role-form");
            }
        });
    });

    $("#action").on("click", "#add-role", function (e) {


        if ($("#add-role-form").valid()) {
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();
            var model = {RoleName: $("#RoleName").val()}

            $.ajax({
                url: "/Admin/AddRole",
                type: 'POST',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: 'application/json',
                headers:
                {
                    "RequestVerificationToken": token
                },
                success: function (response) {
                    if (response == true) {
                        $('#action').empty();

                        $.ajax({
                            url: "/Admin/RolesEditor",
                            type: 'Get',
                            success: function (response) {
                                $("#result").html(response)
                            }
                        });
                    }
                    else
                        $("#action").html(response.view)
                }
            });
        }
    });


    $("#result").on("click", ".delete-role", function (e) {
        var id = this.id;
        $.ajax({
            url: "/Admin/ConfirmDelete",
            type: 'GET',
            success: function (response) {
                $("#action").html(response)

                var agree_btn = $("#action").find(".delete-agree");
                $(agree_btn).attr('id', id);
                $(agree_btn).attr('class', "delete-role-agree");
            }
        });
    });


    $("#action").on("click", ".delete-role-agree", function (e) {
        model = { "Id": this.id, "Table": "Role" }
        $.ajax({
            url: "/Admin/Delete",
            type: 'POST',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: 'application/json',
            success: function (response) {
                $('#action').empty();

                $.ajax({
                    url: "/Admin/RolesEditor",
                    type: 'Get',
                    success: function (response) {
                        $("#result").html(response)
                    }
                });
            }
        });
    });


    $("#devices").click(function (e) {

        $.ajax({
            url: "/Admin/DevicesEditor",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)
            }
        });

        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");
    });


    $("#result").on("click", "#add-device-view", function (e) {
        $.ajax({
            url: "/Admin/AddDevice",
            type: 'Get',
            success: function (response) {
                $("#action").html(response)

                $("#add-device-form").removeData("validator");
                $("#add-device-form").removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse("#add-device-form");
            }
        });
    });



    $("#action").on("click", "#add-device", function (e) {
        if ($("#add-device-form").valid()) {
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();
            var model = {DeviceName: $("#DeviceName").val(), DeviceType: $("#DeviceType").val() };

            $.ajax({
                url: "/Admin/AddDevice",
                type: 'POST',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: 'application/json',
                headers:
                {
                    "RequestVerificationToken": token
                },
                success: function (response) {
                    if (response == true) {
                        $('#action').empty();

                        $.ajax({
                            url: "/Admin/DevicesEditor",
                            type: 'Get',
                            success: function (response) {
                                $("#result").html(response)
                            }
                        });
                    }
                    else
                        $("#action").html(response.view)
                }
            });
        }
    });


    $("#result").on("click", ".delete-device", function (e) {
        var id = this.id;
        $.ajax({
            url: "/Admin/ConfirmDelete",
            type: 'GET',
            success: function (response) {
                $("#action").html(response)

                var agree_btn = $("#action").find(".delete-agree");
                $(agree_btn).attr('id', id);
                $(agree_btn).attr('class', "delete-device-agree");
            }
        });
    });


    $("#action").on("click", ".delete-device-agree", function (e) {
        model = { "Id": this.id, "Table": "Device" }
        $.ajax({
            url: "/Admin/Delete",
            type: 'POST',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: 'application/json',
            success: function (response) {
                $('#action').empty();

                $.ajax({
                    url: "/Admin/DevicesEditor",
                    type: 'Get',
                    success: function (response) {
                        $("#result").html(response)
                    }
                });
            }
        });
    });



    $("#workspaces").click(function (e) {

        $.ajax({
            url: "/Admin/WorkspaceEditor",
            type: 'Get',
            success: function (response) {
                $("#result").html(response)
            }
        });

        var active = $(".active-nav");
        active.removeAttr("class");
        $(this).attr("class", "active-nav");
    });


    $("#result").on("click", "#add-workspace-view", function (e) {
        $.ajax({
            url: "/Admin/AddWorkspace",
            type: 'Get',
            success: function (response) {
                $("#action").html(response)

                $("#add-workspace-form").removeData("validator");
                $("#add-workspace-form").removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse("#add-workspace-form");
            }
        });
    });

    $("#action").on("click", ".add-device-btn", function (e) {
        var devices = $(".devices-section ul");

        devices.append('<li class="' + this.id + '">' + $(this).text() + ' <span class="cross remove-device" id="' + this.id+'"></span></li>');

        $(this).css("display", "none");

    });

    $("#action").on("click", ".remove-device", function (e) {
        $("." + this.id).remove();
        $("li#" + this.id).css("display", "block");
    });


    $("#action").on("click", "#add-workspace", function (e) {
        if ($("#add-workspace-form").valid()) {
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();

            var devicesId = "";

            $(".devices-section ul li").each(function () {

                devicesId += ($(this).attr("class") +";");

            });


            var model = { StartDate: $("#StartDate").val(), EndDate: $("#EndDate").val(), Description: $("#Description").val(), DevicesId: devicesId };

            $.ajax({
                url: "/Admin/AddWorkspace",
                type: 'POST',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: 'application/json',
                headers:
                {
                    "RequestVerificationToken": token
                },
                success: function (response) {
                    if (response == true) {
                        $('#action').empty();

                        $.ajax({
                            url: "/Admin/WorkspaceEditor",
                            type: 'Get',
                            success: function (response) {
                                $("#result").html(response)
                            }
                        });
                    }
                    else
                        $("#action").html(response.view)
                }
            });
        }
    });


    $("#result").on("click", ".delete-workspace", function (e) {
        var id = this.id;
        $.ajax({
            url: "/Admin/ConfirmDelete",
            type: 'GET',
            success: function (response) {
                $("#action").html(response)

                var agree_btn = $("#action").find(".delete-agree");
                $(agree_btn).attr('id', id);
                $(agree_btn).attr('class', "delete-workspace-agree");
            }
        });
    });


    $("#action").on("click", ".delete-workspace-agree", function (e) {
        model = { "Id": this.id, "Table": "Workspace" }
        $.ajax({
            url: "/Admin/Delete",
            type: 'POST',
            data: JSON.stringify(model),
            dataType: "json",
            contentType: 'application/json',
            success: function (response) {
                $('#action').empty();

                $.ajax({
                    url: "/Admin/WorkspaceEditor",
                    type: 'Get',
                    success: function (response) {
                        $("#result").html(response)
                    }
                });
            }
        });
    });


    $("#result").on("click", ".confirm-application-btn button", function (e) {
        model = { "OrderId": this.id }
        alert(this.id);
        $.ajax({
            url: "/Admin/ConfirmApplication",
            type: 'Post',
            data: JSON.stringify(model),
            contentType: 'application/json',
            success: function (response) {
                $("#result").html(response)
            }
        });
    });
});

