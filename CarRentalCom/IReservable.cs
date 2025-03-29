﻿namespace CarRentalCom;
// interface
public interface IReservable
{
    void Reserve(string customer);
    void CancelReservation();
    bool IsAvailable();
}