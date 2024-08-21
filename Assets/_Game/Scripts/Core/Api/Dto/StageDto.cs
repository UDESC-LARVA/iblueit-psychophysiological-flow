using System;
using Newtonsoft.Json;

namespace Assets._Game.Scripts.Core.Api.Dto
{
    public class StageDto        // DeepDDA
    {
        [JsonProperty("_id")] public string Id { get; set; }
        [JsonProperty("pacientId")] public string PacientId { get; set; }
        [JsonProperty("stageId")] public int StageId { get; set; }
        [JsonProperty("phase")] public int Phase { get; set; }
        [JsonProperty("level")] public int Level { get; set; }
        [JsonProperty("ObjectSpeedFactor")] public float ObjectSpeedFactor { get; set; }
        [JsonProperty("Loops")] public int Loops { get; set; }
        [JsonProperty("HeightIncrement")] public float HeightIncrement { get; set; }
        [JsonProperty("HeightUpThreshold")] public int HeightUpThreshold { get; set; }
        [JsonProperty("HeightDownThreshold")] public int HeightDownThreshold { get; set; }
        [JsonProperty("SizeIncrement")] public float SizeIncrement { get; set; }
        [JsonProperty("SizeUpThreshold")] public int SizeUpThreshold { get; set; }
        [JsonProperty("SizeDownThreshold")] public int SizeDownThreshold { get; set; }
    }
}