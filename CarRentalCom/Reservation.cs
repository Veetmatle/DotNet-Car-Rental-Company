namespace CarRentalCom
{
    public class Reservation
    {
        public Vehicle VehicleReserved { get; set; }
        public string Customer { get; set; }
        public DateTime ReservationDate { get; set; }

        public override string ToString()
        {
            return $"Reservation for {Customer} : {VehicleReserved.Brand} {VehicleReserved.Model} on {ReservationDate}";
        }
    }
}