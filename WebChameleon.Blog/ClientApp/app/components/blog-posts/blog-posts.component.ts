import { Component } from '@angular/core';
import { DataService } from '../../services/api.service';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';

import { BlogPost } from "../../models/blogpost";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'blog-posts',
    templateUrl: './blog-posts.component.html',
    styleUrls: ['./blog-posts.component.css']
})
export class BlogPostsComponent {
    private _apiResource_All: string = 'api/blog/GetBlogPosts';
    private _apiResource_ByTagId: string = 'api/blog/GetBlogPostsByCategoryID';
    private _posts: Array<BlogPost>;
    private _tagId: string;

    constructor(public blogService: DataService,
        public utilityService: UtilityService,
        public notificationService: NotificationService,
        private route: ActivatedRoute
    ) {

        this.route.params.subscribe(params => this._tagId = params['Id']);
    }

    ngOnInit() {
        if (this._tagId == undefined || this._tagId == '') {
            this.blogService.set(this._apiResource_All);
        } else {
            this.blogService.set(this._apiResource_ByTagId + '/' + this._tagId);
        }
        this.getBlogPosts();
    }

    getBlogPosts(): void {
        this.blogService.get()
            .subscribe(res => {
                var data: any = res.json();
                this._posts = data.map(function (item: any) {
                    return new BlogPost(item.id, item.PublishedDate, item.title, item.body, item.category, item.tags);
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
