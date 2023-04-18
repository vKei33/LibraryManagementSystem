import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from 'src/app/features/feature-author/model/author';
import { Genre } from 'src/app/features/feature-genre/model/genre';
import { Publisher } from 'src/app/features/feature-publisher/model/publisher';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.scss']
})
export class UpdateBookComponent implements OnInit {
  form = this.formBuilder.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    stock: [, Validators.required],
    language: ['', Validators.required],
    genreId: ['', Validators.required],
    authorId: ['', Validators.required],
    publisherId: ['', Validators.required]
  });

  genres: Genre[] = [];
  authors: Author[] = [];
  publishers: Publisher[] = [];
  bookId: any;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.bookId = this.route.snapshot.paramMap.get('id');
    await this.getAllGenres();
    await this.getAllAuthors();
    await this.getAllPublishers();
    await this.getBook();
  }

  async getBook() {
    let result = (await this.api.get(`/books/${this.bookId}`)).item;
    if (result) {
      this.form.reset({
        title: result.title,
        description: result.description,
        stock: result.stock,
        language: result.language,
        genreId: result.genreId,
        authorId: result.authorId,
        publisherId: result.publisherId
      });
    }
    console.log(result);
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
    this.api.put('/books/update-book', this.bookId, this.form.value).subscribe({
      next: () => {
        console.log('Success');
        this.form.reset();
        this.router.navigate(['/books']);
      },
      error: () => {
        console.log('Failed');
      }
    })
  }
}
