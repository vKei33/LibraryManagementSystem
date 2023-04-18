import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Author } from 'src/app/features/feature-author/model/author';
import { Genre } from 'src/app/features/feature-genre/model/genre';
import { Publisher } from 'src/app/features/feature-publisher/model/publisher';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.scss']
})
export class CreateBookComponent implements OnInit {
  form = this.formBuilder.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    stock: [0, Validators.required],
    language: ['', Validators.required],
    genreId: ['', Validators.required],
    authorId: ['', Validators.required],
    publisherId: ['', Validators.required]
  });

  genres: Genre[] = [];
  authors: Author[] = [];
  publishers: Publisher[] = [];

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  async ngOnInit() {
    await this.getAllGenres();
    await this.getAllAuthors();
    await this.getAllPublishers();
  }

  async getAllGenres() {
    this.genres = (await this.api.get('/genres/all')).items || [];
  }

  async getAllAuthors() {
    this.authors = (await this.api.get('/authors/all')).items || [];
  }

  async getAllPublishers() {
    this.publishers = (await this.api.get('/publishers/all')).items || [];
  }

  onSubmit() {
    this.api.post('/books/create-book', this.form.value).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/books']);
      },
      error: () => {
        console.log("Failed");
      }
    });
  }
}
