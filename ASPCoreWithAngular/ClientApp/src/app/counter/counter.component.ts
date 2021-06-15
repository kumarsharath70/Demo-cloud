import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PurchaseService } from '../services/puchase.service';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  constructor(private _purchaseService: PurchaseService,
private router: Router  ) {
    // this._purchaseService.updateProductCarts("Read");
  }
  public incrementCounter() {
    //this.currentCount++;
    this.router.navigateByUrl('/fetch-purchase');
  }
}
