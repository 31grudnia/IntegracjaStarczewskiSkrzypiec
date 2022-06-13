import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Chart } from 'node_modules/chart.js';
import { registerables } from 'node_modules/chart.js';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit {
  songsChartDane: any;
  wynagrodzenieWartosci: any;
  value: any;
  myChart: any;

  constructor(private http: HttpClient) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.getDaneWynLata();
  }

  generateChart() {
    console.log(this.value);
    this.myChart.destroy();
    this.getDaneWynLata();
  }

  getDaneWynLata() {
    this.http
      .get('https://localhost:5003/api/songs/zlicz')
      .subscribe((response) => {
        this.songsChartDane = response;
        let tablicaL = [];
        let tablicaW = [];
        for (let item of this.songsChartDane) {
          tablicaL.push(item.year);
        }
        for (let item of this.songsChartDane) {
          tablicaW.push(item.liczba);
        }
        this.getChart(tablicaL, tablicaW);
      });
  }

  getChart(daneL: any, daneW: any) {
    const ctx = document.getElementById('myChart');
    this.myChart = new Chart('myChart', {
      type: 'line',
      data: {
        labels: daneL,

        datasets: [
          {
            label: 'Liczba wydanych piosenek',
            data: daneW,
            backgroundColor: [
              'rgba(255, 99, 132, 0.2)',
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 206, 86, 0.2)',
              'rgba(75, 192, 192, 0.2)',
              'rgba(153, 102, 255, 0.2)',
              'rgba(255, 159, 64, 0.2)',
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(255, 159, 64, 1)',
            ],
            borderWidth: 1,
          },
        ],
      },
      options: {
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Ilość piosenek',
            },
          },
          x: {
            title: {
              display: true,
              text: 'Lata',
            },
          },
        },
      },
    });
  }
}
