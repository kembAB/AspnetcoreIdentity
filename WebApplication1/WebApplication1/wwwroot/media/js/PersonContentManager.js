var elementToChange = "#result"

function GetPerson() {
    let id = document.getElementById("personIDNumber").value;
    console.log(elementToChange);
    $.get("/AJAX/Get?personid=" + id, function (data, status) {
        console.log(status);
        if (status == "success") {
            $(elementToChange).html(data);
        }

    })
    console.log("Finished");
}

function GetAll() {
    $.get("/AJAX/GetAll", function (data, status) {
        $(elementToChange).html(data);
    })
}

function DeletePerson() {
    let id = document.getElementById("personIDNumber").value;
    $.post("/AJAX/Delete?personid=" + id, function (data, status) {
         $(elementToChange).html(data)
    })
}