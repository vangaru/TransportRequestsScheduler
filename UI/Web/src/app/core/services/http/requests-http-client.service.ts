import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConfigurationService } from '../configuration/configuration.service';
import { buildUrl } from 'build-url-ts';
import { Request } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class RequestsHttpClientService {

  private readonly jsonContentTypeHeaders = new HttpHeaders({'Content-Type': 'application/json'});
  private readonly requestsUri: string;

  constructor(private readonly httpClient: HttpClient, 
              private readonly configurationService: ConfigurationService) {
    
    this.requestsUri = buildUrl(
      this.configurationService.requestsServerUrl, 
      { path: this.configurationService.requestsEndpoint });
  }

  public submitRequest(request: Request): Observable<any> {
    const requestBody: string = JSON.stringify(request);
    return this.httpClient.post(this.requestsUri, requestBody, {headers: this.jsonContentTypeHeaders});
  }
}