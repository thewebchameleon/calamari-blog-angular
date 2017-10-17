import { Component, Inject } from '@angular/core';
import { UtilityService } from '../../services/utility.service';
import { ActivatedRoute } from "@angular/router";
import { DataService } from '../../services/data.service';

import { BlogPost } from '../../models/blogpost';

@Component({
    selector: 'blog-post',
    templateUrl: './blog-post.component.html',
    styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent {
    private _resource: string;

    private title: string;
    private categoryIcon: string;
    private categoryName: string;
    private publishedDate: Date;
    private body: string;

    constructor(
        utilityService: UtilityService,
        route: ActivatedRoute,
        public dataService: DataService) {

        route.params.subscribe(params => {

            var id = params['Id'];
            if (id == undefined) {
                utilityService.navigateToBlog();
            } else {
                this._resource = 'api/blog/get-blog-post?id=' + id;
            }
        });
    }

    ngOnInit() {
        this.getBlogPost();
    }

    getBlogPost() {
        this.dataService.set(this._resource);
        this.dataService.get().subscribe(res => {
            var data: BlogPost = res.json();

            this.body = data.body;
            this.categoryIcon = data.category.icon;
            this.categoryName = data.category.name;
            this.title = data.title;
            this.publishedDate = data.publishedDate;
        });
    }
}