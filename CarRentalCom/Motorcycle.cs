namespace CarRentalCom;
public class Motorcycle : Vehicle, IReservable
{
    private int EngineCapacity { get; set; }
    private Reservation _reservation = new();
    
    public Motorcycle(int id, string brand, string model, int year, int engineCapacity)
    {
        this.Id = id;
        this.Brand = brand;
        this.Model = model;
        this.Year = year;
        this.EngineCapacity = engineCapacity;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Motorcycle: {Brand} {Model} ({Year}) : Engine Capacity: {EngineCapacity}");
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