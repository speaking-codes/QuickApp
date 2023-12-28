// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { fadeInOut } from '../../services/animations';
import { InsuranceCompany } from 'src/app/models/insurance-company';
import { AboutService } from 'src/app/services/about.service';
import { error } from 'console';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
  animations: [fadeInOut]
})
export class AboutComponent implements OnInit{
  private loadingIndicator = false;  
  
  insuranceCompany: InsuranceCompany;
  companyName: string;
  companyAddress: string;
  companyCity: string;
  companyPostalCode: string;
  companyCountry: string;
  companyPhoneNumber: string;
  companyFaxNumber: string;
  companyEmail: string;
  companyAbstract: string;
  companyCapitaleSociale: string;
  companyDescription: string;
  companyDescriptionList: [string];

  constructor(private aboutService: AboutService, private alertService: AlertService){}

  ngOnInit(): void{
    this.loadingIndicator = true;
    debugger;
    this.aboutService.getInsuranceCompanyAbout()
        .subscribe({
            next: results => this.onDataLoadSuccessful(results),
            error: results => this.onDataLoadFailed(results)
        });
  }

  onDataLoadSuccessful(insuranceCompany: InsuranceCompany){   
    this.insuranceCompany = insuranceCompany;
    this.insuranceCompany = insuranceCompany;
    this.companyName = insuranceCompany.name;
    this.companyAddress = insuranceCompany.address;
    this.companyCity = insuranceCompany.city
    this.companyPostalCode = insuranceCompany.postalCode;
    this.companyCountry = insuranceCompany.country;
    this.companyPhoneNumber = insuranceCompany.phoneNumber;
    this.companyFaxNumber = insuranceCompany.faxNumber;
    this.companyEmail = insuranceCompany.email;
    this.companyAbstract = insuranceCompany.abstract;
    this.companyCapitaleSociale = insuranceCompany.capitaleSociale;
    this.companyDescription = insuranceCompany.description;
    this.companyDescriptionList = insuranceCompany.descriptionList;
  }

  onDataLoadFailed(error: HttpErrorResponse){
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

}
