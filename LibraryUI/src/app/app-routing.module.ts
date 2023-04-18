import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './features/feature-author/components/author/author.component';
import { CreateAuthorComponent } from './features/feature-author/components/create-author/create-author.component';
import { UpdateAuthorComponent } from './features/feature-author/components/update-author/update-author.component';
import { BookDetailComponent } from './features/feature-book/components/book-detail/book-detail.component';
import { BookComponent } from './features/feature-book/components/book/book.component';
import { CreateBookComponent } from './features/feature-book/components/create-book/create-book.component';
import { UpdateBookComponent } from './features/feature-book/components/update-book/update-book.component';
import { CreateGenreComponent } from './features/feature-genre/components/create-genre/create-genre.component';
import { GenreComponent } from './features/feature-genre/components/genre/genre.component';
import { UpdateGenreComponent } from './features/feature-genre/components/update-genre/update-genre.component';
import { CreateMemberComponent } from './features/feature-member/components/create-member/create-member.component';
import { MemberComponent } from './features/feature-member/components/member/member.component';
import { UpdateMemberComponent } from './features/feature-member/components/update-member/update-member.component';
import { CreatePublisherComponent } from './features/feature-publisher/components/create-publisher/create-publisher.component';
import { PublisherComponent } from './features/feature-publisher/components/publisher/publisher.component';
import { UpdatePublisherComponent } from './features/feature-publisher/components/update-publisher/update-publisher.component';
import { CreateRentComponent } from './features/feature-rent/components/create-rent/create-rent.component';
import { RentComponent } from './features/feature-rent/components/rent/rent.component';
import { UpdateRentComponent } from './features/feature-rent/components/update-rent/update-rent.component';
import { LoginComponent } from './shared/login/login.component';

const routes: Routes = [
  { path: 'books', component: BookComponent },
  { path: 'books/detail/:id', component: BookDetailComponent },
  { path: 'books/create-book', component: CreateBookComponent },
  { path: 'books/update-book/:id', component: UpdateBookComponent },
  { path: 'rents', component: RentComponent },
  { path: 'rents/create-rent', component: CreateRentComponent },
  { path: 'rents/update-rent/:id', component: UpdateRentComponent },
  { path: 'genres', component: GenreComponent },
  { path: 'genres/create-genre', component: CreateGenreComponent },
  { path: 'genres/update-genre/:id', component: UpdateGenreComponent },
  { path: 'authors', component: AuthorComponent },
  { path: 'authors/create-author', component: CreateAuthorComponent },
  { path: 'authors/update-author/:id', component: UpdateAuthorComponent },
  { path: 'publishers', component: PublisherComponent },
  { path: 'publishers/create-publisher', component: CreatePublisherComponent },
  { path: 'publishers/update-publisher/:id', component: UpdatePublisherComponent },
  { path: 'members', component: MemberComponent },
  { path: 'members/create-member', component: CreateMemberComponent },
  { path: 'members/update-member/:id', component: UpdateMemberComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
