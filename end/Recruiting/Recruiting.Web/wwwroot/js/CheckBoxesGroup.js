$(document).ready(function () {
    $(document).on('change', '.form-check-input', function (event) {
        $('.elements-changed').val(true);
        var selectedChecks = $('.form-check-input').filter(function (index) { return $(this).prop('checked'); })
        $('.hidden-elements').val(selectedChecks.map(function () { return $(this).val(); }).get().join());
    });
});
function arrayRemove(arr, value) {
    return arr.filter(function (ele) { return ele != value; });
}