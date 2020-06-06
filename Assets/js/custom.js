$(document).ready(function () {
    $("#hsck").mouseleave(function () {
        let hsck = $("#hsck").val();
        let hsgk = 1 - hsck;
        hsgk = Math.round(hsgk * 10) / 10;
        $("#hsgk").val(hsgk);
    });
});

