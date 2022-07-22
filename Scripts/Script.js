
    $(document).ready(function () {
        debugger;
        $.ajax({
            type: "GET",
            //url: '@Url.Action("GetData","Home")',
            url: '/Home/GetData',
            data: "",
            dataType: "json",
            success: function (data) {
                loadData(data);
            },
            error: function () {
                alert("Failed! Please try again.");
            }
        });


                    function loadData(data) {
                        // Here we will format & load/show data
                        var tab = $('<table class="myTable"></table>');
                        var thead = $('<thead></thead>');
                        thead.append('<th>ID</th>');
                        thead.append('<th>Title</th>');
                        thead.append('<th>Completed</th>');

                        tab.append(thead);
                        $.each(data, function (i, val) {
                            // Append database data here
                            var trow = $('<tr></tr>');
                            trow.append('<td>' + val.Id + '</td>');
                            trow.append('<td>' + val.Title + '</td>');
                            trow.append('<td>' + val.Completed + '</td>');
                            tab.append(trow);
                        });
                        $("tr:odd", tab).css('background-color', '#C4C4C4');
                        $("#UpdatePanel").html(tab);
                    };

                });
            
