import { Driver } from "./driver.model";

export class Trip {
  constructor(
    public tripId: number,
    public origin: string,
    public destination: string,
    public startDate: string,
    public driver: Driver,
    public carPlate: string
  ) { }
}
