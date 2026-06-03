import { Component } from '@angular/core';
import { RouterLink } from "@angular/router";
import { Testimonials } from "../testimonials/testimonials";
import { StatisticsSection } from "../statistics-section/statistics-section";
import { Footer } from "../footer/footer";

@Component({
  selector: 'app-home',
  imports: [RouterLink, Testimonials, StatisticsSection, Footer],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {

}
