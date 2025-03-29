namespace CarRentalCom
{
    class Program
    {
        static void Main(string[] args)
        {
            var rentalCompany = new RentalCompany();
            rentalCompany.OnNewReservation += message => Console.WriteLine($"Notification: {message}");
            
            var car1 = new Car(1,
                "Toyota",
                "Camry",
                2022,
                "Sedan");


            var motorcycle1 = new Motorcycle(2,
                "Harley-Davidson",
                "Street 750",
                2021,
                750);
            
            
            rentalCompany.AddVehicle(car1);
            rentalCompany.AddVehicle(motorcycle1);
            
            Console.WriteLine("\n\nAll Vehicles:");
            foreach (var vehicle in rentalCompany.GetAllVehicles())
            {
                vehicle.DisplayInfo();
            }
            
            rentalCompany.ReserveVehicle(car1, "John Doe");
            
            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var vehicle in rentalCompany.GetAvailableVehicles())
            {
                vehicle.DisplayInfo();
            }
            
            rentalCompany.CancelReservation(car1.Id);
            
            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var vehicle in rentalCompany.GetAvailableVehicles())
            {
                vehicle.DisplayInfo();
            }
        }
    }
}