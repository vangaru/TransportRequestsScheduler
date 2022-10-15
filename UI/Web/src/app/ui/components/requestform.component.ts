import { Component, OnInit } from '@angular/core';
import { RequestsHttpClientService } from 'src/app/core/services/http/requests-http-client.service';
import { Request } from 'src/app/core/services/models/request';

@Component({
  selector: 'app-requestform',
  templateUrl: './requestform.component.html',
  styleUrls: ['./requestform.component.css']
})
export class RequestFormComponent implements OnInit {

  public readonly request = new Request("", "", NaN);

  private readonly minSeatsCount: number = 1;
  private readonly maxSeatsCount: number = 3;

  constructor(private readonly requestsHttpClientService: RequestsHttpClientService) { }

  ngOnInit(): void {
  }

  public submitRequest(): void {
    if (this.validateRequest()) {
      this.requestsHttpClientService.submitRequest(this.request).subscribe();
    }
  }

  private validateRequest(): boolean {
    if (this.request.sourceBusStopName.trim() === "") {
      alert("Заполните поле отправной точки.");
      return false;
    }

    if (this.request.destinationBusStopName.trim() === "") {
      alert("Заполните поле точки назначения.");
      return false;
    }

    if (this.request.seatsCount === NaN || 
        this.request.seatsCount < this.minSeatsCount ||
        this.request.seatsCount > this.maxSeatsCount) {
          alert("Неверное количество мест.");
          return false;
        }

    return true;
  }
}