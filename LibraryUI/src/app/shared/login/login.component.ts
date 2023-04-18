import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  ngOnInit() {

  }

  async onSubmit() {
    if (this.form.valid) {
      this.api.post('/users/authenticate', this.form.value).subscribe({
        next: (res) => {
          console.log("Successfully logged in.");
          this.form.reset();
          this.router.navigate(['/books']);
        },
        error: () => {
          console.log("Failed to log in.");
          this.form.reset();
        }
      })
    }
  }
}
