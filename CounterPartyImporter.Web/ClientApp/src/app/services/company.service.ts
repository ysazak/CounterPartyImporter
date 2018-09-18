import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse, HttpEvent } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/publishReplay';
import 'rxjs/add/operator/share';
import 'rxjs/add/operator/retry';

import { Company } from '../models/company';
import { PagedTable } from '../models/pagedTable';


@Injectable()
export class CompanyService {
    apiBaseUrl = 'api/Company';

    constructor(private http: Http,
        private httpClient: HttpClient) { }

    // uploadFile(form: FormData): Observable<any>{
    //     return this.http.post(`${this.apiBaseUrl}/UploadFile`, form);
    // }

    importFile(formData: FormData): Observable<HttpEvent<{}>> {
        const uploadReq = new HttpRequest('POST', `${this.apiBaseUrl}/ImportFile`, formData, {
            reportProgress: true,
        });
        return this.httpClient.request(uploadReq);
    }

    getCompanies(page: number, pageSize: number): Observable<PagedTable<Company>> {
        return this.http.get(`${this.apiBaseUrl}?page=${page}&pageSize=${pageSize}`)
            .map(response => response.json() as PagedTable<Company>);
    }


}

