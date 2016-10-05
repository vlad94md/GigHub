var FollowController = function (followService) {
    var link;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        link = $(e.target);
        var userId = link.attr("data-user-id");

        if (link.text() === "Follow") {
            followService.toFollow(userId, done, fail);
        }
        else if (link.text() === "Unfollow") {
            followService.toUnfollow(userId, done, fail);
        }
    }

    var fail = function () {
        alert("Something failed");
    };

    var done = function () {
        var text = (link.text() === "Follow" ? "Unfollow" : "Follow");
        link.text(text);
    };

    return {
        init: init
    }

}(FollowService);