import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  form: FormGroup = new FormGroup({});
username: string;
  userid: number;
  matchflag = true;
  constructor(private fb: FormBuilder,
    private _service: LoginService, 
    private router: Router) { 
      this.form = fb.group({
        userName: ['', [Validators.required]],
        password: ['', [Validators.required]]
      });
    }

    get f(){
      return this.form.controls;
    }

    submit() {
    this._service.getCheckLogin(this.form.value["userName"],this.form.value["password"]).subscribe(
      (data: any) => {
        this.userid = data;
        if(this.userid == 0) {this.matchflag= false;}
        else if(this.userid) {
          localStorage.setItem('UserName',this.form.value["userName"]);
          localStorage.setItem('UserId', this.userid.toString());
         this.router.navigate(['/fetch-purchase']);
        }
      }
    );
  }
  validatorPassword(userid: number){

  }
  
}
