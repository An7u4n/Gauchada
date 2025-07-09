namespace Gauchada.Backend.Model.DTO
{
    public class CarDTO
    {
        public required string CarPlate { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string Color { get; set; }
        public required string OwnerUserName { get; set; }
        public int MaxPassengers { get; set; }
        public override bool Equals(object? obj)
        {
            var other = obj as CarDTO;

            if (other == null)
                return false;

            return this.CarPlate == other.CarPlate &&
                   this.Brand == other.Brand &&
                   this.Model == other.Model &&
                   this.Color == other.Color &&
                   this.OwnerUserName == other.OwnerUserName &&
                   this.MaxPassengers == other.MaxPassengers;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarPlate, Brand, Model, Color, OwnerUserName, MaxPassengers);
        }
    }
}
