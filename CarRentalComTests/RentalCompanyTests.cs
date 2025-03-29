using CarRentalCom;
using NUnit.Framework;
using System;
using System.Linq;

namespace CarRentalComTests;

public class RentalCompanyTests
{
    [Test]
    public void AddVehicle_ShouldAddVehicleToCollection()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");

        // Act
        rentalCompany.AddVehicle(car);

        // Assert
        Assert.That(rentalCompany.GetAllVehicles(), Contains.Item(car));
        Assert.That(rentalCompany.GetAllVehicles().Count, Is.EqualTo(1));
    }

    [Test]
    public void ReserveVehicle_ShouldReserveAvailableVehicle()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var customer = "Magnus Carlsen";
        rentalCompany.AddVehicle(car);

        // Act
        rentalCompany.ReserveVehicle(car, customer);

        // Assert
        Assert.That(car.Available, Is.False);
    }

    [Test]
    public void ReserveVehicle_ShouldNotReserveUnavailableVehicle()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var customer1 = "Magnus Carlsen";
        var customer2 = "Hikaru Nakamura";
        rentalCompany.AddVehicle(car);

        // Act
        rentalCompany.ReserveVehicle(car, customer1);
        rentalCompany.ReserveVehicle(car, customer2);

        // Assert
        Assert.That(car.Available, Is.False);
    }

    [Test]
    public void CancelReservation_ShouldMakeVehicleAvailableAgain()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var customer = "Magnus Carlsen";
        rentalCompany.AddVehicle(car);
        rentalCompany.ReserveVehicle(car, customer);

        // Act
        rentalCompany.CancelReservation(car.Id);

        // Assert
        Assert.That(car.Available, Is.True);
    }

    [Test]
    public void GetAvailableVehicles_ShouldReturnOnlyAvailableVehicles()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car1 = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var car2 = new Car(2, "Honda", "Civic", 2023, "Sedan");
        var motorcycle = new Motorcycle(3, "Harley-Davidson", "Street 750", 2021, 750);
        
        rentalCompany.AddVehicle(car1);
        rentalCompany.AddVehicle(car2);
        rentalCompany.AddVehicle(motorcycle);
        
        rentalCompany.ReserveVehicle(car1, "Magnus Carlsen");

        // Act
        var availableVehicles = rentalCompany.GetAvailableVehicles();

        // Assert
        Assert.That(availableVehicles.Count, Is.EqualTo(2));
        Assert.That(availableVehicles, Contains.Item(car2));
        Assert.That(availableVehicles, Contains.Item(motorcycle));
        Assert.That(availableVehicles, Does.Not.Contain(car1));
    }

    [Test]
    public void OnNewReservation_ShouldTriggerEventWhenReservationMade()
    {
        // Arrange
        var rentalCompany = new RentalCompany();
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var customer = "Magnus Carlsen";
        string? notificationMessage = null;
        
        rentalCompany.AddVehicle(car);
        rentalCompany.OnNewReservation += message => notificationMessage = message;

        // Act
        rentalCompany.ReserveVehicle(car, customer);

        // Assert
        Assert.That(notificationMessage, Is.Not.Null);
        Assert.That(notificationMessage, Does.Contain("Magnus Carlsen"));
        Assert.That(notificationMessage, Does.Contain("Toyota"));
        Assert.That(notificationMessage, Does.Contain("Camry"));
    }
    
}

public class VehicleTests
{
    [Test]
    public void Car_ShouldBeInitializedWithCorrectProperties()
    {
        // Arrange, Act
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");

        // Assert
        Assert.That(car.Id, Is.EqualTo(1));
        Assert.That(car.Brand, Is.EqualTo("Toyota"));
        Assert.That(car.Model, Is.EqualTo("Camry"));
        Assert.That(car.Year, Is.EqualTo(2022));
        Assert.That(car.Available, Is.True);
    }

    [Test]
    public void CarIsAvailable_ShouldReturnTrueWhenVehicleIsAvailable()
    {
        var car  = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        Assert.That(car.IsAvailable, Is.True);
    }

    [Test]
    public void Motorcycle_ShouldBeInitializedWithCorrectProperties()
    {
        // Arrange & Act
        var motorcycle = new Motorcycle(2, "Harley-Davidson", "Street 750", 2021, 750);

        // Assert
        Assert.That(motorcycle.Id, Is.EqualTo(2));
        Assert.That(motorcycle.Brand, Is.EqualTo("Harley-Davidson"));
        Assert.That(motorcycle.Model, Is.EqualTo("Street 750"));
        Assert.That(motorcycle.Year, Is.EqualTo(2021));
        Assert.That(motorcycle.Available, Is.True);
    }
}

public class ReservationTests
{
    [Test]
    public void Reservation_ShouldBeCreatedWithCorrectProperties()
    {
        // Arrange
        var car = new Car(1, "Toyota", "Camry", 2022, "Sedan");
        var customer = "Magnus Carlsen";
        var now = DateTime.Now;
        
        // Act
        var reservation = new Reservation
        {
            VehicleReserved = car,
            Customer = customer,
            ReservationDate = now
        };
        
        // Assert
        Assert.That(reservation.VehicleReserved, Is.EqualTo(car));
        Assert.That(reservation.Customer, Is.EqualTo(customer));
        Assert.That(reservation.ReservationDate, Is.EqualTo(now).Within(TimeSpan.FromSeconds(1)));
    }
}