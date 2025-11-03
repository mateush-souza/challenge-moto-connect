using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Linq;

namespace challenge_moto_connect.Application.Services
{
    // Classe de entrada para o modelo (dados de entrada)
    public class VehicleMaintenanceInput
    {
        [LoadColumn(0)]
        public float Mileage { get; set; } // Quilometragem atual
        [LoadColumn(1)]
        public float AgeInYears { get; set; } // Idade da moto em anos
        [LoadColumn(2)]
        public string VehicleModel { get; set; } // Modelo da moto (categórico)
        [LoadColumn(3)]
        public bool IsElectric { get; set; } // Se é elétrica ou não
    }

    // Classe de saída do modelo (predição)
    public class MaintenancePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool NeedsMaintenance { get; set; } // Predição: precisa de manutenção (True/False)
        public float Score { get; set; } // Pontuação de confiança
    }

    public class MLService
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;

        public MLService()
        {
            _mlContext = new MLContext();
            // Em um cenário real, o modelo seria carregado de um arquivo .zip
            // Ex: _trainedModel = _mlContext.Model.Load("model.zip", out var modelInputSchema);
            
            // Para fins de demonstração, vamos criar um modelo de classificação binária simples.
            TrainModel();
        }

        private void TrainModel()
        {
            // 1. Dados de Treinamento (Simulados)
            var data = new List<VehicleMaintenanceInput>
            {
                new VehicleMaintenanceInput { Mileage = 10000, AgeInYears = 1, VehicleModel = "Sport", IsElectric = false },
                new VehicleMaintenanceInput { Mileage = 50000, AgeInYears = 5, VehicleModel = "Cruiser", IsElectric = false },
                new VehicleMaintenanceInput { Mileage = 20000, AgeInYears = 2, VehicleModel = "Scooter", IsElectric = true },
                new VehicleMaintenanceInput { Mileage = 80000, AgeInYears = 8, VehicleModel = "Sport", IsElectric = false },
                new VehicleMaintenanceInput { Mileage = 15000, AgeInYears = 1, VehicleModel = "Cruiser", IsElectric = true },
                new VehicleMaintenanceInput { Mileage = 60000, AgeInYears = 6, VehicleModel = "Scooter", IsElectric = false },
            };

            // 2. Labels (Simulados) - True se precisar de manutenção
            var labels = new List<bool> { false, true, false, true, false, true };

            var dataView = _mlContext.Data.LoadFromEnumerable(data.Zip(labels, (input, label) => new { input.Mileage, input.AgeInYears, input.VehicleModel, input.IsElectric, Label = label }));

            // 3. Pipeline de Treinamento
            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("VehicleModelEncoded", "VehicleModel")
                .Append(_mlContext.Transforms.Concatenate("Features", "Mileage", "AgeInYears", "IsElectric", "VehicleModelEncoded"))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            // 4. Treinamento do Modelo
            _trainedModel = pipeline.Fit(dataView);
        }

        /// <summary>
        /// Realiza a predição de necessidade de manutenção para uma lista de veículos.
        /// </summary>
        /// <param name="input">Dados de entrada do veículo.</param>
        /// <returns>Resultado da predição.</returns>
        public MaintenancePrediction PredictMaintenance(VehicleMaintenanceInput input)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<VehicleMaintenanceInput, MaintenancePrediction>(_trainedModel);
            return predictionEngine.Predict(input);
        }
    }
}
