import { User } from "./user.model";

export class Trip {
  constructor(
    public tripId: number,
    public origin: string,
    public destination: string,
    public startDate: string,
    public driver: User,
    public carPlate: string
  ) { }
}
