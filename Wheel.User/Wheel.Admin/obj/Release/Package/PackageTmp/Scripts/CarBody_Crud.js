$(function () {

    altair_crud_table.init();
}), altair_crud_table = {
    init: function () {
        var cachedModelOptions = null;
        $("#students_crud")
            .jtable({
                title: "The Car Bodies",
                paging: true,
                pageSize: 10,
                sorting: false,
                multiSorting: true,
                addRecordButton: $("#recordAdd"),
                deleteConfirmation: function(t) {
                    t.deleteConfirmMessage = "Are you sure to delete Car Body: " + t.record.Name + "?";
                },
                formCreated: function(t, e) {
                    e.form.find(".jtable-option-text-clickable")
                        .each(function() {
                            var t = $(this).prev().attr("id");
                            $(this)
                                .attr("data-click-target", t)
                                .off("click")
                                .on("click",
                                    function(t) {
                                        t.preventDefault(), $("#" + $(this).attr("data-click-target")).iCheck("toggle");
                                    });
                        }), e.form.find("select")
                        .each(function() {
                            var t = $(this);
                            t.after('<div class="selectize_fix"></div>')
                                .selectize({
                                    dropdownParent: "body",
                                    placeholder: "Click here to select ...",
                                    onDropdownOpen: function(t) {
                                        t.hide()
                                            .velocity("slideDown",
                                            {
                                                begin: function() {
                                                    t.css({
                                                        "margin-top": "0"
                                                    });
                                                },
                                                duration: 200,
                                                easing: easing_swiftOut
                                            });
                                    },
                                    onDropdownClose: function(t) {
                                        t.show()
                                            .velocity("slideUp",
                                            {
                                                complete: function() {
                                                    t.css({
                                                        "margin-top": ""
                                                    });
                                                },
                                                duration: 200,
                                                easing: easing_swiftOut
                                            });
                                    }
                                });
                        }), e.form.find('input[type="checkbox"],input[type="radio"]')
                        .each(function() {
                            var t = $(this);
                            t.iCheck({
                                    checkboxClass: "icheckbox_md",
                                    radioClass: "iradio_md",
                                    increaseArea: "20%"
                                })
                                .on("ifChecked",
                                    function(e) {
                                        t.parent("div.icheckbox_md").next("span").text("Active");
                                    })
                                .on("ifUnchecked",
                                    function(e) {
                                        t.parent("div.icheckbox_md").next("span").text("Passive");
                                    });
                        }), e.form.find(".jtable-input")
                        .children('input[type="text"],input[type="password"],textarea')
                        .not(".md-input")
                        .each(function() {
                            $(this).addClass("md-input"), altair_forms.textarea_autosize();
                        }), altair_md.inputs();
                },
                actions: {
                    listAction: function(postData, jtParams) {
                        return $.Deferred(function($dfd) {
                            $.ajax({
                                url: '/carBody/CarBodyList',
                                type: 'post',
                                dataType: 'json',
                                data: postData,
                                success: function(data) {
                                    $dfd.resolve(data);
                                },
                                error: function() {
                                    $dfd.reject();

                                }
                            });
                        });
                    },
                    createAction: function(postData, jtParams) {
                        return $.Deferred(function($dfd) {
                            $.ajax({
                                url: '/carBody/CreateCarBody',
                                type: 'post',
                                dataType: 'json',
                                data: postData,
                                success: function(data) {

                                    $dfd.resolve(data);
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a> Car Body added succesfuly";
                                    var status = "success";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);

                                },
                                error: function(data) {
                                    $dfd.reject();
                                    console.log(data);
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a>" + data.responseJSON.Message;
                                    var status = "danger";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);
                                }
                            });
                        });
                    },
                    updateAction: function(postData, jtParams) {
                        return $.Deferred(function($dfd) {
                            $.ajax({
                                url: '/carBody/UpdatecarBody',
                                type: 'post',
                                dataType: 'json',
                                data: postData,
                                success: function(data) {
                                    $dfd.resolve(data);
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a>  carBody Updated succesfuly";
                                    var status = "success";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);

                                },
                                error: function() {
                                    $dfd.reject();
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a> Failed! Something was wrong";
                                    var status = "danger";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);
                                }
                            });
                        });
                    },
                    deleteAction: function(postData, jtParams) {
                        console.log(postData);
                        return $.Deferred(function($dfd) {
                            $.ajax({
                                url: '/carBody/DeleteCarBody',
                                type: 'post',
                                dataType: 'json',
                                data: postData,
                                success: function(data) {
                                    $dfd.resolve(data);
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a> carBody Deleted  succesfuly";
                                    var status = "success";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);

                                },
                                error: function() {
                                    $dfd.reject();
                                    var message =
                                        "<a href='#' class='notify-action'>Clear</a> Failed! Something was wrong";
                                    var status = "danger";
                                    var pos = "top-right";
                                    showNotification(message, status, pos);
                                }
                            });
                        });
                    }
                },
                fields: {
                    Id: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    ModelId: {
                        title: 'Models',
                        width: '12%',
                        options: function() {

                            if (cachedModelOptions) { //Check for cache
                                return cachedModelOptions;
                            }

                            var options = [];

                            $.ajax({ //Not found in cache, get from server
                                url: '/carBody/GetModelOptions',
                                type: 'POST',
                                dataType: 'json',
                                async: false,
                                success: function(data) {
                                    if (data.Result != 'OK') {
                                        alert(data.Message);
                                        return;
                                    }
                                    options = data.Options;
                                }
                            });

                            return cachedModelOptions = options; //Cache results and return options
                        }
                    },
                    Year: {
                        title: "Year",
                        width: "23%"
                    },
                    Name: {
                        title: "Name",
                        width: "23%"
                    },
                    Width: {
                        title: "Width"
                        
                    },
                    Height: {
                        title: "Height"

                    },
                    Radius: {
                        title: "Radius"

                    },
                    Description: {
                        title: "Description",
                        type: "textarea"
                    }

                }
            });
             $(".ui-dialog-buttonset")
            .children("button")
            .attr("class", "")
            .addClass("md-btn md-btn-flat")
            .off("mouseenter focus"), $("#AddRecordDialogSaveButton,#EditDialogSaveButton,#DeleteDialogButton")
            .addClass("md-btn-flat-primary");
        //Re-load records when user click 'load records' button.
        $('#LoadRecordsButton').click(function (e) {
            e.preventDefault();
            console.log($('#name').val());
            $('#students_crud').jtable('load', {

                name: $('#name').val(),

                year: $('#year').val()
            });
        });

        //Load all records when page is first shown
        $('#LoadRecordsButton').click();


    }
};