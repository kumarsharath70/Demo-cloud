import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Purchase } from '../../models/purchase';
import { PurchaseService } from '../services/puchase.service';
import { LoginService } from '../services/login.service';
import { Local } from 'protractor/built/driverProviders';


@Component({
  selector: 'app-fetch-purchase',
  templateUrl: './fetch-purchase.component.html',
  styleUrls: ['./fetch-purchase.component.css']
})
export class FetchPurchaseComponent implements OnInit {

  public orders: Purchase[];
  userName: string;
  actionName: string;
 loggedIn: string;
  constructor(private _purchaseService: PurchaseService, 
    private _loginService: LoginService,
    private router: Router) {
     
  }

  ngOnInit() {
    this.loggedIn = localStorage.getItem("UserName");
    this.getPurchaseOrders();
  }
  
  getPurchaseOrders() {
    this._purchaseService.getPurchaseOrder().subscribe(
      (data: Purchase[]) => {
        this.orders = data;
      }
    );
  }

  ExportToExcel() {
    this._purchaseService.getExportToExcel().subscribe(
      (response: any) => {
        const blob = new Blob([response], {
          type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const url = window.URL.createObjectURL(blob);
        var a = document.createElement("a");
        a.href = url;
        a.download = "OrderListExcel";
        a.click();
      }
    );
  }

  navigate(order: Purchase) {
    this.userName = localStorage.getItem("UserId");
    this.actionName = "Read";
    this._loginService.updateFileRead(this.userName, this.actionName, order.fileName).subscribe(
    (response: any) => {  
    localStorage.setItem('filename', order.fileName);
    localStorage.setItem('approve',order.approved);
      this.router.navigateByUrl("/order-details");
    },
     error => console.error(error)
    )
  }


signOut(){
  localStorage.clear();
  this.router.navigateByUrl("/");
}
  
}
