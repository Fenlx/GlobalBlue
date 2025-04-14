import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { VatRequest } from '../models/vat.request.view.model';
import { Observable } from 'rxjs';
import { VatResponse } from '../models/vat.response.view.model';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  API_URL = environment.apiURL;
  routePrefix: string = 'api/business/';

  constructor(private http: HttpClient) { }

  public calculateVat(vatRequest: VatRequest): Observable<VatResponse> {
    return this.http.post<VatResponse>(this.API_URL + this.routePrefix + 'calculatevat', vatRequest);
  }
}
