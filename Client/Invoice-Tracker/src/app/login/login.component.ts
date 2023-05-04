import { Component, ViewChild, ElementRef, OnInit, AfterViewInit } from '@angular/core';
import { TokenHandler } from '../helpers/tokenHandler';
import { AccountsService } from '../services/accounts.service';
import Swal from 'sweetalert2'
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model = {
    email: '',
    password: ''
  }
  model2 = {
    token: '',
    provider: ''
  }

  ngOnInit(): void {
    this.tokenHandler.removeToken();
  }

  constructor(private accountsService: AccountsService, private tokenHandler: TokenHandler, private authService: MsalService, private router: Router) { }

  saveData() {
    this.accountsService.loginUser(this.model).subscribe({
      next: (res: any) => {
        if (res.isValid)
          var role = this.tokenHandler.getRoleFromToken();
        if (role == "SuperAdmin") {
          this.router.navigateByUrl('/superadmin/dashboard');
        }
        else if (role == "BUHead") {
          this.router.navigateByUrl('/buhead');
        }
        else if (role == "ProjectManager") {
          this.router.navigateByUrl('/projectmanager')
        }
        else {
          Swal.fire(
            'Did you enter the correct credentials?',
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
        this.model2.token = response.idToken.toString();
        this.model2.provider = "Microsoft";
        this.accountsService.microsoftLogin(this.model2).subscribe({
          next: (res: any) => {
            if (res.isValid)
              var role = this.tokenHandler.getRoleFromToken();
            if (role == "SuperAdmin") {
              this.router.navigateByUrl('/superadmin/dashboard');
            }
            else if (role == "BUHead") {
              this.router.navigateByUrl('/buhead');
            }
            else if (role == "ProjectManager") {
              this.router.navigateByUrl('/projectmanager')
            }
            else {
              Swal.fire(
                '401- Unauthorized',
                res.errors[""][0],
                'error'
              )
            }
          }
        })
      });
  }
}

