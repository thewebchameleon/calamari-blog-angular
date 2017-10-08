import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';

import { BlogPostCategory } from '../../models/blogpostcategory';

@Component({
    selector: 'blog-categories',
    templateUrl: './blog-categories.component.html',
    styleUrls: ['./blog-categories.component.css']
})
export class BlogCategoriesComponent {
    private _resource: string = 'api/blog/GetBlogCategories';
    private _categories: Array<BlogPostCategory>;

    constructor(
        http: Http,
        @Inject('BASE_URL') baseUrl: string,
        utilityService: UtilityService,
        notificationService: NotificationService) {

        http.get(baseUrl + 'api/blog/GetBlogCategories').subscribe(result => {
            this._categories = result.json() as Array<BlogPostCategory>;
        },
            error => console.error(error)
        );
    }
}