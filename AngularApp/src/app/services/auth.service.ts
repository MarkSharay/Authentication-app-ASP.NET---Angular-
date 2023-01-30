import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + 'identity/login/';
  private registerPath = environment.apiUrl + 'identity/register/';
  private httpClient: HttpClient;
  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }
  login(data: any): Observable<any> {
    return this.httpClient.post<any>(this.loginPath, data);
  }
  register(data: any): Observable<any> {
    return this.httpClient.post<any>(this.registerPath, data);
  }
  saveToken(token: any){
    localStorage.setItem('token', token);
  }

  getToken(){
    return localStorage.getItem('token');
  }
}

