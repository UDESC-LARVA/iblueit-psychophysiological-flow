using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class GameAgent : Agent
{
    public static GameAgent instance;

    public float rewardModifier { get; private set; } = 1f;
    public float obstacleRateModifier { get; private set; } = 1f;
    public float obstacleSizeModifier { get; private set; } = 1f;
    public float targetRateModifier { get; private set; } = 1f;
    public float targetSizeModifier { get; private set; } = 1f;
    public float gameSpeed { get; private set; } = 1f;

    private void Awake()
    {
        instance = this;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);

        // aqui recebe os dados e usa a informação...
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);

        // aqui add os dados para passar pro ML
        /*
        - tempo de jogo total
        - tempo de jogo desde ultima colisão
        - pontuação
        - vidas
        - Leituras dos aparelhos
        - medidas paciente
        */

        //sensor.AddObservation();
    }

    public override void Heuristic(float[] actionsOut)
    {
        base.Heuristic(actionsOut);

        // input for testing if needed
    }

    public void RewardPlayer(float reward, bool useMultiplier)
    {
        if (useMultiplier)
            reward *= rewardModifier;

        AddReward(reward);
    }
}
