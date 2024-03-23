import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { InsuranceCategoryPolicyCard, InsuranceCoveragePolicyFooter } from 'src/app/models/insurance-coverage';
import { CardComponent } from '../../controls/card/card.component';
import { TxtStyleDirective } from 'src/app/directives/txt-style.directive';
import { IconStyleDirective } from 'src/app/directives/icon-style.directive';

@Component({
  selector: 'app-insurancecoverage',
  templateUrl: './insurancecoverage.component.html',
  styleUrls: ['./insurancecoverage.component.scss']
})
export class InsurancecoverageComponent implements OnInit {
  isShowen: boolean;

  @Input()
  title = "";

  @Input()
  insuranceCoverageCards: InsuranceCategoryPolicyCard[] = null;

  @Output()
  insuranceCoverageAdded = new EventEmitter<InsuranceCategoryPolicyCard>();

  @Output()
  insuranceCoverageRemoved = new EventEmitter<string>();

  @ViewChild('flexSwitchCheckChecked')
  searchInput!: ElementRef;

  ngOnInit(): void {
    this.isShowen = false;
  }
  
  IsShowen():boolean {
    return this.isShowen;
  }

  Show(): void { 
    debugger;
    this.isShowen = true; 
  }

  Hide(): void { 
    debugger;
    this.isShowen = false; 
  }

  selectItem(event):void { 
    var isChecked = event.target.checked;
    var selectedValue = event.target.defaultValue;
    if (isChecked){
        var indexSelected = this.insuranceCoverageCards.findIndex(x => x.code == selectedValue);
        this.insuranceCoverageAdded.emit(this.insuranceCoverageCards[indexSelected]);
    }
    else
    {
        this.insuranceCoverageRemoved.emit(selectedValue);
    }
  }
}
