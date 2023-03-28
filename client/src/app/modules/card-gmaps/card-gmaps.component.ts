import { Component, ViewChild } from '@angular/core';
import { GoogleMap, MapInfoWindow } from '@angular/google-maps';

@Component({
  selector: 'app-card-gmaps',
  templateUrl: './card-gmaps.component.html',
  styleUrls: ['./card-gmaps.component.scss']
})
export class CardGmapsComponent {

  @ViewChild(GoogleMap, { static: false }) map: GoogleMap;
  @ViewChild(MapInfoWindow, { static: false }) info: MapInfoWindow;

  zoom = 12;
  center: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    mapTypeId: 'hybrid',
    zoomControl: false,
    scrollwheel: false,
    disableDoubleClickZoom: true,
    maxZoom: 15,
    minZoom: 8,
  };
  markers = [];

  ngOnInit(): void {
    navigator.geolocation.getCurrentPosition((position) => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
      };

      this.addMarker()
    })

    // setTimeout(() => {
    //   var input = document.getElementById('location') as HTMLInputElement;
    //   var options = {
    //     types: ['(cities)'],
    //     // componentRestrictions: { country: 'fr' }
    //   };
    //   var autocomplete = new google.maps.places.Autocomplete(input, options);
    // }, 2000);
  }

  addMarker() {
    this.markers.push({
      position: {
        lat: this.center.lat,
        lng: this.center.lng,
      },
      label: {
        color: 'red',
        text: 'Your location',
      },
      title: 'Your location',
      info: 'Your location',
      options: {
        animation: google.maps.Animation.BOUNCE,
      },
    });
  }
}
