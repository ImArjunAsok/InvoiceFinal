import { Component, OnInit } from '@angular/core';
import { TokenHandler } from 'src/app/helpers/tokenHandler';

@Component({
  selector: 'app-menubar',
  templateUrl: './menubar.component.html',
  styleUrls: ['./menubar.component.css']
})
export class MenubarComponent implements OnInit {

  name: string = '';
  role: string = '';
  email: string = '';

  ngOnInit(): void {
    this.name = this.tokenHandler.getNameFromToken();
    this.role = this.tokenHandler.getRoleFromToken();
    this.email = this.tokenHandler.getEmailFromToken();
  }

  constructor(private tokenHandler: TokenHandler) { }

  profileDropdown() {
    var dropdown = document.querySelector('.dropdown-content') as HTMLElement | null;
    if (dropdown) {
      dropdown.classList.toggle('d-none');
      dropdown.classList.toggle('show');
    }
  }
}
