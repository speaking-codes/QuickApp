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
  companyName: string;
  companyAddress: string;
  companyCity: string;
  companyPostalCode: string;
  companyCountry: string;
  companyPhoneNumber: string;
  companyFaxNumber: string;
  companyEmail: string;
  companyDescription1: string;
  companyDescription2: string;
  companyDescription3: string;
  companyDescription4: string;
  companyDescription5: string;
  companyDescription6: string;
  companyCapitaleSociale: string; 

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
    this.companyName = aboutCompany.name;
    this.companyAddress = aboutCompany.address;
    this.companyCity = aboutCompany.city
    this.companyPostalCode = aboutCompany.postalCode;
    this.companyCountry = aboutCompany.country;
    this.companyPhoneNumber = aboutCompany.phoneNumber;
    this.companyFaxNumber = aboutCompany.faxNumber;
    this.companyEmail = aboutCompany.email;
    this.companyDescription1 = aboutCompany.description1;
    this.companyDescription2 = aboutCompany.description2;
    this.companyDescription3 = aboutCompany.description3;
    this.companyDescription4 = aboutCompany.description4;
    this.companyDescription5 = aboutCompany.description5;
    this.companyDescription6 = aboutCompany.description6;
    this.companyCapitaleSociale = aboutCompany.socialCapital;
  }

  onDataLoadFailed(error: HttpErrorResponse){
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

}
