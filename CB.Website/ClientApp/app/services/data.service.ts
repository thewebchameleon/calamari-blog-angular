import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataService {
    private _baseURL: string;
    private _route: string;
    private _pageSize?: number;

    constructor(
        public http: Http,
        @Inject('BASE_URL') baseUrl: string) {
        this._baseURL = baseUrl;
    }

    set(route: string, pageSize?: number): void {
        this._route = route;
        this._pageSize = pageSize;
    }

    get(page?: number) {
        var uri = this._baseURL + this._route;
        return this.http.get(uri);
    }
}