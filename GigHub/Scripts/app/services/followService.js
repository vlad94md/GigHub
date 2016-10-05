var FollowService = function () {
    var toFollow = function (userId, done, fail) {
        $.ajax({
            url: "/api/following/",
            data: { followeeId: userId},
            method: "POST"
        })
            .done(done)
            .fail(fail);

    };

    var toUnfollow = function (userId, done, fail) {
        $.ajax({
            url: "/api/following/",
            data: { followeeId: userId },
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        toFollow: toFollow,
        toUnfollow: toUnfollow
    }

}();
