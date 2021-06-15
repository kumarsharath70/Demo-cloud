import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpResponse} from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';
import { UpdatedActionOrder } from '../../models/UpdatedActionOrder';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  myAppUrl = '';
  updatedAction : UpdatedActionOrder;
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl + 'api/PurchaseOrder/';
  }
  private cartAmountSource = new Subject<UpdatedActionOrder>();

  // Observable string streams
  cartQuantity$ = this.cartAmountSource.asObservable();

  // Service message commands
  updateProductCarts(updatedOrder: UpdatedActionOrder) {
    updatedOrder.action =  "Read";
    this.cartAmountSource.next(updatedOrder);
  }

  getPurchaseOrder() {
    return this._http.get(this.myAppUrl + 'Index').pipe(map(
      response => {
        return response;
      }));
  }

  getExportToExcel() {
    return this._http.get(this.myAppUrl + 'ExportToExcel', { responseType: 'blob' });
     
  }

  //getEmployeeById(id: number) {
  //  return this._http.get(this.myAppUrl + 'Details/' + id)
  //    .pipe(map(
  //      response => {
  //        return response;
  //      }));
  //}

  //saveEmployee(employee: Employee) {
  //  return this._http.post(this.myAppUrl + 'Create', employee)
  //    .pipe(map(
  //      response => {
  //        return response;
  //      }));
  //}

  //updateEmployee(employee: Employee) {
  //  return this._http.put(this.myAppUrl + 'Edit', employee)
  //    .pipe(map(
  //      response => {
  //        return response;
  //      }));
  //}

  //deleteEmployee(id: number) {
  //  return this._http.delete(this.myAppUrl + 'Delete/' + id)
  //    .pipe(map(
  //      response => {
  //        return response;
  //      }));
  //}
}
