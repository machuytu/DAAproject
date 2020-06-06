$(document).ready(function () {
    $("#hsgk").val("vui lòng nhập hệ số cuối kỳ");
    $("#hsck").mouseleave(function () {
        var hsck = $("#hsck").val();
        var hsgk = 1 - hsck;
        hsgk = Math.round(hsgk * 10) / 10;
        $("#hsgk").val(hsgk);
    });
});

