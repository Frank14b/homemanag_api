import { Component, ElementRef, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { GoogleMap, MapInfoWindow } from '@angular/google-maps';
import { DataLocation } from '../suadmin/properties/properties.types';

@Component({
  selector: 'app-card-gmaps',
  templateUrl: './card-gmaps.component.html',
  styleUrls: ['./card-gmaps.component.scss']
})
export class CardGmapsComponent implements OnInit {

  @ViewChild(GoogleMap, { static: false }) map: GoogleMap;
  @ViewChild(MapInfoWindow, { static: false }) info: MapInfoWindow;
  @ViewChild('locationInput') locationInput: ElementRef;
  @Output() data_location = new EventEmitter<DataLocation>();

  zoom = 12;
  center: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    mapTypeId: 'hybrid',
    zoomControl: false,
    scrollwheel: false,
    disableDoubleClickZoom: true,
    disableDefaultUI: true,
    fullscreenControl: true,
    maxZoom: 15,
    minZoom: 8,
  };
  markers = [];
  visible = false
  locationInputValue = ""
  dataLocation: DataLocation;

  ngOnInit(): void {
    this.visible = false

    setTimeout(() => {

      const searchBox = new google.maps.places.SearchBox(
        this.locationInput.nativeElement,
      )
  
      this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(
        this.locationInput.nativeElement
      )

      searchBox.addListener('places_changed', ()=> {

        const places = searchBox.getPlaces();
        if(places.length == 0){
          return;
        }

        this.dataLocation = {
          name: searchBox.getPlaces()[0].formatted_address,
          lat: searchBox.getPlaces()[0].geometry.location.lat(),
          lng: searchBox.getPlaces()[0].geometry.location.lng(),
          url: searchBox.getPlaces()[0].url,
          city: searchBox.getPlaces()[0].address_components.find((x) => x.types.includes("locality")).long_name,
          country: searchBox.getPlaces()[0].address_components.find((x) => x.types.includes("country")).long_name,
          countryCode: searchBox.getPlaces()[0].address_components.find((x) => x.types.includes("country")).short_name
        }

        this.center = {
          lat: this.dataLocation.lat,
          lng: this.dataLocation.lng
        }
        this.addMarker(this.center.lat, this.center.lng, this.dataLocation.name)

        this.data_location.emit(this.dataLocation)

        // const bounds = new google.maps.LatLngBounds();
        // places.forEach(place => {
        //   if(place.geometry || !place.geometry.location){
        //     return;
        //   }
        //   if(place.geometry.viewport){
        //     bounds.union(place.geometry.viewport);
        //   }else{
        //     bounds.extend(place.geometry.location);
        //   }
        // });
        // this.map.fitBounds(bounds);
      })

      navigator.geolocation.getCurrentPosition((position) => {
        this.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };

        this.addMarker(this.center.lat, this.center.lng, "Your location")
      })
    }, 1000)
  }

  addMarker(lat:number, lng:number, name: string) {
    this.markers = [];

    this.markers.push({
      position: {
        lat: lat,
        lng: lng,
      },
      label: {
        color: 'red',
        text: name,
      },
      title: name,
      info: name,
      options: {
        animation: google.maps.Animation.BOUNCE,
      },
    });
  }

  getInputPlace(ev:any)
  {
    this.locationInputValue = ev.target.value
    this.locationInput.nativeElement.focus()
  }
}
