import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';

import { Global } from '../../models/global';

@Component({
  selector: 'router-outlet-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  private heartIcon: string | undefined;
  private applicationName: string | undefined;

  constructor(public dataService: DataService) { }

  ngOnInit() {
    this.getGlobalConfig();
  }

  getGlobalConfig(): void {
    this.dataService.get<Global>('api/cms/get-global-config').subscribe(data => {

      this.heartIcon = data.heartIconURL;
      this.applicationName = data.applicationName;

    });
  }
}
