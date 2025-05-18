import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

import { setMaxListeners } from 'events';
setMaxListeners(50);


bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
