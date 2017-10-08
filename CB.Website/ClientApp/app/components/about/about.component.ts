import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UtilityService } from '../../services/utility.service';
import { NotificationService } from '../../services/notification.service';

import { Profile } from '../../models/profile';

@Component({
    selector: 'about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.css']
})
export class AboutComponent {
    private _profile: Profile;
    private _resource: string;

    constructor(
        http: Http,
        @Inject('BASE_URL') baseUrl: string,
        utilityService: UtilityService,
        notificationService: NotificationService) {

        http.get(baseUrl + 'api/profile').subscribe(result =>
        {
            this._profile = result.json() as Profile;
        },
            error => console.error(error)
        );
    }
}