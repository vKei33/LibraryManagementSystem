import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Book } from '../../model/book';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.scss']
})
export class BookDetailComponent implements OnInit {
  book!: Book;
  bookId: any;

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.bookId = this.route.snapshot.paramMap.get('id');
    await this.getBook();
  }

  async getBook() {
    this.book = (await this.api.get(`/books/${this.bookId}`)).item;
    console.log(this.book);
  }
}
