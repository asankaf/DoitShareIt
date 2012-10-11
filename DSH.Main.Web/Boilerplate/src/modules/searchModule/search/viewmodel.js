define(['Boiler'], function (Boiler) {



    var ViewModel = function (moduleContext) {

        $(document).ready(function () {
            $('#name-list').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/Home/Searchuser",
                        data: { searchText: request.term, maxResults: 10 },
                        dataType: "json",
                        success: function (data) {
                            response($.map(data, function (item) {
                                return {
                                    value: item.DisplayName,
                                    avatar: item.PicLocation,
                                    rep: item.Reputation,
                                    selectedId: item.Id

                                };
                            }));

                        }
                    });
                },
                select: function (event, ui) {


                    Boiler.UrlController.goTo("user/" + ui.item.selectedId);
                    $('#name-list').val("");
                    return false;
                }

            }).data("autocomplete")._renderItem = function (ul, item) {
                var inner_html = '<a><div class="list_item_container"><div class="image"><img src="' + item.avatar + '"></div><div class="label"><h3> Reputation:  ' + item.rep + '</h3></div><div class="description">' + item.label + '</div></div></a><hr/>';
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append(inner_html)
                    .appendTo(ul);

            };



        });









    };


    return ViewModel;

});


 
