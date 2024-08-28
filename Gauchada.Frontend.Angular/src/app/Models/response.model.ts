export class ApiResponse {
  constructor(
    public data: any,
    public message: string,
    public status: number
  ) { }
}