using System.Collections.Generic;
using System.Collections;
using Assets._Game.Scripts.Core.Api.Dto;
using Ibit.Core.Data;
using Ibit.Core.Data.Manager;
using Ibit.Core.Game;
using Ibit.Core.Util;
using Ibit.Core.Serial;
using Ibit.Plataform.Data;
using Ibit.Plataform.Manager.Score;
using Ibit.Plataform.Manager.Spawn;
using Ibit.Plataform.Manager.Stage;
using UnityEngine;
using UnityEngine.UI;
using Ibit.Plataform.UI;

namespace Ibit.Plataform.Logger
{
    public class PlataformLogger : Logger<PlataformLogger>
    {
        private SerialControllerPitaco scp;
        private SerialControllerMano scm;
        private SerialControllerCinta scc;
        private SerialControllerOximetro sco;

        private Player plr;
        private Scorer scr;
        private Spawner spwn;
        private PitacoLogger _pitacoLogger;
        private ManoLogger _manoLogger;
        private CintaLogger _cintaLogger;
        private OximetroLogger _oximetroLogger;
        [SerializeField] private ToggleGroup borgScaleToggleGroup;
        public static int selectedBorgScale = 4;

        protected override void Awake()
        {
            scp = FindObjectOfType<SerialControllerPitaco>();
            scm = FindObjectOfType<SerialControllerMano>();
            scc = FindObjectOfType<SerialControllerCinta>();
            sco = FindObjectOfType<SerialControllerOximetro>();


            sb.AppendLine("time;tag;instanceId;posX;posY");
            plr = FindObjectOfType<Player>();
            spwn = FindObjectOfType<Spawner>();
            scr = FindObjectOfType<Scorer>();
            _pitacoLogger = FindObjectOfType<PitacoLogger>();
            _manoLogger = FindObjectOfType<ManoLogger>();
            _cintaLogger = FindObjectOfType<CintaLogger>();
            _oximetroLogger = FindObjectOfType<OximetroLogger>();
            FindObjectOfType<StageManager>().OnStageEnd += StopLogging;
        }
        
        //DeepDDA: Métodos BorgScale referente ao FR-02.04 "Ajuste de Carga POR MEIO DA ESCALA DE BORG"
        protected override void Save()
        {
            StartCoroutine(SaveRoutine());
        }
        private IEnumerator SaveRoutine()
        {
            var canvas = GameObject.Find("Canvas");
            if (canvas == null)
            {
                Debug.LogError("Canvas não encontrado.");
                yield break;
            }
            var borgScalePanel = canvas.transform.Find("BorgScalePanel")?.gameObject;
            if (borgScalePanel == null)
            {
                Debug.LogError("BorgScalePanel não encontrado.");
                yield break;
            }
            borgScalePanel.SetActive(true);
            SetupBorgScaleListener();
            yield return new WaitUntil(() => !borgScalePanel.activeSelf);
            // Continua com a execução do código após confirmar BorgScale
            var path = @"savedata/pacients/" + Pacient.Loaded.Id + @"/" + $"{recordStart:yyyyMMdd-HHmmss}_" + FileName + ".csv";
            FileManager.WriteAllText(path, sb.ToString());
            LogPlaySession();
        }
    
        private void SetupBorgScaleListener()
        {
            if (borgScaleToggleGroup == null)
            {
                Debug.LogError("borgScaleToggleGroup não está atribuído.");
                return;
            }
            foreach (var toggle in borgScaleToggleGroup.GetComponentsInChildren<Toggle>())
            {
                toggle.onValueChanged.AddListener((isOn) => {
                    OnBorgScaleChanged(toggle, isOn);
                });
            }
        }

        private void OnBorgScaleChanged(Toggle changedToggle, bool isOn)
        {
            var textComponent = changedToggle.GetComponentInChildren<Text>();
            if (textComponent == null)
            {
                Debug.LogError("Componente Text não encontrado no Toggle.");
                return;
            }
            string borg = textComponent.text;
            Debug.Log($"Escala de Borg selecionada: {borg}");
            selectedBorgScale = int.Parse(borg);
        }

