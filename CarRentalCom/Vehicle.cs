namespace CarRentalCom
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        
        public bool Available { get; set; } = true;
        public abstract void DisplayInfo();
        public abstract void Reserve(string customer);
        public abstract void CancelReservation();
    }
}