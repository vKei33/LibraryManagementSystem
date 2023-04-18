import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-genre',
  templateUrl: './update-genre.component.html',
  styleUrls: ['./update-genre.component.scss']
})
export class UpdateGenreComponent implements OnInit {
  genreId: any;
  form = this.formBuilder.group({
    name: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit() {
    this.genreId = this.route.snapshot.paramMap.get('id');
    await this.getGenre();
  }

  async getGenre() {
    let result = (await this.api.get(`/genres/${this.genreId}`)).item;
    if (result) {
      this.form.reset({
        name: result.name
      });
    }
  }

  onSubmit() {
    this.api.put('/genres/update-genre', this.genreId, this.form.value).subscribe({
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
