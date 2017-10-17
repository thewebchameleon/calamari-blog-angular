import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { DataService } from '../../services/data.service';
import { UtilityService } from '../../services/utility.service';

import { BlogPost } from '../../models/blogpost';

@Component({
    selector: 'blog-posts',
    templateUrl: './blog-posts.component.html',
    styleUrls: ['./blog-posts.component.css']
})
export class BlogPostsComponent {
    private _resource: string;
    private _posts: Array<BlogPost>;

    constructor(
        public utilityService: UtilityService,
        public dataService: DataService,
        route: ActivatedRoute) {

        route.params.subscribe(params => {

            var id = params['Id'];
            if (id == undefined) {
                this._resource = 'api/blog/get-blog-posts';
            } else {
                this._resource = 'api/blog/get-blog-posts-by-category-id?id=' + id;
            }
            this.getBlogPosts();
        });
    }

     ngOnInit() {
        
    }

    getBlogPosts() {
        this.dataService.set(this._resource);
        this.dataService.get().subscribe(res => {
            var data: Array<BlogPost> = res.json();
            this._posts = data;
        });
    }

    public removeHTMLtags(text: string) {
        return this.utilityService.removeHTMLtags(text);
    }
}