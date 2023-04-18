import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-publisher',
  templateUrl: './create-publisher.component.html',
  styleUrls: ['./create-publisher.component.scss']
})
export class CreatePublisherComponent {
  form = this.formBuilder.group({
    name: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  onSubmit() {
    this.api.post('/publishers/create-publisher', this.form.value).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/publishers']);
      },
      error: () => {
        console.warn("Failed");
      }
    });
  }
}
