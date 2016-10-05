var GigsController = function (attendanceService, followService) {
    var button;

    var init = function (container) {
        $(container).on("click",  ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, done, fail);
        } else {
            attendanceService.deleteAttendace(gigId, done, fail);
        }
    }

    var fail = function () {
        alert("Something wrong");
    };

    var done = function () {
        var text = (button.text() === "I'm In!" ? "Going?" : "I'm In!");

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    return {
        init: init
    }
}(AttendanceService);