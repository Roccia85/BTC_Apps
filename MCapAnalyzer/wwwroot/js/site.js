// Write your Javascript code.


$(document).ready(function () {



    $("#newCoinTable")
        .DataTable({
            "processing": true,
            "ajax": {
                "url": "Home/CapsAsync?numberOfToken=600",
                "dataSrc": ""

            },
            "columns": [
                { "data": "name" },
                {
                    "data": "symbol",
                    "sClass": "center"
                },
                {
                    "data": "valueEur",
                    render: $.fn.dataTable.render.number('.', ',', 5, ''),
                    "sClass": "right"
                },
                {
                    "data": "valueBtc",
                    render: $.fn.dataTable.render.number('.', ',', 8, ''),
                    "sClass": "right"
                },
                {
                    "data": "marketCap",
                    render: $.fn.dataTable.render.number('.', ',', 0, ''),
                    "sClass": "right"
                },
                {
                    "data": "change1h",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
                    "sClass": "right"
                },
                {
                    "data": "change24h",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
                    "sClass": "right"
                },
                {
                    "data": "change7d",
                    render: $.fn.dataTable.render.number('.', ',', 2, ''),
                    "sClass": "right"
                },
                {
                    "data": "volume24h",
                    render: $.fn.dataTable.render.number('.', ',', 0, ''),
                    "sClass": "right"
                },
                {
                    "data": "marketCapVolume",
                    render: $.fn.dataTable.render.number('.', ',', 4, ''),
                    "sClass": "right"
                },
                {
                    "data": "hourlyVolumeDiff",
                    render: $.fn.dataTable.render.number('.', ',', 0, ''),
                    "sClass": "right"
                },
                {
                    "data": "hourlyMarketCapVolume",
                    render: $.fn.dataTable.render.number('.', ',', 4, ''),
                    "sClass": "right"
                }
            ],
            "createdRow": function (row, data, index) {
                var ratio = parseFloat(data['marketCapVolume']);
                console.info(ratio);
                var change1h = parseFloat(data['change1h']);
                var change24h = parseFloat(data['change24h']);
                var change7d = parseFloat(data['change7d']);
                var colChange1h = 5;
                var colChange24h = 6;
                var colChange7d = 7;
                if (Math.abs(ratio) < 8) {
                    if (change1h >= 0)
                        $(row).addClass('markedUp');
                    else {
                        $(row).addClass('markedDown');
                    }
                }
                else {
                    if (change1h >= 0)
                        $('td', row).eq(colChange1h).addClass('markedTextUp');
                    else {
                        $('td', row).eq(colChange1h).addClass('markedTextDown');
                    }
                    if (change24h >= 0)
                        $('td', row).eq(colChange24h).addClass('markedTextUp');
                    else {
                        $('td', row).eq(colChange24h).addClass('markedTextDown');
                    }
                    if (change7d >= 0)
                        $('td', row).eq(colChange7d).addClass('markedTextUp');
                    else {
                        $('td', row).eq(colChange7d).addClass('markedTextDown');
                    }
                }
            },
            //"order": [[9, "asc"]],
            "pageLength": 100,
            dom: 'lfrt<B>p',
            buttons: [
                'excel'
            ]

        });
});

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


