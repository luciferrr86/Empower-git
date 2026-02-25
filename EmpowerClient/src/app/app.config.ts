import { ColorPickerDirective } from 'ngx-color-picker';
import { ApplicationConfig, importProvidersFrom, provideZonelessChangeDetection } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { RouterModule, RouterOutlet, provideRouter } from '@angular/router';
import { environment } from '../environments/environment';
import { ToastrModule } from 'ngx-toastr';
import { FlatpickrDefaults } from 'angularx-flatpickr';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), provideZonelessChangeDetection(),

    provideClientHydration(), RouterOutlet, RouterModule, BrowserModule, provideAnimations(), FlatpickrDefaults,

    BrowserModule,

    provideHttpClient(),

    importProvidersFrom(ColorPickerDirective,

      ToastrModule.forRoot({
        timeOut: 15000, // 15 seconds
        closeButton: true,
        progressBar: true,

      }),),]

};
