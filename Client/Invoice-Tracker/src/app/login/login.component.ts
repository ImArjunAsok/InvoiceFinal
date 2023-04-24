import { Component, ViewChild, ElementRef, OnInit, AfterViewInit } from '@angular/core';
import { TokenHandler } from '../helpers/tokenHandler';
import { AccountsService } from '../services/accounts.service';
import Swal from 'sweetalert2'
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model = {
    email: '',
    password: ''
  }
  constructor(private accountsService: AccountsService, private tokenHandler: TokenHandler, private authService: MsalService) { }

  saveData() {
    this.accountsService.loginUser(this.model).subscribe({
      next: (res: any) => {
        if (res.isValid)
          console.log(this.tokenHandler.getRoleFromToken());
        else {
          Swal.fire(
            'Did you enter the correct email?',
            res.errors[""][0],
            'question'
          )
        }
      }
    })
  }

  handleMicrosoftLogin() {
    this.authService.loginPopup()
      .subscribe((response: AuthenticationResult) => {
        console.log(response);
        this.authService.instance.setActiveAccount(response.account);
      });
  }
}

