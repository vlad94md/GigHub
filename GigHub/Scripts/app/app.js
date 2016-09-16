function initGigs() {

    $(".js-toggle-attendance").click(function (e) {
        var button = $(e.target);

        if (button.hasClass("btn-default")) {

            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(function () {
                    button.removeClass("btn-default").addClass("btn-info").text("I'm In!");
                })
                .fail(function () {
                    alert("Something wrong!");
                });

        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"
            })
                .done(function () {
                    button.removeClass("btn-info")
                        .addClass("btn-default")
                        .text("Going?");
                })
                .fail(function () {
                    alert("Something wrong");
                });
        }
    });

    $(".js-toggle-follow").click(function (e) {
        var link = $(e.target);

        if (link.text() === "Follow") {
            $.post("/api/following", { followeeId: link.attr("data-user-id") })
                .done(function () {
                    link.text("Unfollow");
                })
                .fail(function () {
                    alert("Something wrong!");
                });

        }
        if (link.text() === "Unfollow") {
            $.ajax({
                url: "/api/following/" + link.attr("data-user-id"),
                method: "DELETE"
            })
                .done(function () {
                    link.text("Follow");
                })
                .fail(function () {
                    alert("Something wrong!");
                });
        }
    });
}