import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigurationService } from './core/services/configuration/configuration.service';
import { RequestFormComponent } from './ui/components/requestform.component';

@NgModule({
  declarations: [
    AppComponent,
    RequestFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      deps: [ConfigurationService],
      useFactory: (configService: ConfigurationService) => {
        return async () => {
          await configService.loadAppConfig();
        }
      },
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }