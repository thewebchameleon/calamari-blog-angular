export class BlogPostCategory {
    Id: string;
    Name: string;
    Icon: string;

    constructor(id: string,
        name: string,
        icon: string) {
        this.Id = id;
        this.Name = name;
        this.Icon = icon;
    }
}