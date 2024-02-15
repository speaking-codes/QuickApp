import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardHeaderComponent } from '../dashboard-header/dashboard-header.component';
import { DashTitleComponent } from '../dash-title/dash-title.component';
import { InsurancecoverageComponent } from '../insurancecoverage/insurancecoverage.component';
import { InsuranceCoverageFooterComponent } from '../insurance-coverage-footer/insurance-coverage-footer.component';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { InsuranceCategoryPolicyCard, InsuranceCoveragePolicyFooter } from 'src/app/models/insurance-coverage';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';
import { Console } from 'console';
import { EnumDashboardCall } from 'src/app/models/enums';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{
    elementNumber: number = 6;
    customerCode: string = "";
    customerFullName: string = "";
    titleInsuranceCoverageRecommended: string;
    titleInsuranceCoverageTopSelling: string;
    titleInsuranceCoverageOther: string;
    insuranceCoverageCodes: string;
        
    insuranceCoverageRecommended: InsuranceCategoryPolicyCard[] = [];
    insuranceCoverageTopSelling: InsuranceCategoryPolicyCard[] = [];
    insuranceCoverageOthers: InsuranceCategoryPolicyCard[] = [];
    insuranceCoveragePolicyFooters: InsuranceCoveragePolicyFooter[] = [];
    
    constructor(private dashboardService: DashboardServiceService, private readonly route: ActivatedRoute, private alertService: AlertService){}

    ngOnInit(): void {
      this.customerCode = this.route.snapshot.params['customerCode'];    
      this.titleInsuranceCoverageRecommended ="Beni suggeriti al cliente";
      this.titleInsuranceCoverageTopSelling = "Beni più venduti";
      this.titleInsuranceCoverageOther = "Altri beni";
      this.insuranceCoverageCodes = "";
      
      this.loadData();
    }

    loadData(){
      this.dashboardService.getDashboardInsuranceCoverageRecommended(this.customerCode)
          .subscribe({
              next: results => this.onDataLoadSuccessful(results, EnumDashboardCall.Recommended),
              error: error => this.onDataLoadFailed(error)
          });
    }

    onDataLoadSuccessful(data: InsuranceCategoryPolicyCard[], callType: EnumDashboardCall){
        if (data.length == 0) 
            return;

        for(var i = 0; i < data.length; i++)
          this.insuranceCoverageCodes = this.insuranceCoverageCodes + data[i].code + ";";
        
        switch(callType)
        {
          case EnumDashboardCall.Recommended:
              this.insuranceCoverageRecommended = data;
              this.dashboardService.getDashboardInsuranceCoverageTopSelling(this.elementNumber, this.insuranceCoverageCodes)
                  .subscribe({
                      next: results => this.onDataLoadSuccessful(results, EnumDashboardCall.TopSelling),
                      error: error => this.onDataLoadFailed(error)
                  });
            break;
          case EnumDashboardCall.TopSelling:
              this.insuranceCoverageTopSelling = data;
              this.dashboardService.getDashboardInsuranceCoverageOther(this.insuranceCoverageCodes)
              .subscribe({
                  next: results => this.onDataLoadSuccessful(results, EnumDashboardCall.Other),
                  error: error => this.onDataLoadFailed(error)
              });
            break;
          case EnumDashboardCall.Other:
            this.insuranceCoverageOthers = data;
            break;
          default:
              this.insuranceCoverageCodes = "";
            break;
        }
    }

    onDataLoadFailed(error: HttpErrorResponse){
      this.alertService.stopLoadingMessage();

      this.alertService.showStickyMessage('Load Error',
          `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
          MessageSeverity.error, error);
    }

    onInsuranceCoverageAdded(selectedItem: InsuranceCategoryPolicyCard){ 
        debugger;
        var newInsuranceCoveragePolicy = new InsuranceCoveragePolicyFooter(selectedItem.code, selectedItem.name, selectedItem.iconCssClass, selectedItem.salesLineBackgroundCssClass);
        this.insuranceCoveragePolicyFooters.push(newInsuranceCoveragePolicy);
    }

    onInsuranceCoverageRemoved(selectedValue: string){ 
        this.insuranceCoveragePolicyFooters = this.insuranceCoveragePolicyFooters.filter(x => x.code != selectedValue);
     }

     onLoad(customerName: string){
      debugger;
      this.customerFullName = customerName;
    }
}
