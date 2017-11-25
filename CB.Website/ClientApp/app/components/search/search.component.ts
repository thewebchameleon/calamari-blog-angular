import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css'],
})
export class SearchComponent {
    private keyword: string;

    constructor(public dataService: DataService) { }

    ngOnInit() {
        
    }

    public searchBlogPosts(): void {
        alert(this.keyword);

        this.dataService.set('api/search');
        this.dataService.get().subscribe(res => {
           
        });
    }
}
