import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Info, InfoService } from 'src/app/backend/info.service';

@Component({
  selector: 'info-detail',
  templateUrl: './info-detail.component.html',
  styleUrls: ['./info-detail.component.scss'],
})
export class InfoDetailComponent implements OnInit {
  public info: Info;
  public filteredInfo: Info;
  constructor(public router: Router, public infoService: InfoService) {
    this.infoService.currentInfo$.subscribe(inf => {
      this.info = inf;
      this.filteredInfo = inf;
    }
    );
  }

  ngOnInit() {
  }

  public onFilterText(event) {
    const query = event.target.value.toLowerCase();
    console.log("### ", query);
    if(!query || (query && query === "")) {
      this.filteredInfo = this.info;
      console.log("### ", this.info.text);
    }
    if(this.info) {
      const splitted = this.info.text.split("\n");
      const includes = [];
      for(const split of splitted) {
        if(split.toLowerCase().includes(query))
          includes.push(split);
      }
      this.filteredInfo.text = includes.join("\n");
    }
  }
}
