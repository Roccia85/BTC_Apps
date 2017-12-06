// Write your Javascript code.


$(document).ready(function () {

    $("#coinTable")
        .DataTable({
            "processing": true,
            "ajax": {
                "url": "https://api.coinmarketcap.com/v1/ticker/?convert=EUR&limit=300",
                "dataSrc": ""

            },
            "columns": [
                { "data": "name" },
                {
                    "data": "symbol",
                    "sClass": "center"
                },
                {
                    "data": "price_eur",
                    render: $.fn.dataTable.render.number('.', ',', 8, ''),
                    "sClass": "right"
                },
                {
                    "data": "price_btc",
                    render: $.fn.dataTable.render.number('.', ',', 8, ''),
                    "sClass": "right"
                },
                {
                    "data": "market_cap_eur",
                    render: $.fn.dataTable.render.number('.', ',', 0, ''),
                    "sClass": "right"
                },
                {
                    "data": "24h_volume_eur",
                    render: $.fn.dataTable.render.number('.', ',', 0, ''),
                    "sClass": "right"
                },
                {
                    mRender: function (data, type, row) {
                        var ratio = parseFloat(row['market_cap_eur'] / row['24h_volume_eur']);
                        return ratio.toFixed(5);
                    },
                    "sClass": "right"
                }
            ],
            "createdRow": function (row, data, index) {
                var ratio = parseFloat(data['market_cap_eur'] / data['24h_volume_eur']);
                if (ratio < 8) {
                    $(row).addClass('marked');
                }
            },
            "order": [[2, "desc"]],
            "pageLength": 100,
            dom: 'lfrt<B>p',
            buttons: [
                'excel'
            ]
            //"columnDefs": [
            //    {
            //        // The `data` parameter refers to the data for the cell (defined by the
            //        // `data` option, which defaults to the column being worked with, in
            //        // this case `data: 0`.
            //        "render": function (data, type, row) {
            //            console.log(row);
            //            return data + ' (' + row[1] + ')';
            //        },
            //        "targets": 0
            //    }
            //]

        });
});

function FillTable(n) {
    $.getJSON('https://api.coinmarketcap.com/v1/ticker/?convert=EUR&limit=' + n, function (data) {
        var th = "<tr><th>Coin</th><th>Symbol</th><th>Value €</th><th>Market cap €</th><th>Volume 24h €</th><th>MCap / Volume</th></tr >";
        //$("#coinTable").html(th);
        $.each(data, function (key, value) {
            var tr = "<tr>";
            var td1 = "<td>" + value.name + "</td>"
                + "<td>" + value.symbol + "</td>"
                + "<td>" + value.price_eur + "</td>"
                + "<td>" + value['market_cap_eur'] + "</td>"
                + "<td>" + value['24h_volume_eur'] + "</td>"
                + "<td>" + value.market_cap_eur / value['24h_volume_eur'] + "</td></tr>"
            $("#coinTable").append(tr + td1);
        });


    });
}


function exportExcel() {
    $(".table2excel").table2excel({
        exclude: ".noExl",
        name: "Excel Document Name",
        filename: "myFileName" + new Date().toISOString().replace(/[\-\:\.]/g, ""),
        fileext: ".xls",
        exclude_img: true,
        exclude_links: true,
        exclude_inputs: true
    });

}

function RefreshTable() {
    var n = $('#coins')[0].value;
    FillTable(n);
}

