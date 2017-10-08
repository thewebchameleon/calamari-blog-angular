import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';
import { ActivatedRoute } from "@angular/router";

import { BlogPost } from '../../models/blogpost';

@Component({
    selector: 'blog-posts',
    templateUrl: './blog-posts.component.html',
    styleUrls: ['./blog-posts.component.css']
})
export class BlogPostsComponent {
    private _utilityService: UtilityService;
    private _resource: string;
    private _posts: Array<BlogPost>;

    constructor(
        http: Http,
        @Inject('BASE_URL') baseUrl: string,
        utilityService: UtilityService,
        notificationService: NotificationService,
        route: ActivatedRoute) {

        this._utilityService = utilityService;
        route.params.subscribe(params => {

            var id = params['Id'];
            if (id == undefined) {
                this._resource = 'api/blog/GetBlogPosts';
            } else {
                this._resource = 'api/blog/GetBlogPostsByCategoryID?id=' + id;
            }

            http.get(baseUrl + this._resource).subscribe(result => {
                this._posts = result.json() as Array<BlogPost>;
            },
                error => console.error(error)
            );
        });
    }

    public removeHTMLtags(text: string) {
        return this._utilityService.removeHTMLtags(text);
    }
}