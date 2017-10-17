import { Component, Inject } from '@angular/core';
import { DataService } from '../../services/data.service';

import { BlogPostCategory } from '../../models/blogpostcategory';

@Component({
    selector: 'blog-categories',
    templateUrl: './blog-categories.component.html',
    styleUrls: ['./blog-categories.component.css']
})

export class BlogCategoriesComponent {
    private _categories: Array<BlogPostCategory>;

    constructor(public dataService: DataService) { }

    ngOnInit() {
        this.getBlogCategories();
    }

    getBlogCategories() {
        this.dataService.set('api/blog/get-blog-categories');
        this.dataService.get().subscribe(res => {
            var data: Array<BlogPostCategory> = res.json();
            this._categories = data;
        });
    }
}