        private async void LogPlaySession()
        {
            
            //TODO: A responsabilidade do logger não é de bloquear e mostrar a tela de salvamento, porém o refatoramento a ser feito é muito grande... Deixa para depois
            GameObject.Find("Canvas").transform.Find("SavingBgPanel").gameObject.SetActive(true);

            if (StageModel.Loaded.Id == Pacient.Loaded.UnlockedLevels)
            {
                if (scr.Result == GameResult.Success)
                {

                    Pacient.Loaded.UnlockedLevels++;
                }
                else
                {
                    if (scr.Score < scr.MaxScore * 0.3f)
                        Pacient.Loaded.UnlockedLevels--;

                    if (Pacient.Loaded.UnlockedLevels <= 0)
                        Pacient.Loaded.UnlockedLevels = 1;
                }
            }

            Pacient.Loaded.PlaySessionsDone++;
            Pacient.Loaded.AccumulatedScore += scr.Score;
            //DeepDDA: Performance atual e Escala de Borg (parâmetros do jogo mongoDB)
            Pacient.Loaded.CurrentPerformance = DeepDDAAgent.currentPerformance;
            Pacient.Loaded.CurrentBorgScale = selectedBorgScale;

            var pacientSendDto = Pacient.MapToPacientSendDto();

            var responsePacient = await DataManager.Instance.UpdatePacient(pacientSendDto);
            if (responsePacient.ApiResponse == null)
                SysMessage.Info("Erro ao atualizar o paciente na nuvem!\n Os dados poderão ser enviados posteriormente.");
            Debug.Log($"Deu update no Paciente");

            var stageSendDto = new StageSendDto
            {
                PacientId = StageModel.Loaded.PacientIdApi,
                StageId = StageModel.Loaded.Id,
                Phase = StageModel.Loaded.Phase,
                Level = StageModel.Loaded.Level,
                ObjectSpeedFactor = StageModel.Loaded.ObjectSpeedFactor,
                Loops = StageModel.Loaded.Loops,
                HeightIncrement = StageModel.Loaded.HeightIncrement,
                HeightUpThreshold = StageModel.Loaded.HeightUpThreshold,
                HeightDownThreshold = StageModel.Loaded.HeightDownThreshold,
                SizeIncrement = StageModel.Loaded.SizeIncrement,
                SizeUpThreshold = StageModel.Loaded.SizeUpThreshold,
                SizeDownThreshold = StageModel.Loaded.SizeDownThreshold
            };

            var stageResponse = await DataManager.Instance.UpdateGameparameter(stageSendDto);
            if (stageResponse.ApiResponse == null)
                SysMessage.Info("Erro ao atualizar os parâmetros do jogo na nuvem!\n Os dados poderão ser enviados posteriormente.");
            Debug.Log($"Deu update nos Parâmetros do jogo");

            var plataformOverviewSendDto = new PlataformOverviewSendDto
            {
                Duration = FindObjectOfType<StageManager>().Duration,
                Result = scr.Result,
                StageId = StageModel.Loaded.Id,
                Phase = StageModel.Loaded.Phase,
                Level = StageModel.Loaded.Level,
                RelaxTimeSpawned = spwn.RelaxTimeSpawned,
                Score = scr.Score,
                MaxScore = scr.MaxScore,
                ScoreRatio = scr.Score / scr.MaxScore,
                TargetsSpawned = spwn.TargetsSucceeded + spwn.TargetsFailed,
                TargetsSuccess = spwn.TargetsSucceeded,
                TargetsInsSuccess = spwn.TargetsInsSucceeded,
                TargetsExpSuccess = spwn.TargetsExpSucceeded,
                TargetsFails = spwn.TargetsFailed,
                TargetsInsFail = spwn.TargetsInsFailed,
                TargetsExpFail = spwn.TargetsExpFailed,
                ObstaclesSpawned = spwn.ObstaclesSucceeded + spwn.ObstaclesFailed,
                ObstaclesSuccess = spwn.ObstaclesSucceeded,
                ObstaclesInsSuccess = spwn.ObstaclesInsSucceeded,
                ObstaclesExpSuccess = spwn.ObstaclesExpSucceeded,
                ObstaclesFail = spwn.ObstaclesFailed,
                ObstaclesInsFail = spwn.ObstaclesInsFailed,
                ObstaclesExpFail = spwn.ObstaclesExpFailed,
                PlayerHp = plr.HeartPoints,
                PacientId = Pacient.Loaded.IdApi,
                PlayStart = recordStart,
                PlayFinish = recordStop,
                FlowDataDevices = new List<FlowDataDevice>(),
                BorgScale = selectedBorgScale
            };


            if (scp.IsConnected) // Se PITACO conectado
            {
                Debug.Log("PlatformLogger - Device: Pitaco.");
                plataformOverviewSendDto.FlowDataDevices.Add(_pitacoLogger.flowDataDevice);
            } else {
            if (scm.IsConnected) // Se MANO conectado
            {
                Debug.Log("PlatformLogger - Device: Mano.");
                plataformOverviewSendDto.FlowDataDevices.Add(_manoLogger.flowDataDevice);
            } else {
            if (scc.IsConnected) // Se CINTA conectada
            {
                Debug.Log("PlatformLogger - Device: Cinta.");
                plataformOverviewSendDto.FlowDataDevices.Add(_cintaLogger.flowDataDevice);
            }}}

            if (sco.IsConnected) // Se OXÍMETRO conectado
            {
                Debug.Log("PlatformLogger - Device: Oxímetro.");
                plataformOverviewSendDto.FlowDataDevices.Add(_oximetroLogger.flowDataDevice);
            }


            var plataformResponse = await DataManager.Instance.SavePlataformOverview(plataformOverviewSendDto);
            if (plataformResponse.ApiResponse == null)
                SysMessage.Info("Erro ao salvar dados da plataforma na nuvem!\n Os dados poderão ser enviados posteriormente.");

            GameObject.Find("Canvas").transform.Find("SavingBgPanel").gameObject.SetActive(false);

            GameObject.Find("Canvas").transform.Find("ResultPanel").gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!isLogging || GameManager.GameIsPaused)
                return;

            sb.AppendLine($"{Time.time};{plr.tag};{plr.GetInstanceID()};{plr.transform.position.x:F};{plr.transform.position.y:F}");

            foreach (var o in spwn.SpawnedObjects)
            {
                if (o != null)
                    sb.AppendLine($"{Time.time};{o.tag};{o.GetInstanceID()};{o.position.x:F};{o.position.y:F}");
            }
        }
    }
}