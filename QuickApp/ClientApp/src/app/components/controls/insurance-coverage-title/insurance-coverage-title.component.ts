import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-insurance-coverage-title',
  templateUrl: './insurance-coverage-title.component.html',
  styleUrls: ['./insurance-coverage-title.component.scss']
})
export class InsuranceCoverageTitleComponent {
  @Input()
  cssStyleTitle = "";

  @Input()
  titleDescription = "";
}
