function clearTextbox() {
    $("#query").val("");
}

function disableAdd() {
    $("#btnAdd").attr("disabled",true);
}

function enableAdd() {
    $("#btnAdd").removeAttr("disabled");
}