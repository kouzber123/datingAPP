import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor() {}
  //runs on start
  ngOnInit(): void {}

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  // cancel form > sends false to this and changes registermode
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
