import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Book } from '../../model/book';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  books: Book[] = [];
  bookId: any;
  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.getAll();
    this.bookId = this.route.snapshot.paramMap.get('id');
  }

  async getAll() {
    this.books = (await this.api.get('/books/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure?')) {
      this.api.delete('/books/delete-book', this.bookId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
