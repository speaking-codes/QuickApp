import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appTextMultiline]'
})
export class TextMultilineDirective implements OnInit{

  constructor(private el: ElementRef) { }

  @Input()
  appTextMultiline: string = "";

  ngOnInit(): void {
    debugger;
    var textLines = this.appTextMultiline.split('\n');
    var innerHtml = "";
    textLines.forEach(item =>{
      innerHtml = innerHtml + item + '<br />';
    });
    console.log(this.appTextMultiline);
    this.el.nativeElement.innerHtml = innerHtml;
  }
}
