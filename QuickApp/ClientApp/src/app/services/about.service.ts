import { Injectable } from '@angular/core';
import { AboutEndpointService } from './about-endpoint.service';
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
