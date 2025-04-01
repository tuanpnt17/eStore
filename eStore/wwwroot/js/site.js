function drawOrderChart(orders) {
    console.log("Received orders:", orders); // Kiểm tra dữ liệu nhận được

    if (!orders || orders.length === 0) {
        console.warn("No data to display in chart.");
        return; // Nếu không có dữ liệu, không vẽ biểu đồ
    }
    const ctx = document.getElementById("orderChart").getContext("2d");
    const labels = orders.map(o => o.date);
    const data = orders.map(o => o.count);

    new Chart(ctx, {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Orders per day",
                data: data,
                borderColor: "blue",
                borderWidth: 2,
                fill: false
            }]
        },
        options: {
            responsive: true,
            scales: {
                x: { title: { display: true, text: "Date" } },
                y: { title: { display: true, text: "Order Count" }, beginAtZero: true }
            }
        }
    });
}
