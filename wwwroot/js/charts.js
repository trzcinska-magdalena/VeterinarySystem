const ctx = document.getElementById("chart");

const config = {
    type: "line",
    data: {
        labels: JSON.parse(datesOfWeightsChart),
        datasets: [{
            label: "",
            data: JSON.parse(valuesOfWeightChart),
            pointStyle: "circle",
            borderColor: "#593196",
            pointRadius: 5,
        }]
    },
    options: {
        plugins: {
            legend: {
                display: false
            }
        },
        onClick: function (evt) {
            const points = chartLine.getElementsAtEventForMode(evt, "nearest", { intersect: true }, true);
            if (points.length) {
                const firstPoint = points[0];
                const label = chartLine.data.labels[firstPoint.index];

                var answer = prompt("Enter a new value. To delete - leave blank");
                var value = parseInt(answer) ?? 0;

                $.ajax({
                    type: "POST",
                    url: "/Employee/Animal/UpdateWeights",
                    data: {
                        label, value, id: animalId
                    },
                    success: function (result) {
                        window.location.href = "/Employee/Animal/Detail/" + animalId;
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            }
        }
    }
}
const chartLine = new Chart(ctx, config);