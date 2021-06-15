import { Component, OnInit, ViewChild } from '@angular/core';
import { OrderDetailsService } from '../services/order-details.service';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {
  filename: string;
  approveStatus: string;
  poDetails; any;
 poDescription:any;
 poSupplier: any;
 poMessage: any;
 poParts:any;
 poTotal:any;
 poCondition: any;
 poComments: any;
 poMessages: any;
 loggedIn: string;

  constructor(private _orderService: OrderDetailsService, private _loginService: LoginService) {
    
  }

  ngOnInit(){
    this.loggedIn = localStorage.getItem("UserName");
   this.filename = localStorage.getItem('filename');
this.approveStatus =localStorage.getItem('approve')
  this.getOrderDetails(this.filename);
  }


  getOrderDetails(filename: string) {
    this._orderService.getOrderDetails(filename).subscribe(
      (data: any) => {
        console.log(data);
        this.poDetails= data.poDetails[0];
        this.poDescription = data.poDescription[0];
        this.poSupplier = data.poSupplier[0];
        this.poMessage = data.poMessage;
        this.poParts =data.poParts[0];
        this.poTotal = data.poTotals[0];
        this.poCondition =data.poConditions[0];
        this.poComments = data.poComment;
        this.poMessages = data.poMessages[0];
      }
    );
  }
  openModal(){
    var modal = document.getElementById("myModal");
    modal.style.display = "block";
  }

    
close(){
  var modal = document.getElementById("myModal");
  modal.style.display = "none";
}

addMessage(msg: any){
  console.log("messag" +msg.value);
  this._orderService.addMessage(this.filename,msg.value,this.loggedIn).subscribe(x=>{console.log(x)});
  var modal = document.getElementById("myModal");
  modal.style.display = "none";
}
  
}
