import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppTitleService } from 'src/app/services/app-title.service';

@Component({
  selector: 'app-company-insurance',
  templateUrl: './company-insurance.component.html',
  styleUrls: ['./company-insurance.component.scss']
})
export class CompanyInsuranceComponent {
  appTitle = 'Dat - Assicurazioni';
  pathLogo = "assets/images/logo-white.png";

  constructor() {
    AppTitleService.appName = this.appTitle;
    AppTitleService.pathLogo = this.pathLogo;
  }
}
