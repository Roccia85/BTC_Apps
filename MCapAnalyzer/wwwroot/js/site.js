// Write your Javascript code.


$(document).ready(function () {

    $("#coinTable")
        .DataTable({
            "processing": true,
            "ajax": {
                "url": "https://api.coinmarketcap.com/v1/ticker/?convert=EUR&limit=500",
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
                    render: $.fn.dataTable.render.number('.', ',', 5, ''),
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
                    "data": "percent_change_1h",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
                    "sClass": "right"
                },
                {
                    "data": "percent_change_24h",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
                    "sClass": "right"
                },
                {
                    "data": "percent_change_7d",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
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
                var change1h = parseFloat(data['percent_change_1h']);
                var change24h = parseFloat(data['percent_change_24h']);
                var change7d = parseFloat(data['percent_change_7h']);
                if (ratio < 8) {
                    if (change1h >= 0)
                        $(row).addClass('markedUp');
                    else {
                        $(row).addClass('markedDown');
                    }
                }
                else {
                    if (change1h >= 0)
                        $('td', row).eq(6).addClass('markedTextUp');
                    else {
                        $('td', row).eq(6).addClass('markedTextDown');
                    }
                    if (change24h >= 0)
                        $('td', row).eq(7).addClass('markedTextUp');
                    else {
                        $('td', row).eq(7).addClass('markedTextDown');
                    }
                    if (change7d >= 0)
                        $('td', row).eq(8).addClass('markedTextUp');
                    else {
                        $('td', row).eq(8).addClass('markedTextDown');
                    }
                }
            },
            "order": [[4, "desc"]],
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

