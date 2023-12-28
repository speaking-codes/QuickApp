import { Injectable } from '@angular/core';
import { AboutEndpointService } from './about-endpoint.service';
import { InsuranceCompany } from '../models/insurance-company';
import { IAbout } from '../models/iabout';

@Injectable({
  providedIn: 'root'
})
export class AboutService {

  constructor(private aboutEndpointService: AboutEndpointService) { }

  getInsuranceCompanyAbout(){
    return this.aboutEndpointService.getAboutEndpoint<IAbout>();
  }
}
