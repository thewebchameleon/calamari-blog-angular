import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';

import { Global } from '../../models/global';

@Component({
    selector: 'router-outlet-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.css']
})
export class FooterComponent {
    private heartIcon: string;
    private applicationName: string;

    constructor(public dataService: DataService) { }

    ngOnInit() {
        this.getGlobalConfig();
    }

    getGlobalConfig(): void {
        this.dataService.set('api/global');
        this.dataService.get().subscribe(res => {
            var data: Global = res.json();

            this.heartIcon = data.heartIconURL;
            this.applicationName = data.applicationName;
        });
    }
}
