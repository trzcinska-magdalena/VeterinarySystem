function loadCalendarOfAppointments() {
    var calendarEl = document.getElementById("calendar");
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: "listMonth",
        navLinks: true,
        dayMaxEvents: true,
        headerToolbar: {
            center: "addEventButton"
        },
        customButtons: {
            addEventButton: {
                text: "Add appointment",
                click: function () {
                    $("#addEventModal").modal("show");
                }
            }
        },
        events: "/Employee/Animal/GetAllAppointments/" + animalId,
        eventClick: function (info) {
            //info.event.id;
            $("#exampleModal").modal("show");
        },
    });
    calendar.render();
};

loadCalendarOfAppointments();