import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {

  showToaster(message: string) {
     document.getElementsByClassName("toast-message")[0].innerHTML = message;
            document.getElementById("toastError")?.classList.add("show");
            setTimeout(() => {
            document.getElementById("toastError")?.classList.remove("show");
            },4000);
  }

  closeModal(){
  document.getElementsByClassName("overlay")[0].classList.toggle("hide");
  document.getElementsByClassName("modal")[0].classList.toggle("hide");
}
}
