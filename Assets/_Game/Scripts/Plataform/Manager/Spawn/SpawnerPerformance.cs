using System;
using Ibit.Plataform.Data;
using Ibit.Plataform.Manager.Stage;
using Ibit.Core.Database;
using Ibit.Core.Game;
using NaughtyAttributes;
using UnityEngine;

namespace Ibit.Plataform.Manager.Spawn
{
    public partial class Spawner
    {
        #region Performance

        // Targets

        public int TargetsSucceeded => TargetsExpSucceeded + TargetsInsSucceeded;
        public int TargetsFailed => TargetsExpFailed + TargetsInsFailed;

        public int TargetsInsSucceeded { get; private set; }
        public int TargetsInsFailed { get; private set; }
        public int TargetsExpSucceeded { get; private set; }
        public int TargetsExpFailed { get; private set; }

        // Obstacles

        public int ObstaclesSucceeded => ObstaclesExpSucceeded + ObstaclesInsSucceeded;
        public int ObstaclesFailed => ObstaclesExpFailed + ObstaclesInsFailed;

        public int ObstaclesInsSucceeded { get; private set; }
        public int ObstaclesInsFailed { get; private set; }
        public int ObstaclesExpSucceeded { get; private set; }
        public int ObstaclesExpFailed { get; private set; }

        #endregion Performance

        [ShowNonSerializedField] private float expHeightAcc;
        [ShowNonSerializedField] private float expSizeAcc;
        [ShowNonSerializedField] private float insHeightAcc;
        [ShowNonSerializedField] private float insSizeAcc;

        private int airTargetsHit;
        private int airObstaclesHit;
        private int waterTargetsHit;
        private int waterObstaclesHit;
        private int airTargetsMiss;
        private int airObstaclesMiss;
        private int waterTargetsMiss;
        private int waterObstaclesMiss;

        public event Action<float, float> OnUpdatedPerformanceTarget;
        public event Action<float, float> OnUpdatedPerformanceObstacle;
        public event Action<string, int> OnTargetHitThreshold;
        public event Action<string, int> OnTargetMissThreshold;
        public event Action<string, int> OnObstacleMissThreshold;
        public event Action<string, int> OnObstacleHitThreshold;
        

        private void PerformanceOnPlayerHit(GameObject hit)
        {
            switch (hit.tag)
            {
                case "AirTarget":
                    TargetsInsSucceeded++;
                    airTargetsHit++;
                    if (airTargetsHit >= StageModel.Loaded.HeightUpThreshold)
                    {
                        OnTargetHitThreshold?.Invoke(hit.tag, airTargetsHit);
                        //IncrementInsHeight();
                        airTargetsHit = 0;
                    }
                    airTargetsMiss = 0;
                    break;

                case "WaterTarget":
                    TargetsExpSucceeded++;
                    waterTargetsHit++;
                    if (waterTargetsHit >= StageModel.Loaded.HeightUpThreshold)
                    {
                        OnTargetHitThreshold?.Invoke(hit.tag, waterTargetsHit);
                        //IncrementExpHeight();
                        waterTargetsHit = 0;
                    }
                    waterTargetsMiss = 0;
                    break;

                case "AirObstacle":
                    ObstaclesExpFailed++;
                    airObstaclesHit--;
                    if (airObstaclesHit <= -StageModel.Loaded.SizeDownThreshold)
                    {
                        OnObstacleHitThreshold?.Invoke(hit.tag, airObstaclesHit);
                        //DecrementExpSize();
                        airObstaclesHit = 0;
                    }
                    airObstaclesMiss = 0;
                    break;

                case "WaterObstacle":
                    ObstaclesInsFailed++;
                    waterObstaclesHit--;
                    if (waterObstaclesHit <= -StageModel.Loaded.SizeDownThreshold)
                    {
                        OnObstacleHitThreshold?.Invoke(hit.tag, waterObstaclesHit);
                        //DecrementInsSize();
                        waterObstaclesHit = 0;
                    }
                    waterObstaclesMiss = 0;
                    break;
            }
        }

