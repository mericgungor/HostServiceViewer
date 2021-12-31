var apiUrl = "http://localhost:8081/api";
var testData = {};

function fillTable(data) {
    clearData();
    testData = data;

    for (var i = 0; i < data.length; i++) {
        var serviceDescription = "";
        var id = data[i].mongoDbId;
        if (data[i].type == "ping") {
            serviceDescription = data[i].ip;
        } else if (data[i].type == "telnet") {
            serviceDescription = data[i].ip + ":" + data[i].port;
        } else if (data[i].type == "page") {
            serviceDescription = data[i].url;
        }
        $(".tbl tbody").append("<tr id='tr" + id + "'><td>" + data[i].name + "</td><td>" + data[i].type + "</td><td>" + serviceDescription + "</td><td>" + data[i].timer + "</td><td><button type='button' class='btn btn-info' onClick=\"btnUpdateOnClick('" + id + "')\" >Güncelle</button></td><td><button type='button' class='btn btn-danger' onClick=\"btnDeleteOnClick('" + id + "')\" >Sil</button></td></tr>")
    }

}

function clearData() {
    testData = {};
    $(".tbl tbody tr").remove();
}

function clearForm() {
    $("#tblForm input,#ddlType").val("");
    $("#ddlType").trigger("change");
}

function getTestList() {
    $.ajax(
        {
            type: "GET",
            url: apiUrl + "/selectall",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data, status) {
                //console.log(data);
                //console.log(status);
                if (data.length > 0) {
                    fillTable(data);
                }
            },
            error: function (data) {
                console.log("Error:")
                console.log(data);
                console.log(status);
            }
        });
}

function ddlTypeOnChange() {
    console.log($("#ddlType").val());
    $(".tr-service").hide();
    if ($("#ddlType").val() == "") {

    } else if ($("#ddlType").val() == "ping") {
        $(".tr-ip").show();
    } else if ($("#ddlType").val() == "telnet") {
        $(".tr-telnet").show();
    } else if ($("#ddlType").val() == "page") {
        $(".tr-page").show();
    }
}

function btnDeleteOnClick(id) {

    //var objectId = testData.find(x => x.id.increment == id);


    var ans = window.confirm("Silmek istediğinizden emin misiniz?");
    if (ans) {
        var postData = {
            "MongoDbId": id
        }


        $.ajax(
            {
                type: "POST",
                url: apiUrl + "/delete",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(postData),
                cache: false,
                success: function (data, status) {
                    $("#tr" + id).remove();
                    //clearData();
                    //getTestList();
                    //alert("Silindi")
                },
                error: function (data) {
                    console.log("Error:")
                    console.log(data);
                    console.log(status);
                }
            });
    }

}
function fillForm(data) {
    $("#hdMongoDbId").val(data.mongoDbId);
    $("#txtName").val(data.name);
    $("#txtDescription").val(data.description);
    $("#ddlType").val(data.type);
    $("#ddlType").trigger("change");
    $("#txtIp").val(data.ip);
    $("#txtTimer").val(data.timer);
    $("#txtPort").val(data.port);
    $("#txtPortName").val(data.portName);
    $("#txtUrl").val(data.url);
    $("#ddlActive").val(data.active.toString());
    $("#txtOrder").val(data.order);



}

function btnCancelOnClick() {
    clearForm();
}
function btnUpdateOnClick(id) {

    var data = testData.find(x => x.mongoDbId == id);
    //console.log(data);

    fillForm(data);
}

function btnSaveOnClick() {

    if ($("#txtName").val() == "") {
        alert("Ad Alanını Doldurunuz");
        $("#txtName").focus();
        return false;
    }
    if ($("#ddlType").val() == "") {
        alert("Servis Seçiniz");
        $("#ddlType").focus();
        return false;
    }
    if ($("#txtTimer").val() == "") {
        alert("Sn Alanını Doldurunuz");
        $("#txtTimer").focus();
        return false;
    }
    if ($("#ddlType").val() == "ping" && $("#txtIp").val() == "") {
        alert("Ip Alanını Doldurunuz");
        $("#txtIp").focus();
        return false;
    } else if ($("#ddlType").val() == "telnet" && ($("#txtIp").val() == "" || $("#txtPort").val() == "")) {
        alert("Ip ve Port Alanını Doldurunuz");
        return false;
    } else if ($("#ddlType").val() == "page" && $("#txtUrl").val() == "") {
        alert("Url Alanını Doldurunuz");
        $("#txtUrl").focus();
        return false;
    }


    var postData = {
        "Name": $("#txtName").val(),
        "Description": $("#txtDescription").val(),
        "Type": $("#ddlType").val(),
        "Ip": $("#txtIp").val(),
        "Timer": eval($("#txtTimer").val()) || 1,
        "Port": eval($("#txtPort").val()) || 1,
        "PortName": $("#txtPortName").val(),
        "Url": $("#txtUrl").val(),
        "Active": eval($("#ddlActive").val()),
        "Order": eval($("#txtOrder").val()) || 1
    }

    if ($("#hdMongoDbId").val() == "") {
        url = apiUrl + "/insert";
    }
    else {
        postData.MongoDbId = $("#hdMongoDbId").val();
        url = apiUrl + "/update";
    }

    $.ajax(
        {
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(postData),
            cache: false,
            success: function (data, status) {
                //console.log(data);
                clearForm();
                getTestList();
            },
            error: function (data) {
                console.log("Error:")
                console.log(data);
                console.log(status);
            }
        });

}

$(function () {
    getTestList();
});
