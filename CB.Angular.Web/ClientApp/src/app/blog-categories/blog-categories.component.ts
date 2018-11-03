import { Component, Inject } from '@angular/core';
import { DataService } from '../../services/data.service';

import { BlogPostCategory } from '../../models/blogpostcategory';

@Component({
  selector: 'blog-categories',
  templateUrl: './blog-categories.component.html',
  styleUrls: ['./blog-categories.component.css']
})

export class BlogCategoriesComponent {
  private _categories: Array<BlogPostCategory> | undefined;

  constructor(public dataService: DataService) { }

  ngOnInit() {
    this.getBlogCategories();
  }

  getBlogCategories() {
    this.dataService.get<Array<BlogPostCategory>>('api/cms/get-blog-categories').subscribe(data => {
      this._categories = data;
    });
  }
}
