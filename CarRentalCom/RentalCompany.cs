namespace CarRentalCom;
public class RentalCompany
{
    private List<Vehicle> _vehicles = new List<Vehicle>();
    private List<Reservation> _reservations = new List<Reservation>();
    
    public event Action<string>? OnNewReservation;

    public void AddVehicle(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
    }
    public void ReserveVehicle(Vehicle vehicle, string customer)
    {
        if (vehicle.Available)
        {
            vehicle.Reserve(customer);
            var reservation = new Reservation
            {
                VehicleReserved = vehicle,
                Customer = customer,
                ReservationDate = DateTime.Now
            };
            _reservations.Add(reservation);
            
            OnNewReservation?.Invoke($"New reservation by {customer} for {vehicle.Brand} {vehicle.Model}");
        }
        else
        {
            Console.WriteLine("Vehicle is not available for reservation.");
        }
    }
    public void CancelReservation(int reservationId)
    {
        var reservation = _reservations.FirstOrDefault(r => r.VehicleReserved.Id == reservationId);
        if (reservation != null)
        {
            reservation.VehicleReserved.CancelReservation();
            _reservations.Remove(reservation);
        }
    }
    public List<Vehicle> GetAllVehicles() => _vehicles;
    public List<Vehicle> GetAvailableVehicles() => _vehicles.GetAvailableVehicles();
}
