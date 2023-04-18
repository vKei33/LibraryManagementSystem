import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrls: ['./create-author.component.scss']
})
export class CreateAuthorComponent {
  form = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  onSubmit() {
    this.api.post('/authors/create-author', this.form.value).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/authors']);
      },
      error: () => {
        console.warn("Failed");
      }
    })
  }
}
