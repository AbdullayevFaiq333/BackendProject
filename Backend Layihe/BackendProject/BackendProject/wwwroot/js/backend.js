$(document).ready(function () {

    //Search
    function Search(product) {
        let search;
        $(document).on("keyup", `#input-search-${product}`, function () {
            search = $(this).val().trim();


            $(`#new-${product}`).empty();
            if (search.length == 0) {
                $(`#old-${product}`).css("display", "block")
                $(`#pagination-${product}`).css("display", "block")

                return;
            }

            $.ajax({
                url: '/Home/Search/',
                type: "GET",
                data: {
                    "search": search,
                    "path": product
                },
                success: function (res) {
                    $(`#old-${product}`).css("display", "none")
                    $(`#pagination-${product}`).css("display", "none")
                    $(`#new-${product}`).append(res);
                }
            });
        });
    }
    Search("Course")
    Search("Event")
    Search("Blog")
    Search("Teacher")
})