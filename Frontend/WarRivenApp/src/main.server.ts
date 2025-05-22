import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config.server';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { AuthInterceptor } from './app/auth.interceptor';

export default function () {
  return bootstrapApplication(AppComponent, {
    providers: [
      provideHttpClient(
        withFetch(),
        withInterceptors([AuthInterceptor])
      ),
      ...appConfig.providers
    ]
  });
}