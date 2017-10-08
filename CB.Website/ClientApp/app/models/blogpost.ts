import { BlogPostCategory } from './blogpostcategory';
import { BlogPostTag } from './blogposttag';

export interface BlogPost {
    id: string;
    publishedDate: Date;
    title: string;
    category: BlogPostCategory;
    tags: Array<BlogPostTag>;
}