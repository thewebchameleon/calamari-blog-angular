import { Component } from '@angular/core';
import { DataService } from '../../services/api.service';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';

import { BlogPostCategory } from "../../models/blogpostcategory";

@Component({
    selector: 'blog-categories',
    templateUrl: './blog-categories.component.html',
    styleUrls: ['./blog-categories.component.css']
})
export class BlogCategoriesComponent {
    private _apiResource: string = 'api/blog/GetBlogCategories';
    private _categories: Array<BlogPostCategory>;

    constructor(public blogService: DataService,
        public utilityService: UtilityService,
        public notificationService: NotificationService) {
    }

    ngOnInit() {
        this.blogService.set(this._apiResource);
        this.getCategories();
    }

    getCategories(): void {
        this.blogService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._categories = data.map(function (item: any) {
                    return new BlogPostCategory(item.id, item.name, item.icon)
                });
            },
            error => {

                if (error.status == 401 || error.status == 404) {
                    this.notificationService.printErrorMessage('Authentication required');
                    this.utilityService.navigateToHome();
                }
            });
    }
}
