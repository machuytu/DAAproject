$(document).ready(function () {
    //$('#example').DataTable();
    var listid = [];
    var ajaxurl = '';

    //dkhp
    $('.checkeditem :checkbox').change(function () {
        // this will contain a reference to the checkbox   
        
        var idlop = $(this).val();
        var pathname = window.location.pathname;
        if (pathname == '/dkhp') {
            ajaxurl = '/DKHP/AddHoc'
        }
        else if (pathname == '/huydkhp') {
            ajaxurl = '/DKHP/DeleteHoc'
        }

        if (this.checked) {
            $('#clear').hide();
            $('#submit').prop('disabled', false);
            var malop = $(this).attr("data-malop");
            var giangvien = $(this).attr("data-tengv");
            var tenmon = $(this).attr("data-tenmon");
            var tc = $(this).attr("data-tc");
            var tkb = $(this).attr("data-tkb");
            $('#danhsach').append("<tr id='dachon" + idlop + "'><td>" + malop + "</td><td>" + giangvien + "</td><td>" + tenmon + "</td><td>" + tc + "</td><td>" + tkb + "</td></tr>");
            // the checkbox is now checked 
            listid.push(idlop);
        } else {
            listid.splice(listid.indexOf(idlop), 1);
            // the checkbox is now no longer checked
            $('#dachon' + idlop).remove();
            if (listid.length == 0) {
                $('#clear').show();
                $('#submit').prop("disabled", true);
            }
        }
    });

    $('#submit').click(function () {
        $.ajax({
            url: ajaxurl,
            data: { listid: JSON.stringify(listid) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                listid = [];
                if (res.status == true) {
                    localStorage.setItem("listsuc", JSON.stringify(res.listsuc));
                    localStorage.setItem("listerr", JSON.stringify(res.listerr));
                    location.reload();
                } else {
                    location.reload();
                }
            }
        });
    });

    if (localStorage.getItem("listsuc") && localStorage.getItem("listerr")) {
        let listsuc = JSON.parse(localStorage.getItem("listsuc"));
        let listerr = JSON.parse(localStorage.getItem("listerr"));
        localStorage.clear();
        $.each(listsuc, function (key, val) {
            $("#thanhcong").append(val + "</br>");
        });
        $.each(listerr, function (key, val) {
            $("#loidkhp").append(val + "</br>");
        });

    }
});