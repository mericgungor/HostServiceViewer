var testData = {};
var timeoutHandles = [];
var status = true;
var apiUrl = "http://localhost:8081/api";

function fillTable(data) {
    clearData();

    testData = data;

    for (var i = 0; i < testData.length; i++) {
        var serviceDescription = "";
        if (testData[i].type == "ping") {
            serviceDescription = testData[i].ip;
        } else if (testData[i].type == "telnet") {
            serviceDescription = testData[i].ip + ":" + testData[i].port;
        } else if (testData[i].type == "page") {
            serviceDescription = testData[i].url;
        }
        $(".tbl tbody").append("<tr id='tr" + testData[i].id.increment + "'><td>" + testData[i].name + "</td><td>" + testData[i].type + "</td><td>" + serviceDescription + "</td><td>" + testData[i].timer + "</td><td class='status'></td></tr>")
    }

    startHostServiceViewer();

}

function clearData() {
    $(".tbl tbody tr").remove();
    testData = {};
}
function startHostServiceViewer() {


    for (var i = 0; i < testData.length; i++) {
        var mstimer = testData[i].timer * 1000;
        if (testData[i].type == "ping") {
            ping(testData[i].id.increment, testData[i].ip, mstimer);
        } else if (testData[i].type == "telnet") {
            telnet(testData[i].id.increment, testData[i].ip, testData[i].port, mstimer);
        } else if (testData[i].type == "page") {
            page(testData[i].id.increment, testData[i].url, mstimer);
        }
    }

}

function setupTimeOut(id, code, time) {
    if (id in timeoutHandles) {
        clearTimeout(timeoutHandles[id])
    }

    timeoutHandles[id] = setTimeout(code, time)
}

function ping(id, ip, mstimer) {
    setupTimeOut(id, "ping(\"" + id + "\",\"" + ip + "\"," + mstimer + ")", mstimer);

    var strTime = moment().format('HH:mm:ss ');
    var $tr = $("#tr" + id);
    $tr.find("td.status").html("taranıyor " + strTime);
    $('#dvScanningNow').html(ip + " taranıyor");

    ajaxGet($tr, apiUrl+"/ping?ip=" + ip);
}

function telnet(id, ip, port, mstimer) {
    setupTimeOut(id, "telnet(\"" + id + "\",\"" + ip + "\",\"" + port + "\"," + mstimer + ")", mstimer);

    var strTime = moment().format('HH:mm:ss ');
    var $tr = $("#tr" + id);
    $tr.find("td.status").html("taranıyor " + strTime);

    $('#dvScanningNow').html(ip + ":" + port + " taranıyor");

    ajaxGet($tr, apiUrl+"/telnet?ip=" + ip + "&port=" + port);


}

function page(id, url, mstimer) {
    setupTimeOut(id, "page(\"" + id + "\",\"" + url + "\"," + mstimer + ")", mstimer);

    var strTime = moment().format('HH:mm:ss ');

    var $tr = $("#tr" + id);
    $tr.find("td.status").html("taranıyor " + strTime);

    $('#dvScanningNow').html(url + " taranıyor");

    ajaxGet($tr, apiUrl +"/page?url=" + url);
}
function ajaxGet($tr, url) {
    $.ajax(
        {
            type: "GET",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (result) {
                $tr.attr("status", result);
                if (result) {
                    $tr.removeClass("tr-error");
                } else {
                    $tr.addClass("tr-error");
                }
            },
            error: function (data) {
                $tr.attr("status", false);
                $tr.addClass("tr-error");
            }
        }).always(function () {
            checkSystem();
        });
}
function checkSystem() {
    if ($("tr[status=false]").length > 0) {
        systemError();
    } else {
        systemOk();
    }
}
function systemOk() {
    status = true;
    $("#dvTopMessage div.ui-state").removeClass("state-error");
    $("#dvTopMessage div.ui-state").addClass("state-ok");
    $("#dvTopMessage div.ui-state").text("SYSTEM ONLINE");
}
function systemError() {
    status = false;
    $("#dvTopMessage div.ui-state").removeClass("state-ok");
    $("#dvTopMessage div.ui-state").addClass("state-error");
    $("#dvTopMessage div.ui-state").text("SYSTEM ERROR");
}

function getTestList() {
    $.ajax(
        {
            type: "GET",
            url: "http://localhost:8081/api/list",
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

$(function () {

    getTestList();
});
