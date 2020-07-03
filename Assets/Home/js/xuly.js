$(document).ready(function () {
    //$('#example').DataTable();
    var listid = [];

    //dkhp
    $(".tablerow").click(function (event) {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }
    });
    
    $('.checkeditem :checkbox').change(function () {
        // this will contain a reference to the checkbox   
        var idlop = $(this).val();
        if (this.checked) {
            // the checkbox is now checked
            $(this).closest('tr').addClass("table-active");
            listid.push(idlop);
            $('#clear').hide();
            $('#dkhp').prop('disabled', false);
            $('#huydkhp').prop('disabled', false);
            var malop = $(this).attr("data-malop");
            var tengv = $(this).attr("data-tengv");
            var mamon = $(this).attr("data-mamon");
            var tenmon = $(this).attr("data-tenmon");
            var tc = $(this).attr("data-tc");
            var ca = $(this).attr("data-ca");
            $('#danhsach').append("<tr id='dachon" + idlop + "'><td>" + malop + "</td><td>" + mamon + "</td><td>" + tenmon + "</td><td>" + tengv + "</td><td>" + tc + "</td><td>" + ca + "</td></tr>");
        } else {
            // the checkbox is now no longer checked
            $(this).closest('tr').removeClass("table-active");
            listid.splice(listid.indexOf(idlop), 1);
            $('#dachon' + idlop).remove();
            if (listid.length == 0) {
                $('#clear').show();
                $('#dkhp').prop("disabled", true);
                $('#huydkhp').prop("disabled", true);
            }
        }
    });

    $('#dkhp').click(function () {
        FuncTkbAjax('/DKHP/AddHoc');
    });

    $('#huydkhp').click(function () {
        FuncTkbAjax('/DKHP/DeleteHoc');
    });

    function FuncTkbAjax(ajaxurl) {
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
    }

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
        $("#loidkhp").append("</br>");
    }

    var divNhapDiem = $("#NhapDiem");
    divNhapDiem.hide();
    $("#btnNhapDiem").click(function () {
        //alert();
        divNhapDiem.toggle();
    });

});