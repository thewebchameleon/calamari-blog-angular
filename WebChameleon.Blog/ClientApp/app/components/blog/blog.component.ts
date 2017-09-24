import { Component } from '@angular/core';
import { DataService } from "../../services/api.service";

@Component({
    selector: 'blog',
    templateUrl: './blog.component.html',
    styleUrls: ['./blog.component.css']
})
export class BlogComponent {

    constructor(private blogService: DataService) { }

    ngOnInit() {
    }
}
