// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { fadeInOut } from 'src/app/services/animations';
import { AboutService } from 'src/app/services/about.service';
import { error } from 'console';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';
import { HttpResponse } from '@angular/common/http';
import { IAbout, About } from 'src/app/models/iabout';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
  animations: [fadeInOut]
})
export class AboutComponent implements OnInit{
  private loadingIndicator = false;  
  
  aboutCompany: About;

 constructor(private aboutService: AboutService, private alertService: AlertService){}

  ngOnInit(): void{
    this.loadingIndicator = true;
    this.aboutService.getInsuranceCompanyAbout()
        .subscribe({
            next: results => this.onDataLoadSuccessful(results),
            error: results => this.onDataLoadFailed(results)
        });
  }

  onDataLoadSuccessful(aboutCompany: IAbout){   
    this.aboutCompany = aboutCompany as About;
  }

  onDataLoadFailed(error: HttpErrorResponse){
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

}
