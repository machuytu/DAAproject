let indexid = null;

function getid(setid) {
    indexid = setid;
    console.log(setid);
}
// edit
function edituser(setid) {
    indexid = setid;
    window.location.href = "/Admin/sinhviens/Edit/" + setid;
}
function editgiangvien(setid) {
    indexid = setid;
    window.location.href = "/Admin/giangviens/Edit/" + setid;
}
function editdangkyhocphan(setid) {
    indexid = setid;
    window.location.href = "/Admin/dangkyhocphans/Edit/" + setid;
}
function edithoc(setid) {
    indexid = setid;
    window.location.href = "/Admin/hocs/Edit/" + setid;
}
function editkhoa(setid) {
    indexid = setid;
    window.location.href = "/Admin/khoas/Edit/" + setid;
}
function editlopcn(setid) {
    indexid = setid;
    window.location.href = "/Admin/lopcns/Edit/" + setid;
}
function editlop(setid) {
    indexid = setid;
    window.location.href = "/Admin/lops/Edit/" + setid;
}
function editmon(setid) {
    indexid = setid;
    window.location.href = "/Admin/mons/Edit/" + setid;
}
function editsinhvien(setid) {
    indexid = setid;
    window.location.href = "/Admin/sinhviens/Edit/" + setid;
}
function edittaikhoan(setid) {
    indexid = setid;
    window.location.href = "/Admin/taikhoans/Edit/" + setid;
}
function editthongbao(setid) {
    indexid = setid;
    window.location.href = "/Admin/thongbaos/Edit/" + setid;
}

// detail
function detailuser(setid) {
    indexid = setid;
    window.location.href = "/Admin/sinhviens/Details/" + setid;
}
function detailgiangvien(setid) {
    indexid = setid;
    window.location.href = "/Admin/giangviens/Details/" + setid;
}
function detaildangkyhocphan(setid) {
    indexid = setid;
    window.location.href = "/Admin/dangkyhocphans/Details/" + setid;
}
function detailhoc(setid) {
    indexid = setid;
    window.location.href = "/Admin/hocs/Details/" + setid;
}
function detailkhoa(setid) {
    indexid = setid;
    window.location.href = "/Admin/khoas/Details/" + setid;
}
function detaillopcn(setid) {
    indexid = setid;
    window.location.href = "/Admin/lopcns/Details/" + setid;
}
function detaillop(setid) {
    indexid = setid;
    window.location.href = "/Admin/lops/Details/" + setid;
}
function detailmon(setid) {
    indexid = setid;
    window.location.href = "/Admin/mons/Details/" + setid;
}
function detailsinhvien(setid) {
    indexid = setid;
    window.location.href = "/Admin/sinhviens/Details/" + setid;
}
function detailtaikhoan(setid) {
    indexid = setid;
    window.location.href = "/Admin/taikhoans/Details/" + setid;
}
function detailthongbao(setid) {
    indexid = setid;
    window.location.href = "/Admin/thongbaos/Details/" + setid;
}

// delete
function deleteuser() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/sinhviens/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                console.log('thanhcgn');
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });
    // console.log('abcd')

}

function deletegiangvien() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/giangviens/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletedangkyhocphan() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/dangkyhocphans/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletehoc() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/hocs/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletekhoa() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/khoas/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletelopcn() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/lopcns/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletelop() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/lops/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletemon() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/mons/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletesinhvien() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/sinhviens/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletetaikhoan() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/taikhoans/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function deletethongbao() {
    var id = indexid;
    console.log(indexid);
    //alert(id);
    $.ajax({
        url: '/Admin/thongbaos/Deleteuser/' + id,
        data: id,
        dataType: 'json',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.success) {
                location.reload();
            } else {
                // DoSomethingElse()
                $("#exampleModal").modal('hide');

                $("#idThongBaoXoaSV").modal('show');
            }
        } // then reload the page.(3)
    });

}

function refreshPage() {
    location.reload(true);
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

