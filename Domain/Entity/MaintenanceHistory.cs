namespace challenge_moto_connect.Domain.Entity
{
    /// <summary>
    /// Representa o histórico de manutenção de um veículo.
    /// </summary>
    public class MaintenanceHistory
    {
        /// <summary>
        /// Identificador único do histórico de manutenção.
        /// </summary>
        public Guid MaintenanceHistoryID { get; set; }

        /// <summary>
        /// Identificador do veículo.
        /// </summary>
        public Guid VehicleID
        {
            get => _vehicleid;
            set
            {
                // Tornar a validação mais tolerante para quando o EF estiver carregando os dados
                // Apenas registrar o valor para evitar exceções durante o carregamento
                _vehicleid = value;
            }
        }

        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        public Guid UserID
        {
            get => _userid;
            set
            {
                // Tornar a validação mais tolerante para quando o EF estiver carregando os dados
                // Apenas registrar o valor para evitar exceções durante o carregamento
                _userid = value;
            }
        }

        /// <summary>
        /// Data da manutenção.
        /// </summary>
        public DateTime MaintenanceDate
        {
            get => _maintenanceDate;
            set
            {
                // Validação mais segura para evitar erros durante o carregamento do EF
                if (value == default || value == DateTime.MinValue)
                    _maintenanceDate = DateTime.Now; // Define um valor padrão
                else
                    _maintenanceDate = value;
            }
        }

        /// <summary>
        /// Descrição da manutenção.
        /// </summary>
        public string Description
        {
            get => _description;
            set => _description = value ?? ""; // Aceita valores nulos, convertendo para string vazia
        }

        private Guid _vehicleid;
        private Guid _userid;
        private string _description = "";
        private DateTime _maintenanceDate = DateTime.Now;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public MaintenanceHistory() { }

        /// <summary>
        /// Construtor com parâmetros.
        /// </summary>
        public MaintenanceHistory(Guid maintenanceHistoryID, Guid userID, Guid vehicleID, DateTime maintenanceDate, string description)
        {
            // Aplicar validação rigorosa apenas para inserções/atualizações explícitas
            if (userID == Guid.Empty)
                throw new ArgumentNullException(nameof(userID), "User não pode ser nulo.");
            if (vehicleID == Guid.Empty)
                throw new ArgumentNullException(nameof(vehicleID), "Vehicle não pode ser nulo.");

            MaintenanceHistoryID = maintenanceHistoryID;
            _userid = userID;  // Atribuição direta para evitar validação
            _vehicleid = vehicleID;  // Atribuição direta para evitar validação

            if (maintenanceDate == default || maintenanceDate == DateTime.MinValue)
                _maintenanceDate = DateTime.Now;
            else
                _maintenanceDate = maintenanceDate;

            _description = description ?? "";
        }

        /// <summary>
        /// Valida a entidade para inserção ou atualização, lançando exceções se inválida.
        /// </summary>
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

        // Valida se a data não é o valor padrão (DateTime.MinValue)
        private bool IsValidDate(DateTime date)
        {
            return date != default && date != DateTime.MinValue;
        }
    }
}