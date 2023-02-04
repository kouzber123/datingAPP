import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

//to make use of this component we have to use selector
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  constructor(
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }
  //setting the current user local if not any then return else jsonparse
  setCurrentUser() {
    const userString = localStorage.getItem("users");
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
