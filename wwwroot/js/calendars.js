function loadAnimalAppointmentsCalendar(animalId) {
    const calendarEl = document.getElementById("animal-appointments-calendar");
    if (!calendarEl) {
        console.error("Calendar element not found.")
    }
    const calendar = new FullCalendar.Calendar(calendarEl, {
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
            console.log(info.event.id);

            $.ajax({
                type: "GET",
                url: "/Employee/Animal/ShowAppointmentInfo",
                data: {
                    id: info.event.id
                },
                success: function (result) {
                    console.log(result);
                },
                error: function (error) {
                    console.error(error);
                }
            });
            $("#detailEventModal").modal("show");
        },
    });
    calendar.render();
};

function loadVetAppointmentsCalendar() {
    const calendarEl = document.getElementById('vet-appointments-calendar');
    if (!calendarEl) {
        console.error("Calendar element not found.")
    }
    const calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        navLinks: true,
        headerToolbar: {
            left: 'prev,next',
            center: 'title',
            right: 'dayGridMonth,dayGridWeek,dayGridDay'
        },
        dayMaxEvents: true,
        events: "/Identity/Account/Manage?handler=AllEvents"
    });
    calendar.render();
};