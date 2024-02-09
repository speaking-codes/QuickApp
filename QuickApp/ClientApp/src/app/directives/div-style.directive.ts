import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appDivStyle]'
})
export class DivStyleDirective implements OnInit {

  constructor(private el: ElementRef) { }

  @Input()
  appDivStyle: string = "";

  ngOnInit(): void {
    this.el.nativeElement.classList.add(this.appDivStyle);
  }
}
