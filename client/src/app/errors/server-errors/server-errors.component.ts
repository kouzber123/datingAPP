import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-errors',
  templateUrl: './server-errors.component.html',
  styleUrls: ['./server-errors.component.css']
})
export class ServerErrorsComponent implements OnInit {
  error: any;

  constructor(private router: Router) { 
    const navgation = this.router.getCurrentNavigation();
    this.error = navgation?.extras?.state?.["error"] 
  }

  ngOnInit(): void {
  }

}
