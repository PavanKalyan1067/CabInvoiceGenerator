using System;
using System.Collections.Generic;
using System.Text;
namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        //Declaring ride Type
        RideType type;
        //Declaring Varaibles
        private double MINIMUM_COST_PER_KM;
        private int COST_PER_TIME;
        private double MINIMUM_FARE;
        //Paramerterized Constructor
        public InvoiceGenerator(RideType type)
        {
            this.type = type;
            //Initializing varaibles for Normal Ride
            if (type.Equals(RideType.NORMAL_RIDE))
            {
                this.MINIMUM_COST_PER_KM = 10;
                this.COST_PER_TIME = 1;
                this.MINIMUM_FARE = 5;
            }
            //Initializing varaibles for Premium Ride
            if (this.type.Equals(RideType.PREMIUM_RIDE))
            {
                this.MINIMUM_COST_PER_KM = 15;
                this.COST_PER_TIME = 1;
                this.MINIMUM_FARE = 20;
            }
        }
        //Method to Calculate the fare and handling exception
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0.0;
            try
            {
                if (!(distance <= 0 && time <= 0))
                {
                    totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
                }
                else if (distance <= 0)
                {
                    throw new CustomException(CustomException.ExceptionType.INVALID_DISTANCE, "Distance should be positive integer");
                }
                else if (time <= 0)
                {
                    throw new CustomException(CustomException.ExceptionType.INVALID_DISTANCE, "Time should be positive integer");
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.message);
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        //Method to calculate the totalfare for multiple rides 
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                if (rides == null)
                {
                    throw new CustomException(CustomException.ExceptionType.NULL_RIDES, "No rides found");
                }
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.message);
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
    }
}