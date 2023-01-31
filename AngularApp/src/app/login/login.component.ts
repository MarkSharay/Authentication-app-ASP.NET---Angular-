import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators, FormBuilder} from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../register/auth.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  signs:string = "qwertyuiopasdfghjklzxcvbnm1234567890";
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = fb.group({
      'email': ['', [Validators.required, Validators.email]],
      'password': ['', [Validators.required]],  
    });
  }
  ngOnInit() {

  }
  login(){
    this.authService.login(this.loginForm.value).subscribe(data=>{
      //console.log(data);
      this.authService.saveToken(data['token']);
    })
  }

  get email(){
    return this.loginForm.get('email');  
  }

  get password(){
    return this.loginForm.get('password');
  }
}
