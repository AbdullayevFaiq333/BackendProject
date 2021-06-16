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

let subInput;
$(document).on("click", "#button-subscribe", function () {
    $("#response-subscribe").empty()
    subInput = $("#Email-sucscribe").val()
    if (subInput.length > 1) {
        $.ajax({
            type: "Get",
            url: "Home/Subscribe",
            data: {
                "email": subInput

            },
            success: function (res) {
                $("#response-subscribe").append(res)
            }
        })
    }

})


$(document).ready(function () {

    //Global Search
    
    let search;
    $(document).on("keyup", `#input-search-home`, function () {
        search = $(this).val().trim();


        $(`#home-search .li`).remove();

        if (se.length>0) {
            $.ajax({
                url: '/Home/GlobalSearch/',
                type: "GET",
                data: {
                    "search": search,
                    
                },
                success: function (res) {
                    $(`#home-search`).append(res)
                }
            });
        }
        
    });    
    
})
