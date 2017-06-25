import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafeHtmlPipe } from './safe-html.pipe';
import { SafeUrlPipe } from './safe-url.pipe';
import { SafeResourceUrlPipe } from './safe-resource-url.pipe';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        SafeHtmlPipe,
        SafeUrlPipe,
        SafeResourceUrlPipe
    ],
    exports: [
        SafeHtmlPipe,
        SafeUrlPipe,
        SafeResourceUrlPipe
    ],
})
export class SharedPipesModule { }
