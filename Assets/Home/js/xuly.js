$(document).ready(function () {
    //$('#example').DataTable();

    var listid = [];
    var ajaxurl = '';

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
        //alert(ajaxurl);

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
        //alert(listid);
        $("#loidkhp").html("");
        $.ajax({
            url: ajaxurl,
            data: { listid: JSON.stringify(listid) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    listid = [];
                    location.reload();
                } else {
                    $.each(res.status, function () {
                        $("#loidkhp").append(this + "</br>");

                    });
                    //let list = res.status;
                    //list.each(
                    //    $("#loidkhp").append($(this) + "</br>");
                    //);
                    //$("#loidkhp").append(res.status);
                }
                
            }
        });
    });
});