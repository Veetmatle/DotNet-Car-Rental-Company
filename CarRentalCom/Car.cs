namespace CarRentalCom
{
    public class Car : Vehicle, IReservable
    {
        private string BodyType { get; set; }
        private Reservation _reservation = new Reservation();

        public Car(int id, string brand, string model, int year, string bodyType)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.BodyType = bodyType;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Car: {Brand} {Model} ({Year}) - Body Type: {BodyType}, IsAvailable: {Available}");
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public override void Reserve(string customer)
        {
            if (Available)
            {
                _reservation = new Reservation
                {
                    VehicleReserved = this,
                    Customer = customer,
                    ReservationDate = DateTime.Now
                };
                Available = false;
            }
            else
            {
                Console.WriteLine("Vehicle is not available for reservation.");
            }
        }

        public override void CancelReservation()
        {
            Console.WriteLine($"\nVehicle {this.Brand} {this.Model} reservation has been cancelled.");
            Available = true;
        }
    }
}