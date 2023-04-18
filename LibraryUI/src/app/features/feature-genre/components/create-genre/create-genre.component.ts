import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-genre',
  templateUrl: './create-genre.component.html',
  styleUrls: ['./create-genre.component.scss']
})
export class CreateGenreComponent {
  form = this.formBuilder.group({
    name: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  onSubmit() {
    this.api.post('/genres/create-genre', (this.form.value)).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/genres']);
      },
      error: () => {
        console.warn("Failed");
      }
    });
  }
}
