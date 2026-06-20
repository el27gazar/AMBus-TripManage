import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-driver',
  imports: [CommonModule],
  templateUrl: './profile-driver.html',
  styleUrl: './profile-driver.css',
})
export class ProfileDriver {
driver = {
  fullName: 'Amr',
  email: 'a597fc6320@emailinbo.live',
  phoneNumber: '01236547892',
  licenseNumber: 'KK654123',
  licenseExpiry: '2026-06-26T14:51:00',
  emergencyContact: '01236547892',
  isAvailable: false,
  createdDate: '2026-06-17T11:51:15.1184364',
  stats: {
    totalTrips: 4,
    completedTrips: 3,
    cancelledTrips: 0,
    activeTrips: 0,
    upcomingTrips: 0
  }
};
}
