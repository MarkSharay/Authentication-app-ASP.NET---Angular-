import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators, FormBuilder} from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./auth.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = fb.group({
      'username': [''],
      'email': [''],
      'password': ['']
    });
  }
  ngOnInit() {

  }
  register(){
    this.authService.register(this.registerForm.value).subscribe(data=>{
      console.log(data);
    })
  }
}