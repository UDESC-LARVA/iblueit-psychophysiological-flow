// DeepDDA: script modificado para eliminar necessidade de .csv
using System;
using System.Collections.Generic;
using UnityEngine;
using Ibit.Core.Database;
using Assets._Game.Scripts.Core.Api.Dto;

namespace Ibit.Plataform.Data
{
    [CreateAssetMenu(fileName = "NewStage", menuName = "ScriptableObjects/Stage Model", order = 1)]
    [Serializable]
    public class StageModel : ScriptableObject
    {
        [SerializeField]
        public static StageModel Loaded;

        [Header("Info")]

        public int Id; // Stage Identification (min: 1)
        public string IdApi;
        public string PacientIdApi;
        public int Phase; // Phase Identification (min: 1, max: 4)
        public int Level; // Level Identification (min: 1)
        public float ObjectSpeedFactor; // Controls the objects speed (min: 1.0. max: 3.0)
        public int Loops; // Number of times to repeat a script (min: 1, max: 99)
        
        [Space(5)]
        [Header("Target Settings")]

        public float HeightIncrement; // Incremental value that controls player performance during a level (min: 0.0 max: 1.0)
        public int HeightUpThreshold; // Number of successes required to increase performance height by 'Increment' during a level (min: 0.0 max: 10.0)
        public int HeightDownThreshold; // Number of failures required to decrease performance height by 'Increment' during a level (min: 0.0 max: 3.0)

        [Space(5)]
        [Header("Obstacle Settings")]

        public float SizeIncrement; // Incremental value that controls player performance during a level (min: 0.0 max: 1.0)
        public int SizeUpThreshold; // Number of successes required to increase performance sizes by 'Increment' during a level (min: 0.0 max: 10.0)
        public int SizeDownThreshold; // Number of failures required to decrease performance sizes by 'Increment' during a level (min: 0.0 max: 3.0)

        [Space(5)]
        [Header("Stage Object List")]

        public List<ObjectModel> ObjectModels;

        public StageModel()
        {
            ObjectModels = new List<ObjectModel>();
        }
    }
}