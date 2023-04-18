import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-author',
  templateUrl: './update-author.component.html',
  styleUrls: ['./update-author.component.scss']
})
export class UpdateAuthorComponent implements OnInit {
  authorId: any;
  form = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit() {
    this.authorId = this.route.snapshot.paramMap.get('id');
    await this.getAuthor();
  }

  async getAuthor() {
    let result = (await this.api.get(`/authors/${this.authorId}`)).item;
    if (result) {
      this.form.reset({
        firstName: result.firstName,
        lastName: result.lastName
      });
    }
  }

  onSubmit() {
    this.api.put('/authors/update-author', this.authorId, this.form.value).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/authors']);
      },
      error: () => {
        console.warn("Failed");
      }
    });
  }
}
