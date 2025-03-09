import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HttpClient } from '@angular/common/http';
import * as intlTelInput from 'intl-tel-input';
import { NgxIntlTelInputModule } from 'ngx-intl-tel-input';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoginComponent, NgxIntlTelInputModule],
  providers: [HttpClient],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'phoneBook';
}
