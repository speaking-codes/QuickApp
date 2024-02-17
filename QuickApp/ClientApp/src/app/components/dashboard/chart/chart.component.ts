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
  chartData = [];
  
  timerReference: ReturnType<typeof setInterval> | undefined;

  @ViewChild(BaseChartDirective)
  chart!: BaseChartDirective;

  constructor(private alertService: AlertService, private dashboardService: DashboardServiceService) { }

  ngOnInit() {
    this.refreshChartOptions();
    this.loadData();    
  }

  ngOnDestroy() {
    clearInterval(this.timerReference);
  }

  loadData(): void{
    this.dashboardService.getDashboardInsuranceCoverageChart(this.customerCode)
    .subscribe({
        next: results => this.onDataLoadSuccessful(results),
        error: error => this.onDataLoadFailed(error)
    });
  }

  onDataLoadSuccessful(data: InsuranceCoverageSalesLineChart[]){
    this.salesLines = data;
    for(let i = 0; i < data.length; i++){
      this.chartData.push({
        data: [data[i].totalCount],
        label: data[i].salesLineName,
        fill: 'Origin',
        backgroundColor: [data[i].backGroundColor]
      });
    }
    this.chart.update();
  }

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
      },
      leggend: {
        position: 'left'
      }
    };

    // if (this.chartType != 'line') {
      const barChartOption: any = {
        legend: {
          position: 'bottom',
          labels: {
            fontSize: 10
          }
        }
      }
      this.chartOptions = {...baseOptions, ...barChartOption };
    // }
    // else {
    //   const lineChartOptions = {
    //     elements: {
    //       line: {
    //         tension: 0.5
    //       }
    //     }
    //   };

    //   this.chartOptions = { ...baseOptions, ...lineChartOptions };
    // }
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
