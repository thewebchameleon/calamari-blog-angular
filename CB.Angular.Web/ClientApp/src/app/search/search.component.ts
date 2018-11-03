import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent {
  private _keyword: string | undefined;

  constructor(public dataService: DataService) { }

  ngOnInit() {

  }

  public searchBlogPosts(): void {
    alert(this._keyword);

    this.dataService.get<string>('api/search').subscribe(res => {

    });
  }
}
