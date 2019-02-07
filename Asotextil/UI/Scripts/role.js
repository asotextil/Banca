$(document).ready(function () {
    // Enable Live Search.  
    $('#CountryList').attr('data-live-search', true);

    //// Enable multiple select.  
    $('#CountryList').attr('multiple', true);
    $('#CountryList').attr('data-selected-text-format', 'count');

    $('.selectCountry').selectpicker(
        {
            width: '100%',
            title: '- [Seleccione los Roles] -',
            style: 'btn-warning',
            size: 6,
            iconBase: 'fa',
            tickIcon: 'fa-check'
        });
}); 