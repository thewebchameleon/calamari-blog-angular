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
  private _post: BlogPost | undefined;

  constructor(
    utilityService: UtilityService,
    route: ActivatedRoute,
    public dataService: DataService) {

    this._resource = '';

    route.params.subscribe(params => {

      var id = params['Id'];
      if (id == undefined) {
        utilityService.navigateToBlog();
      } else {
        this._resource = 'api/cms/get-blog-post?id=' + id;
      }
    });

  }

  ngOnInit() {
    this.getBlogPost();
  }

  getBlogPost() {
    this.dataService.get<BlogPost>(this._resource).subscribe(data => {

      this._post = data;

    });
  }
}
