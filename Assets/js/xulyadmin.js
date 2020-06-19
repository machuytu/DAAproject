let setid = null;

function getid(setid) {
    setid = setid;
    console.log(setid);
}
function edituser(setid) {
    setid = setid;
    window.location.href = "/Admin/sinhviens/Edit/"+setid;
}
function detailuser(setid) {
    setid = setid;
    window.location.href = "/Admin/sinhviens/Details/" + setid;
}
$(document).ready(function () {


    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.avatar').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }


    $(".file-upload").on('change', function () {
        readURL(this);
    });
});