import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  myAppUrl = '';
  
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl + 'api/OrderDetail/';
  }
  


  getOrderDetails(filename : string) {
    let params = new HttpParams();
    params = params.append('filename', filename);
    return this._http.get(this.myAppUrl + 'GetOrderDetail', {params: params}).pipe(map(
      response => {
        return response;
      }));
  }

  addMessage(filename : string, msg: string, userName: string) {
    let params = new HttpParams();
    params = params.append('filename', filename);
    params = params.append('msg', msg);
    params = params.append('userName', userName);
    return this._http.get(this.myAppUrl + 'AddMessage', {params: params}).pipe(map(
      response => {
        return response;
      }));
  }
}
