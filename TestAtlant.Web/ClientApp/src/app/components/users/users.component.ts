import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';



@Component({ templateUrl: 'users.component.html' })
export class UsersComponent implements OnInit {
  loading = false;
  users: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.loading = true;
    this.userService.getAll().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });
  }
}
