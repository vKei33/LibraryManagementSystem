import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GenreComponent } from './features/feature-genre/components/genre/genre.component';
import { AuthorComponent } from './features/feature-author/components/author/author.component';
import { CreateGenreComponent } from './features/feature-genre/components/create-genre/create-genre.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UpdateGenreComponent } from './features/feature-genre/components/update-genre/update-genre.component';
import { CreateAuthorComponent } from './features/feature-author/components/create-author/create-author.component';
import { UpdateAuthorComponent } from './features/feature-author/components/update-author/update-author.component';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { HeaderComponent } from './shared/header/header.component';
import { PublisherComponent } from './features/feature-publisher/components/publisher/publisher.component';
import { CreatePublisherComponent } from './features/feature-publisher/components/create-publisher/create-publisher.component';
import { UpdatePublisherComponent } from './features/feature-publisher/components/update-publisher/update-publisher.component';
import { MemberComponent } from './features/feature-member/components/member/member.component';
import { CreateMemberComponent } from './features/feature-member/components/create-member/create-member.component';
import { UpdateMemberComponent } from './features/feature-member/components/update-member/update-member.component';
import { BookComponent } from './features/feature-book/components/book/book.component';
import { CreateBookComponent } from './features/feature-book/components/create-book/create-book.component';
import { UpdateBookComponent } from './features/feature-book/components/update-book/update-book.component';
import { RentComponent } from './features/feature-rent/components/rent/rent.component';
import { CreateRentComponent } from './features/feature-rent/components/create-rent/create-rent.component';
import { UpdateRentComponent } from './features/feature-rent/components/update-rent/update-rent.component';
import { BookDetailComponent } from './features/feature-book/components/book-detail/book-detail.component';
import { LoginComponent } from './shared/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    GenreComponent,
    AuthorComponent,
    CreateGenreComponent,
    UpdateGenreComponent,
    CreateAuthorComponent,
    UpdateAuthorComponent,
    NavigationComponent,
    HeaderComponent,
    PublisherComponent,
    CreatePublisherComponent,
    UpdatePublisherComponent,
    MemberComponent,
    CreateMemberComponent,
    UpdateMemberComponent,
    BookComponent,
    CreateBookComponent,
    UpdateBookComponent,
    RentComponent,
    CreateRentComponent,
    UpdateRentComponent,
    BookDetailComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
