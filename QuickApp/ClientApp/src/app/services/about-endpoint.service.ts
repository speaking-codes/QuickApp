import { Injectable } from '@angular/core';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
import { error } from 'console';

@Injectable({
  providedIn: 'root'
})
export class AboutEndpointService extends EndpointBase {
  get aboutUrl() { return this.configurationService.baseUrl + '/api/About'; }
  
  constructor(private configurationService: ConfigurationService, http: HttpClient, authService: AuthService) { 
    super(http, authService);
  }

  getAboutEndpoint<T>(){    
    console.log('endpoint url: ' + this.aboutUrl);
    return this.http.get<T>(this.aboutUrl, this.requestHeaders).pipe(
      catchError(error =>{
        console.log('error');
        return this.handleError(error, () => this.getAboutEndpoint<T>())
      })
    );
  }
}
