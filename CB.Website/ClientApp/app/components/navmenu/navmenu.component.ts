import { Component, Inject } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Subscription } from 'rxjs/Subscription';

import { Profile } from '../../models/profile';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
})
export class NavMenuComponent {
    private currentYear: number;
    private name: string;
    private title: string;
    private portrait: string;

    constructor(public dataService: DataService) { }

    ngOnInit() {
        this.currentYear = new Date().getFullYear();
        this.getProfile();
    }

    getProfile(): void {
        this.dataService.set('api/profile');
        this.dataService.get().subscribe(res => {
            var data: Profile = res.json();

            this.name = data.name;
            this.portrait = data.portrait;
            this.title = data.title;
        });
    }
}

