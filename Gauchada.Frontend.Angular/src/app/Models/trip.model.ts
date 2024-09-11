import { User } from "./user.model";

export class Trip {
  constructor(
    public tripId: number,
    public origin: string,
    public destination: string,
    public startDate: string,
    public driverUserName: string,
    public carPlate: string
  ) { }
}
