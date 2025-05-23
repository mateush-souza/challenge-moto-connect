using System.Text.RegularExpressions;
using challenge_moto_connect.Domain.Interfaces;

namespace challenge_moto_connect.Domain.Entity
{
    public class Vehicle : ICancel
    {
        #region ICancel Properties
        public Guid UserCancelID { get; set; }
        public bool IsCancel { get; set; }
        #endregion

        public Guid VehicleId { get; set; }

        private string _licensePlate;

        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                if (!IsValidLicensePlate(value))
                    throw new ArgumentException("O formato da placa está inválido. Tente novamente com o formato: AAA1234 ou AAA1A23");
                _licensePlate = value.Replace(" ", "").ToUpper();
            }
        }

        public VehicleModel VehicleModel { get; set; }

        public Vehicle() { }

        public Vehicle(Guid id, string licensePlate, VehicleModel vehicleModel)
        {
            VehicleId = id;
            LicensePlate = licensePlate; 
            VehicleModel = vehicleModel;
            IsCancel = false;
            UserCancelID = Guid.Empty;
        }

        private bool IsValidLicensePlate(string licensePlate)
        {
            licensePlate = licensePlate.Replace(" ", "").ToUpper();
            return Regex.IsMatch(licensePlate, @"^[A-Z]{3}\d{4}$") ||
                   Regex.IsMatch(licensePlate, @"^[A-Z]{3}\d[A-Z]\d{2}$");
        }
    }
}
