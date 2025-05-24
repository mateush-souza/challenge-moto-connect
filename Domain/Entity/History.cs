namespace challenge_moto_connect.Domain.Entity;
    public class History
    {
        public Guid MaintenanceHistoryID { get; set; }
        public Guid VehicleID
        {
            get => _vehicleid;
            set
            {
                _vehicleid = value;
            }
        }

        public Guid UserID
        {
            get => _userid;
            set
            {
                _userid = value;
            }
        }

    public StatusModel StatusModel { get; set; } = StatusModel.NAO_INICIADO;

    public DateTime MaintenanceDate
        {
            get => _maintenanceDate;
            set
            {
                if (value == default || value == DateTime.MinValue)
                    _maintenanceDate = DateTime.Now;
                else
                    _maintenanceDate = value;
            }
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? ""; 
        }

        private Guid _vehicleid;
        private Guid _userid;
        private string _description = "";
        public StatusModel _statusModel = StatusModel.NAO_INICIADO;
        private DateTime _maintenanceDate = DateTime.Now;

        public History() { }
        public History(Guid maintenanceHistoryID, Guid userID, Guid vehicleID, DateTime maintenanceDate, StatusModel statusModel, string description)
        {
            if (userID == Guid.Empty)
                throw new ArgumentNullException(nameof(userID), "User não pode ser nulo.");
            if (vehicleID == Guid.Empty)
                throw new ArgumentNullException(nameof(vehicleID), "Vehicle não pode ser nulo.");

            MaintenanceHistoryID = maintenanceHistoryID;
            _statusModel = statusModel;
            _userid = userID; 
            _vehicleid = vehicleID; 

            if (maintenanceDate == default || maintenanceDate == DateTime.MinValue)
                _maintenanceDate = DateTime.Now;
            else
                _maintenanceDate = maintenanceDate;

            _description = description ?? "";

        }

        public void ValidateForSave()
        {
            if (_userid == Guid.Empty)
                throw new ArgumentNullException(nameof(UserID), "User não pode ser nulo.");
            if (_vehicleid == Guid.Empty)
                throw new ArgumentNullException(nameof(VehicleID), "Vehicle não pode ser nulo.");
            if (string.IsNullOrEmpty(_description))
                throw new ArgumentNullException(nameof(Description), "Description não pode ser nulo.");
            if (_maintenanceDate == default || _maintenanceDate == DateTime.MinValue)
                throw new ArgumentException("A data da manutenção está em formato inválido.");
        }

        private bool IsValidDate(DateTime date)
        {
            return date != default && date != DateTime.MinValue;
        }
    }