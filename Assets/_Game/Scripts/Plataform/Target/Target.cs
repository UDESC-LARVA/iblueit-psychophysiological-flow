using Ibit.Plataform.Camera;
using Ibit.Plataform.Data;
using Ibit.Plataform.Manager.Spawn;
using Ibit.Plataform.UI;
using UnityEngine;
using Ibit.Core.Database;

namespace Ibit.Plataform
{
    public partial class Target : MonoBehaviour
    {
        [SerializeField] private ObjectModel _model;

        public float Score { get; private set; }

        public void Build(ObjectModel model)
        {
            if (model == null)
            {
                Debug.LogError("The model is null.");
                return;
            }
            _model = model;
            CalculateHeight();
            CalculateScore();
            FindObjectOfType<Spawner>().OnUpdatedPerformanceTarget += OnUpdatedPerformance;   
            FindObjectOfType<DeepDDAManager>().OnUpdatedPerformanceTarget += OnUpdatedPerformance;    
            FindObjectOfType<DeepDDAManager>().OnUpdatedPerformanceLevel += OnUpdatedPerformance;        
        }

        public void OnUpdatedPerformance(float insAcc, float expAcc)
        {
            CalculateHeightDeepDDA(_model.PositionYFactor > 0 ? insAcc : expAcc);
        }

        private void CalculateScore()
        {
            if (_model == null)
            {
                Debug.LogError("_model is null");
                return;
            }
            if (StageModel.Loaded == null)
            {
                Debug.LogError("StageModel.Loaded is null");
                return;
            }

            if (ParametersDb.parameters == null)
            {
                Debug.LogError("ParametersDb.parameters is null");
                return;
            }

            Score = (Mathf.Abs(this.transform.position.y) * (1f + _model.DifficultyFactor) * (1 + StageModel.Loaded.ObjectSpeedFactor))*ParametersDb.parameters.ScoreCalculationFactor; // ScoreCalculationFactor = Fator de Cálculo da Pontuação
        }

        private void CalculateHeight(float performanceAccumulator = 0)
        {
            if (this == null)
            {
                Debug.LogWarning("Tentando acessar um objeto Target que foi destruído.");
                return;
            }

            var tmpPos = this.transform.position;

            tmpPos.y = (1f + performanceAccumulator) * CameraLimits.Boundary * _model.DifficultyFactor;
            //Debug.Log($"Alvos - Altura antes: {tmpPos.y}");

            tmpPos.y = _model.PositionYFactor > 0 ? // Mantem valores do Object Models
                Mathf.Clamp(tmpPos.y, 0f, CameraLimits.Boundary) :
                Mathf.Clamp(-tmpPos.y, -CameraLimits.Boundary, 0f);

            this.transform.position = tmpPos;
        }

        private void CalculateHeightDeepDDA(float performanceHeight)
        {
            if (this == null)
            {
                Debug.LogWarning("Tentando acessar um objeto Target que foi destruído.");
                return;
            }

            var tmpPos = this.transform.position;        
            
            // DeepDDA RF-02.01: se ocorrer (Fi) falhas consecutivas em coletar alvos durante uma sessão.
            if (StageModel.Loaded.HeightIncrement > 0)// && StageModel.Loaded.HeightUpThreshold > 0 && StageModel.Loaded.HeightDownThreshold > 0)
            {
                tmpPos.y = _model.PositionYFactor > 0 ?
                    Mathf.Clamp(tmpPos.y + performanceHeight, 0f, CameraLimits.Boundary) :
                    Mathf.Clamp(tmpPos.y - performanceHeight, -CameraLimits.Boundary, 0f);
                //Debug.Log($"Alvos - Altura depois: {tmpPos.y}");
            }
            

            // DeepDDA RF-02.01: se manter ou melhorar o Desempenho do Jogador (∆DJ) por Si sessões consecutivas.
            // VERIFICAR COMO INCREMENTAR APÓS LOAD DO PRÓXIMO STAGE
            // if (ResultScreenUI.numberFailures < ParametersDb.parameters.lostWtimes && StageModel.Loaded.HeightIncrement != 0)
            // {
            //     tmpPos.y = _model.PositionYFactor > 0 ?
            //         Mathf.Clamp(tmpPos.y*(StageModel.Loaded.HeightIncrement+1), 0f, CameraLimits.Boundary) :
            //         Mathf.Clamp(tmpPos.y*(StageModel.Loaded.HeightIncrement+1), -CameraLimits.Boundary, 0f);
            //     Debug.Log($"Alvos - Altura depois: {tmpPos.y}");
            // }

            // if (ResultScreenUI.numberFailures >= ParametersDb.parameters.lostWtimes)  // Se perder W Stages
            // {   
            //     // decreaseHeight =  Valor de decremento da ALTURA dos Alvos, vindo de _parametersList.csv
            //     tmpPos.y = _model.PositionYFactor > 0 ?
            //         Mathf.Clamp(tmpPos.y*ParametersDb.parameters.decreaseHeight, 0f, CameraLimits.Boundary) :
            //         Mathf.Clamp(tmpPos.y*ParametersDb.parameters.decreaseHeight, -CameraLimits.Boundary, 0f);
            //     Debug.Log($"Alvos - Altura depois: {tmpPos.y}");
            // }


            this.transform.position = tmpPos;
        }

        private void OnDestroy()
        {
            var spwn = FindObjectOfType<Spawner>();
            if (spwn != null)
                spwn.OnUpdatedPerformanceTarget -= OnUpdatedPerformance;
        }
    }
}