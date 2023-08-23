
$(document).on('change', '#ProductGroupId', function () {
    $.ajax({
        type: 'GET',
        url: '/Agreement/GetProductByGroup?id=' + $("#ProductGroupId").val(),
        contentType: "application/json",
        dataType: 'json',
        success: function (res) {
            html = "<option value='0'>Select one</option>";
            for (var i = 0; i < res.length; i++) {
                html += "<option value='" + res[i].id + "'>" + res[i].productNumber + "</option>";
            }

            $("#ProductId").html(html);
        }
    })
});

$(document).on('change', '.form-control', function () {
    if ($(this).val() || $(this).is(':checked')) {
        $(this).removeClass("border-color");
        if ($(this).attr("id") == "ProductPrice" || $(this).attr("id") == "NewPrice") {
            $(this).next().next().text("");
        }
        else
            $(this).next().text("");
    }
});

$(document).on('change', '.form-check-input', function () {
    if ($(this).is(':checked')) {
        $(this).removeClass("border-color");
        $(this).next().text("");
    }
});

$(document).ready(function () {
    $("#agreementManagement").DataTable();

    showInPopup = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#form-modal .modal-body').html(res);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
            }
        })
    }

    submitForm = (e, form) => {
        e.preventDefault();
        if (validateForm()) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            window.location.reload();
                        }
                        else
                            $('#form-modal .modal-body').html(res.html);
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
                //to prevent default form submit event
                return false;
            } catch (ex) {
                console.log(ex)
            }
        }
    }


    function validateForm() {
        var isValid = true;
        $("#ProductGroupId").next().text("");
        $("#ProductId").next().text("");
        $("#EffectiveDate").next().text("");
        $("#ExpirationDate").next().text("");
        $("#ProductPrice").next().text("");
        $("#NewPrice").next().text("");

        if (!$("#ProductGroupId").val() || $("#ProductGroupId").val() == "Select one" || $("#ProductGroupId").val() == "0") {
            $("#ProductGroupId").addClass("border-color")
            $("#ProductGroupId").next().text("Product Group field is required!");
            isValid = false;
        }
        if (!$("#ProductId").val() || $("#ProductId").val() == "Select one" || $("#ProductId").val() == "0") {
            $("#ProductId").addClass("border-color")
            $("#ProductId").next().text("Product field is required!");
            isValid = false;
        }
        if (!$("#EffectiveDate").val()) {
            $("#EffectiveDate").addClass("border-color")
            $("#EffectiveDate").next().text("Effective Date field is required!");
            isValid = false;
        }
        if (!$("#ExpirationDate").val()) {
            $("#ExpirationDate").addClass("border-color")
            $("#ExpirationDate").next().text("Expiration Date field is required!");
            isValid = false;
        }
        if (!parseFloat($("#ProductPrice").val())) {
            $("#ProductPrice").addClass("border-color")
            $("#ProductPrice").next().next().text("Product Price field is required!");
            isValid = false;
        }
        if (!parseFloat($("#NewPrice").val())) {
            $("#NewPrice").addClass("border-color")
            $("#NewPrice").next().next().text("New Price field is required!");
            isValid = false;
        }

        if (!$("#Active").is(':checked')) {
            $("#Active").addClass("border-color")
            $("#Active").next().text("Active field is required!");
            isValid = false;
        }

        return isValid;

    }

    deleteAgreement = form => {
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        window.location.reload();
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }
});