import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './shared/components/navbar/navbar';
import { Footer } from "./shared/components/footer/footer";
import { inject } from '@vercel/analytics';
import { injectSpeedInsights } from '@vercel/speed-insights';

@Component({
  selector: 'app-root',
  imports: [Navbar, RouterOutlet, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit{
  title = 'IndexMe';

  ngOnInit(): void {
    inject();
    injectSpeedInsights();
  }
}
