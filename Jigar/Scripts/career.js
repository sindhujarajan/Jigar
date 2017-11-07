$(document).ready(function () {

});
function submitDetails() {
    var applicantModel = {
        name: $("#name").val(),
        mobile: $("#mobile").val(),
        email: $("#email").val(),
        detail: $("#experience").val()
    };

    var fullUrl = window.location.href;
    var pathName = window.location.pathname;
    var webUrl = fullUrl.replace(pathName, '');

    $.ajax({
        url: webUrl + "/Home/SendApplicantDetails",
        type: "POST",
        datatype: "json",
        cache: false,
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(applicantModel),
        success: function (data) {
            console.log("email sent successfully");
        },
        error: function (data) {
            console.log("error occured" + data);
        }
        

    });
}