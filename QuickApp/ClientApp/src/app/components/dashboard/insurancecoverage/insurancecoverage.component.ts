import { Component, Input } from '@angular/core';
import { InsuranceCategoryPolicyCard } from 'src/app/models/insurance-coverage';
import { CardComponent } from '../../controls/card/card.component';
import { TxtStyleDirective } from 'src/app/directives/txt-style.directive';
import { IconStyleDirective } from 'src/app/directives/icon-style.directive';

@Component({
  selector: 'app-insurancecoverage',
  templateUrl: './insurancecoverage.component.html',
  styleUrls: ['./insurancecoverage.component.scss']
})
export class InsurancecoverageComponent {
  isShowen: boolean;

  @Input()
  title = "";

  @Input()
  insuranceCoverageCards: InsuranceCategoryPolicyCard[] = null;

  IsShowen():boolean {
    return this.isShowen;
  }

  Show(): void { this.isShowen = true; }

  Hide(): void { this.isShowen = false; }

  selectItem(event):void { 
    console.log(event.target.checked);
    console.log(event.target.defaultValue);
  }
}
