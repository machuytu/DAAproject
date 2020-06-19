﻿$(document).ready(function () {
    //$('#example').DataTable();

    var listid = [];
    
    $('.checkeditem :checkbox').change(function () {
        // this will contain a reference to the checkbox   
        var idlop = $(this).val();

        if (this.checked) {
            $('#clear').hide();
            $('#submit').prop('disabled', false);
            var malop = $(this).attr("data-malop");
            var giangvien = $(this).attr("data-tengv");
            var tenmon = $(this).attr("data-tenmon");
            var tkb = $(this).attr("data-tkb");
            $('#danhsach').append("<tr id='dachon" + idlop + "'><td>" + malop + "</td><td>" + giangvien + "</td><td>" + tenmon + "</td><td>" + tkb + "</td></tr>");
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
        $.ajax({
            url: '/DKHP/AddHoc',
            data: { listid: JSON.stringify(listid) },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    listid = [];
                }
            }
        });
        
    });
});