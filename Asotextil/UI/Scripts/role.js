$(document).ready(function () {
    // Enable Live Search.  
    $('#RolList').attr('data-live-search', true);

    //// Enable multiple select.  
    $('#RolList').attr('multiple', true);
    $('#RolList').attr('data-selected-text-format', 'count');

    $('.selectRol').selectpicker(
        {
            width: '100%',
            title: '- [Seleccione los Roles] -',
            style: 'btn-warning',
            size: 6,
            iconBase: 'fa',
            tickIcon: 'fa-check'
        });
}); 