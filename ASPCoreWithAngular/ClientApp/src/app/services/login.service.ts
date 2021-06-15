import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  fileName: string;
  myAppUrl = '';
  
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl + 'api/Login/';
  }
  


  getCheckLogin(name : string,pwd: string) {
    let params = new HttpParams();
    params = params.append('userName', name);
    params = params.append('password', pwd);
    return this._http.get(this.myAppUrl + 'CheckUser', {params: params}).pipe(map(
      response => {
        return response;
      }));
  }

  updateFileRead(userName: string, actionName: string, fileName:string){
    let params = new HttpParams();
    params = params.append('userName', userName);
    params = params.append('actionName', actionName);
    params = params.append('fileName', fileName);
    return this._http.get(this.myAppUrl + 'UpdateReadAction', {params: params}).pipe(map(
      response => {
        return response;
      }));

  }
get getfileName():string{
  return this.fileName;
}

set(fname: string){
  this.fileName =fname;
}
  }
