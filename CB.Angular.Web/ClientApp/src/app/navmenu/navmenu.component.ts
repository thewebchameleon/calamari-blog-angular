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
  private currentYear: number | undefined;
  private profile: Profile | undefined;

  constructor(public dataService: DataService) { }

  ngOnInit() {
    this.currentYear = new Date().getFullYear();
    this.getProfile();
  }

  getProfile(): void {
    this.dataService.get<Profile>('api/cms/get-profile').subscribe(data => {

      this.profile = data;

    });
  }
}

