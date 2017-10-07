import { BlogPostCategory } from './blogpostcategory';
import { BlogPostTag } from './blogposttag';

export class BlogPost {
    Id: string;
    PublishedDate: Date;
    Title: string;
    Category: BlogPostCategory;
    Tags: Array<BlogPostTag>;

    constructor(id: string,
        publishedDate: Date,
        title: string,
        body: string,
        category: BlogPostCategory,
        tags: Array<BlogPostTag>) {
        this.Id = id;
        this.PublishedDate = publishedDate;
        this.Title = title;
        this.Category = category;
        this.Tags = tags;
    }
}