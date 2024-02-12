import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appIconStyle]'
})
export class IconStyleDirective implements OnInit{

  constructor(private el: ElementRef) { }

  @Input()
  appIconStyle: string = "";

  ngOnInit(): void {
    if (this.appIconStyle == "")
      return;
    
    var styleSplitted = this.appIconStyle.split(" ");
    for(var i = 0; i < styleSplitted.length; i++)
      this.el.nativeElement.classList.add(styleSplitted[i]);
  }
}
