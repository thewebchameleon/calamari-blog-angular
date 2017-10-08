import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';
import { ActivatedRoute } from "@angular/router";

import { BlogPost } from '../../models/blogpost';

@Component({
    selector: 'blog-post',
    templateUrl: './blog-post.component.html',
    styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent {
    private _resource: string;
    private _post: BlogPost;

    constructor(
        http: Http,
        @Inject('BASE_URL') baseUrl: string,
        utilityService: UtilityService,
        notificationService: NotificationService,
        route: ActivatedRoute) {

        route.params.subscribe(params => {

            var id = params['Id'];
            if (id == undefined) {
                utilityService.navigateToBlog();
            } else {
                this._resource = 'api/blog/GetBlogPost?id=' + id;
            }

            http.get(baseUrl + this._resource).subscribe(result => {
                this._post = result.json() as BlogPost;
            },
                error => console.error(error)
            );
        });
    }
}