import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenHandler } from '../helpers/tokenHandler';


@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  url: string = "https://localhost:7226/api/Accounts/"

  constructor(private http: HttpClient, private tokenHandler: TokenHandler) { }

  loginUser(model: any) {
    var res = this.http.post(this.url + "AdminLogin", model);
    res.subscribe({
      next: (res: any) => {
        if (res.isValid)
          this.tokenHandler.setToken(res.result);
        else
          this.tokenHandler.removeToken();
      }
    })
    return res;
  }

  microsoftLogin(model: any) {
    var res = this.http.post(this.url + "ExternalLogin", model);
    res.subscribe({
      next: (res: any) => {
        if (res.isValid)
          this.tokenHandler.setToken(res.result);
        else
          this.tokenHandler.removeToken();
      }
    })
    return res;
  }
}
