import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './navmenu/navmenu.component';
import { FooterComponent } from './footer/footer.component';
import { BlogComponent } from './blog/blog.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';
import { SearchComponent } from './search/search.component';
import { BlogCategoriesComponent } from './blog-categories/blog-categories.component';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BlogPostsComponent } from './blog-posts/blog-posts.component';

import { DataService } from './../services/data.service';
import { UtilityService } from './../services/utility.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FooterComponent,
    BlogComponent,
    AboutComponent,
    ContactComponent,
    SearchComponent,
    BlogCategoriesComponent,
    BlogPostsComponent,
    BlogPostComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'blog', pathMatch: 'full' },
      {
        path: 'blog', component: BlogComponent, children: [
          {
            path: '', component: BlogCategoriesComponent, children: [
              { path: '', component: BlogPostsComponent }
            ]
          },
          {
            path: 'category/:Id', component: BlogCategoriesComponent, children: [
              {
                path: '', component: BlogPostsComponent
              }
            ]
          },
          { path: 'post/:Id', component: BlogPostComponent, }
        ]
      },
      { path: 'about-me', component: AboutComponent },
      { path: 'contact-me', component: ContactComponent },
      { path: '**', redirectTo: 'home' }
    ])
  ],
  providers: [UtilityService, DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