        public void PerformanceOnPlayerMiss(string objectTag)
        {
            switch (objectTag)
            {
                case "AirTarget":
                    TargetsInsFailed++;
                    airTargetsMiss--;
                    if (airTargetsMiss <= -StageModel.Loaded.HeightDownThreshold)
                    {
                        OnTargetMissThreshold?.Invoke(objectTag, airTargetsMiss);
                        //DecrementInsHeight();
                        airTargetsMiss = 0;
                    }
                    airTargetsHit = 0;
                    break;

                case "WaterTarget":
                    TargetsExpFailed++;
                    waterTargetsMiss--;
                    if (waterTargetsMiss <= -StageModel.Loaded.HeightDownThreshold)
                    {
                        OnTargetMissThreshold?.Invoke(objectTag, waterTargetsMiss);
                        //DecrementExpHeight();
                        waterTargetsMiss = 0;
                    }
                    waterTargetsHit = 0;
                    break;

                case "AirObstacle":
                    ObstaclesExpSucceeded++;
                    airObstaclesMiss++;
                    if (airObstaclesMiss >= StageModel.Loaded.SizeUpThreshold)
                    {
                        OnObstacleMissThreshold?.Invoke(objectTag, airObstaclesMiss);
                        //IncrementExpSize();
                        airObstaclesMiss = 0;
                    }
                    airObstaclesHit = 0;
                    break;

                case "WaterObstacle":
                    ObstaclesInsSucceeded++;
                    waterObstaclesMiss++;
                    if (waterObstaclesMiss >= StageModel.Loaded.SizeUpThreshold)
                    {
                       OnObstacleMissThreshold?.Invoke(objectTag, waterObstaclesMiss);
                        //IncrementInsSize();
                        waterObstaclesMiss = 0;
                    } 
                    waterObstaclesHit = 0;
                    break;
            }
        }

        // On The Fly Inputs Class
        public void IncrementInsHeight()
        {
            if (StageModel.Loaded.HeightIncrement == 0)
                return;

            insHeightAcc += StageModel.Loaded.HeightIncrement;
            OnUpdatedPerformanceTarget?.Invoke(insHeightAcc, expHeightAcc);
            insHeightAcc = 0;
            expHeightAcc = 0;
        }

        public void DecrementInsHeight()
        {
            if (StageModel.Loaded.HeightIncrement == 0)
                return;
            
            insHeightAcc -= StageModel.Loaded.HeightIncrement;
            //insHeightAcc = insHeightAcc < 0f ? 0f : insHeightAcc;
            OnUpdatedPerformanceTarget?.Invoke(insHeightAcc, expHeightAcc);
            insHeightAcc = 0;
            expHeightAcc = 0;
        }

        public void IncrementExpHeight()
        {
            if (StageModel.Loaded.HeightIncrement == 0)
                return;

            expHeightAcc += StageModel.Loaded.HeightIncrement;
            OnUpdatedPerformanceTarget?.Invoke(insHeightAcc, expHeightAcc);
            insHeightAcc = 0;
            expHeightAcc = 0;
        }

        public void DecrementExpHeight()
        {
            if (StageModel.Loaded.HeightIncrement == 0)
                return;

            expHeightAcc -= StageModel.Loaded.HeightIncrement;
            //expHeightAcc = expHeightAcc < 0f ? 0f : expHeightAcc;
            OnUpdatedPerformanceTarget?.Invoke(insHeightAcc, expHeightAcc);
            insHeightAcc = 0;
            expHeightAcc = 0;
        }

        public void IncrementInsSize()
        {
            if (StageModel.Loaded.SizeIncrement == 0)
                return;

            insSizeAcc += StageModel.Loaded.SizeIncrement;
            OnUpdatedPerformanceObstacle?.Invoke(insSizeAcc, expSizeAcc);
            insSizeAcc = 0;
            expSizeAcc = 0;
        }

        public void DecrementInsSize()
        {
            if (StageModel.Loaded.SizeIncrement == 0)
                return;

            insSizeAcc -= StageModel.Loaded.SizeIncrement;
            //insSizeAcc = insSizeAcc < 0f ? 0f : insSizeAcc;
            OnUpdatedPerformanceObstacle?.Invoke(insSizeAcc, expSizeAcc);
            insSizeAcc = 0;
            expSizeAcc = 0;
        }

        public void IncrementExpSize()
        {
            if (StageModel.Loaded.SizeIncrement == 0)
                return;

            expSizeAcc += StageModel.Loaded.SizeIncrement;
            OnUpdatedPerformanceObstacle?.Invoke(insSizeAcc, expSizeAcc);
            insSizeAcc = 0;
            expSizeAcc = 0;
        }

        public void DecrementExpSize()
        {
            if (StageModel.Loaded.SizeIncrement == 0)
                return;

            expSizeAcc -= StageModel.Loaded.SizeIncrement;
            //expSizeAcc = expSizeAcc < 0f ? 0f : expSizeAcc;
            OnUpdatedPerformanceObstacle?.Invoke(insSizeAcc, expSizeAcc);
            insSizeAcc = 0;
            expSizeAcc = 0;
        }
    }
}