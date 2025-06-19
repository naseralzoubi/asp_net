using System;
using InsuranceQuoteCalculator.Models;

namespace InsuranceQuoteCalculator.Services
{
    public class QuoteBuilder
    {
        private readonly Insuree _insuree;
        private decimal _quote;

        public QuoteBuilder(Insuree insuree)
        {
            _insuree = insuree;
            _quote = 50m; // Base quote
        }

        public QuoteBuilder ApplyAgeRules()
        {
            int age = DateTime.Now.Year - _insuree.DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < _insuree.DateOfBirth.DayOfYear)
                age--;

            if (age <= 18)
                _quote += 100m;
            else if (age <= 25)
                _quote += 50m;
            else
                _quote += 25m;

            return this;
        }

        public QuoteBuilder ApplyCarRules()
        {
            if (_insuree.CarYear < 2000 || _insuree.CarYear > 2015)
                _quote += 25m;

            if (_insuree.CarMake.ToLower() == "porsche")
            {
                _quote += 25m;
                if (_insuree.CarModel.ToLower() == "911 carrera")
                    _quote += 25m;
            }

            return this;
        }

        public QuoteBuilder ApplyDrivingHistoryRules()
        {
            _quote += 10m * _insuree.SpeedingTickets;
            if (_insuree.DUI)
                _quote *= 1.25m;

            return this;
        }

        public QuoteBuilder ApplyCoverageRules()
        {
            if (_insuree.FullCoverage)
                _quote *= 1.5m;

            return this;
        }

        public decimal Build()
        {
            return _quote;
        }
    }
}