import { Component, OnInit } from '@angular/core';
import { Sidebar } from "../../pages/sidebar/sidebar";
import { RouterOutlet } from '@angular/router';
import { NgClass } from '@angular/common';
@Component({
  selector: 'app-admin-layout',
  imports: [Sidebar, RouterOutlet],
  templateUrl: './admin-layout.html',
  styleUrl: './admin-layout.css',
})
export class AdminLayout{



}
