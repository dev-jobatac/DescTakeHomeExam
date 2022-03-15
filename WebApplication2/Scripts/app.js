$('#btnTimeIn').click(function () {

    let email = $('#Email').val();


    $.ajax({
        type: "POST",
        url: '/TimeLog/SaveTimeLog',
        data: { type: "timein", email: email },
        //contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response);

            if (response.success) {
                alert("Success");
                document.getElementById('logoutForm').submit();
            }
            else {
                alert(response.message);
            }

        }
    });

});
$('#btnTimeOut').click(function () {

    let email = $('#Email').val();


    $.ajax({
        type: "POST",
        url: '/TimeLog/SaveTimeLog',
        data: { type: "timeout", email: email },
        //contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response);

            if (response.success) {
                alert("Success");
                document.getElementById('logoutForm').submit();
            }
            else {
                alert(response.message);
            }

        }
    });

});

$('#SaveEditEmployee').click(function () {

    let firstName = $('#FirstName').val();
    let lastName = $('#LastName').val();
    let email = $('#Email').val();
    if (firstName === "" || lastName === "" || email ==="") {
        alert("Invalid data");
        return;
    }


    let model = {
        Id: $('#Id').val(),
        UserTypeId: $('#UserTypeId').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Password: $('#Password').val()
    };

    $.ajax({
        type: "POST",
        url: '/MasterData/Save',
        data: { model: model },
        //contentType: "application/json; charset=utf-8",
        success: function (response) {
            window.location = "/MasterData";
        }
    });
    console.log(model);
});



$('#btnAddEmployee').click(function () {
    window.location = "/MasterData/EditEmployee?id=";
});