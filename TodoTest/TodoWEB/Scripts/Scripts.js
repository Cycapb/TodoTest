function clearTextbox() {
    $("#query").val("");
}

function disableAdd() {
    $("#btnAdd").prop("disabled",true);
}

function enableAdd() {
    $("#btnAdd").prop("disabled", false);
}