import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appTxtStyle]'
})
export class TxtStyleDirective implements OnInit {

  constructor(private el: ElementRef) { }

  @Input()
  appTxtStyle: string = "";

  ngOnInit(): void {
    this.el.nativeElement.style.color = this.appTxtStyle;
  }
}
