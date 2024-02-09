import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appIconStyle]'
})
export class IconStyleDirective implements OnInit{

  constructor(private el: ElementRef) { }

  @Input()
  appIconStyle: string = "";

  ngOnInit(): void {
    this.el.nativeElement.classList.add(this.appIconStyle);
  }
}
