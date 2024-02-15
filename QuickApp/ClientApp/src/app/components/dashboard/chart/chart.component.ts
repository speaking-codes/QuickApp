import { Component, OnInit, OnDestroy, ViewChild, Input } from '@angular/core';
import { AlertService, DialogType, MessageSeverity } from 'src/app/services/alert.service';
import { BaseChartDirective } from 'ng2-charts';
import { ChartEvent, ChartType, Color } from 'chart.js';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { ChartData, InsuranceCoverageSalesLineChart } from 'src/app/models/insurance-coverage';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

type ChartEventArgs = { event: ChartEvent; active: object[] }

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {
  @Input()
  showLeggend: boolean;

  @Input()
  heightChart: string;
  
  @Input()
  customerCode: string;

  private options: any = {
    plugins: { legend: { position: 'left' } }
  }

  salesLines: InsuranceCoverageSalesLineChart[];
  chartOptions: object | undefined;
  chartType: ChartType = 'bar';//'line';//'bubble';//'scatter';//'polarArea';//'bar'; //'pie';
  chartLabels = ['2023'];//['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];
  chartData = [
    {
        data: [2],
        label: 'Veicoli',
        fill: 'Origin'
    },
    {
        data: [5],
        label: 'Relax',
        fill: 'Origin'
    },
    {
        data: [1],
        label: 'Attività Lavorativa',
        fill: 'Origin'
    },
    {
        data: [4],
        label: 'Famiglia',
        fill: 'Origin'
    },
    {
        data: [3],
        label: 'Salute',
        fill: 'Origin'
  }];
  //   {
  //     data: [1],//, 59, 80, 81, 56, 55],
  //     label: 'Series A',
  //     fill: 'origin',
  //   },
  //   {
  //     data: [3],//, 48, 40, 19, 86, 27],
  //     label: 'Series B',
  //     fill: 'origin',
  //   },
  //   {
  //     data: [2],//, 48, 77, 9, 100, 27],
  //     label: 'Series C',
  //     fill: 'origin',
  //   }
  // ];
  lineChartColors: Color[] = [];
  
  timerReference: ReturnType<typeof setInterval> | undefined;

  @ViewChild(BaseChartDirective)
  chart!: BaseChartDirective;

  constructor(private alertService: AlertService, private dashboardService: DashboardServiceService) { }

  ngOnInit() {
    debugger;
    this.refreshChartOptions();
    this.timerReference = setInterval(() => this.randomize(), 5000);
  }

  ngOnDestroy() {
    clearInterval(this.timerReference);
  }

  // loadData(): void{
  //   this.dashboardService.getDashboardInsuranceCoverageChart(this.customerCode)
  //   .subscribe({
  //       next: results => this.onDataLoadSuccessful(results),
  //       error: error => this.onDataLoadFailed(error)
  //   });
  // }

  // onDataLoadSuccessful(data: InsuranceCoverageSalesLineChart[]){
  //   debugger;
  //   this.salesLines = data;
  //   for(var i = 0; i < this.salesLines.length -1; i++){
  //       //this.chartData.push(new ChartData(this.salesLines[i].totalCount, this.salesLines[i].salesLineName, 'origin'));      
  //       this.chartData = [
  //         {
  //           data: [this.salesLines[i].totalCount],
  //           label: this.salesLines[i].salesLineName, 
  //           fill: this.salesLines[i].backGroundColor
  //         },
  //         {
  //           data: [this.salesLines[i+1].totalCount],
  //           label: this.salesLines[i+1].salesLineName, 
  //           fill: this.salesLines[i+1].backGroundColor
  //         }
  //       ];
  //   }
  // }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  refreshChartOptions() {
    const baseOptions = {
      responsive: true,
      maintainAspectRatio: false,
      title: {
        display: true,
        fontSize: 16,
        text: 'Important Stuff'
      }
    };

    if (this.chartType != 'line') {
      this.chartOptions = baseOptions;
    }
    else {
      const lineChartOptions = {
        elements: {
          line: {
            tension: 0.5
          }
        }
      };

      this.chartOptions = { ...baseOptions, ...lineChartOptions };
    }
  }

  randomize(): void {
    for (let i = 0; i < this.chartData.length; i++) {
      for (let j = 0; j < this.chartData[i].data.length; j++) {
        this.chartData[i].data[j] = Math.floor((Math.random() * 0.5) + 1);
      }
    }

    this.chart.update();
  }

  changeChartType(type: ChartType) {
    this.chartType = type;
    this.refreshChartOptions();
  }

  showMessage(msg: string): void {
    this.alertService.showMessage('Demo', msg, MessageSeverity.info);
  }

  showDialog(msg: string): void {
    this.alertService.showDialog(msg, DialogType.prompt, (val) => this.configure(true, val), () => this.configure(false));
  }

  configure(response: boolean, value?: string) {
    if (response) {
      this.alertService.showStickyMessage('Simulating...', '', MessageSeverity.wait);

      setTimeout(() => {
        this.alertService.resetStickyMessage();
        this.alertService.showMessage('Demo', `Your settings was successfully configured to "${value}"`, MessageSeverity.success);
      }, 2000);
    } else {
      this.alertService.showMessage('Demo', 'Operation cancelled by user', MessageSeverity.default);
    }
  }


  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  chartHovered(e: ChartEventArgs): void {
    // Demo
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  chartClicked(e: Partial<ChartEventArgs>): void {
    // Demo
  }
}
