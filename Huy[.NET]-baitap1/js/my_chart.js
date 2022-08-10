const ctx = document.getElementById('myChart').getContext('2d');
const cte = document.getElementById('earning').getContext('2d');
const ctp = document.getElementById('pieChart').getContext('2d');
const ctl = document.getElementById('lineChart').getContext('2d');
const ctr = document.getElementById('radarChart').getContext('2d');
const ctm = document.getElementById('mixChart').getContext('2d');
const myChart = new Chart(ctx, {
    type: 'polarArea',
    data: {
        labels: ['Facebook', 'Youtube', 'Amazon'],
        datasets: [{
            label: '# of Votes',
            data: [1200, 1900, 3000],
            backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)'
                
            ]
        }]
    },
    options: {
        responsive: true
    }
});
const earning = new Chart(cte, {
    type: 'bar',
    data: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August',
                    'September', 'October', 'November', 'December'],
        datasets: [{
            label: 'Earning',
            data: [1200, 1971, 3000, 5913, 2888, 3110, 2992, 5321, 9321, 1500, 2800, 6910],
            backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
        }]
    },
    options: {
        responsive: true,
    }
});
const pieChart = new Chart(ctp, {
    type: 'doughnut',
    data: {
        labels: ['Nguyễn', 'Trần', 'Lê', 'Phạm', 'Huỳnh/Hoàng', 'Phan', 'Vũ/Võ', 'Đặng',
                    'Bùi', 'Đỗ', 'Hồ', 'Ngô', 'Dương', 'Lý', 'Họ khác'],
        datasets: [{
            label: 'Họ Việt Nam',
            data: [38, 11, 10, 7, 5, 5, 4, 2, 2, 1, 1, 1, 1, 1, 11],
            backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 120, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(230, 159, 64, 1)',
                'rgba(196, 99, 132, 1)',
                'rgba(34, 162, 235, 1)',
                'rgba(291, 206, 86, 1)',
                'rgba(205, 192, 192, 1)',
                'rgba(310, 102, 255, 1)',
                'rgba(89, 159, 64, 1)',
                'rgba(120, 102, 255, 1)',
                'rgba(147, 159, 64, 1)',
                'rgba(100, 120, 64, 1)'
            ],
        }]
    },
    options: {
        responsive: true,
    }
});
const lineChart = new Chart(ctl, {
    type: 'line',
    data: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August',
                    'September', 'October', 'November', 'December'],
        datasets: [{
            label: 'Earning',
            data: [1200, 1971, 3000, 5913, 2888, 3110, 2992, 5321, 9321, 1500, 2800, 6910],
            backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
        }]
    },
    options: {
        responsive: true,
    }
});
const radarChart = new Chart(ctr, {
    type: 'radar',
    data: {
        labels: ['Str', 'Dex', 'Int', 'Wit', 'Men'],
        datasets: [{
            label: 'Charater 1',
            data: [65, 59, 90, 81, 56],
            fill: true,
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgb(255, 99, 132)',
            pointBackgroundColor: 'rgb(255, 99, 132)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgb(255, 99, 132)'
          }, {
            label: 'Charater 2',
            data: [28, 48, 40, 19, 92],
            fill: true,
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgb(54, 162, 235)',
            pointBackgroundColor: 'rgb(54, 162, 235)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgb(54, 162, 235)'
          }]
    },
    options: {
        responsive: true,
    }
});
const mixChart = new Chart(ctm, {
    type: 'scatter',
    data: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August',
                    'September', 'October', 'November', 'December'],
        datasets: [{
            type: 'bar',
            label: 'Doanh Thu 2021',
            data: [1200, 1500, 3600, 5800, 2300, 3110, 2900, 5710, 9500, 4789, 2800, 6910],
            borderColor: 'rgb(255, 99, 132)',
            backgroundColor: 'rgba(255, 99, 132, 0.2)'
        },{
            type: 'line',
            label: 'Lợi nhuận 2021',
            data: [800, 900, 2000, 3000, 1200, 1000, 1500, 2950, 7000, 3491, 1400, 4800],
            fill: false,
            borderColor: 'rgb(54, 162, 235)'
        }]
    },
    options: {
        responsive: true,
    }
});
