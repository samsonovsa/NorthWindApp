let productUri = 'https://localhost:44384/api/product';
let categoryUri = 'https://localhost:44384/api/category';

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(productUri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                $('<tr>').append(
                    $('<td>').text(item.name),
                    $('<td>').text(item.quantityPerUnit),
                    $('<td>').text(item.unitPrice),
                    $('<td>').text(item.unitsInStock)
                ).appendTo($('#products'));
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#products').text('Error: ' + err);
        });
});

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(categoryUri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                $('<tr>').append(
                    $('<td>').text(item.name),
                    $('<td>').text(item.description)
                ).appendTo($('#categories'));
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#categories').text('Error: ' + err);
        });
});

function formatItem(item) {
    return item.name + ': $' + item.unitPrice;
}

function find() {
    var id = $('#prodId').val();
    $.getJSON(productUri + '/' + id)
        .done(function (data) {
            $('#product').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error: ' + err);
        });
}