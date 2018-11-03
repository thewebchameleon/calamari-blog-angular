import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataService {
  private _baseURL: string | undefined;
  private _pageSize?: number | undefined;

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this._baseURL = baseUrl;
  }

  get<T>(route: string) {
    var uri = this._baseURL + route;
    return this.http.get<T>(uri);
  }
}
