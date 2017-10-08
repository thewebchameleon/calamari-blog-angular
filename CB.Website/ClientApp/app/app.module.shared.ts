import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { FooterComponent } from './components/footer/footer.component';
import { BlogComponent } from './components/blog/blog.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { SearchComponent } from './components/search/search.component';
import { BlogCategoriesComponent } from './components//blog-categories/blog-categories.component';
import { BlogPostComponent } from './components/blog-post/blog-post.component';
import { BlogPostsComponent } from './components/blog-posts/blog-posts.component';

import { UtilityService } from './services/utility.service';
import { NotificationService } from './services/notification.service';
import { BrowserModule } from "@angular/platform-browser/src/browser";

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
        CommonModule,
        HttpModule,
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
    providers: [UtilityService, NotificationService],
})
export class AppModuleShared {
}