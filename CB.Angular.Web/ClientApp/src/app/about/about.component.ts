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
  private _profile: Profile | undefined;

  constructor(public dataService: DataService) { }

  async ngOnInit() {
    this.getProfile();
  }

  getProfile() {
    this.dataService.get<Profile>('api/cms/get-profile').subscribe(data => {

      this._profile = data;

    });
  }
}
