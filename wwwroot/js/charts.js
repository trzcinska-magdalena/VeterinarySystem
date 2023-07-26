function createWeightsChart(datesOfWeightsChart, valuesOfWeightChart) {
    let datesArray, valuesArray;

    try {
        datesArray = JSON.parse(datesOfWeightsChart);
        valuesArray = JSON.parse(valuesOfWeightChart);
    } catch (error) {
        console.error("Invalid input data:", error);
        return;
    }

    const ctx = document.getElementById("chart");
   
    const config = {
        type: "line",
        data: {
            labels: datesArray,
            datasets: [{
                label: "",
                data: valuesArray,
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

                    const answer = prompt("Enter a new value. To delete - leave blank");
                    const value = parseInt(answer) ?? 0;

                    $.ajax({
                        type: "POST",
                        url: "/Employee/Animal/UpdateWeights",
                        data: {
                            label, value, id: animalId
                        },
                        success: function (result) {
                            console.log(result);
                            window.location.href = "/Employee/Animal/Detail/" + animalId + "?activeTab=Weight";
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
}