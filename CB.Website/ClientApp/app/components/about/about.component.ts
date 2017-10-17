import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { DataService } from '../../services/data.service';

import { Profile } from '../../models/profile';

@Component({
    selector: 'about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.css']
})

export class AboutComponent {
    private name: string;
    private portrait: string;
    private body: string;

    constructor(public dataService: DataService) { }

    async ngOnInit() {
        this.getProfile();
    }

    getProfile() {
        this.dataService.set('api/profile');
        this.dataService.get().subscribe(res => {
            var data: Profile = res.json();

            this.name = data.name;
            this.portrait = data.portrait;
            this.body = data.body;
        });
    }
}