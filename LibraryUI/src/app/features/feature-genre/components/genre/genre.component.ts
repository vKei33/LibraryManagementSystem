import { Component, OnInit } from '@angular/core';
import { Genre } from '../../model/genre';
import { ApiService } from 'src/app/services/api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.scss']
})
export class GenreComponent implements OnInit {
  genres: Genre[] = [];
  genreId: any;
  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.getAll();
    this.genreId = this.route.snapshot.paramMap.get('id');
  }

  async getAll() {
    this.genres = (await this.api.get('/genres/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure?')) {
      this.api.delete('/genres/delete-genre', this.genreId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
