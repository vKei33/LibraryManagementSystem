import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Author } from '../../model/author';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss']
})
export class AuthorComponent implements OnInit {
  authors: Author[] = [];
  authorId: any;
  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.getAll();
    this.authorId = this.route.snapshot.paramMap.get('id');
  }

  async getAll() {
    this.authors = (await this.api.get('/authors/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure?')) {
      this.api.delete('/authors/delete-author', this.authorId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
