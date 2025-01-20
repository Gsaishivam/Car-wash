import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent  {
  // The array of images that will be displayed in the carousel
  images = [
    'https://carfixo.in/wp-content/uploads/2022/05/car-wash-2.jpg',  
    'https://static.vecteezy.com/system/resources/previews/003/492/197/large_2x/car-washing-car-wash-at-the-special-place-alone-man-smiling-to-the-camera-while-washing-black-car-cleaning-car-using-high-pressure-water-concept-free-photo.JPG',  
    'https://th.bing.com/th/id/OIP.GphMe5HZcvOkD0xDsi-GxQHaE2?rs=1&pid=ImgDetMain'
  ];

  currentIndex = 0;

  // Function to go to the next image
  nextImage() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
  }

  // Function to go to the previous image
  prevImage() {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
  }

  
}

