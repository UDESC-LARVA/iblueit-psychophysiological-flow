using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ibit.Plataform.Manager.Stage;

public class MLObjectSpawner : MonoBehaviour
{
    public float minObstacleInterval;
    public float maxObstacleInterval;

    public float minTargetInterval;
    public float maxTargetInterval;

    public float minRelaxInterval;
    public float maxRelaxInterval;

    [Range(0,1)]
    public float relaxChanceAfterObstacle = .5f;

    private GameAgent mlGameAgent;

    private float timeToNextObstacle;
    private float timeToNextTarget;
    private float relaxRemainingTime;

    // Start is called before the first frame update
    void Start()
    {
        mlGameAgent = GameAgent.instance;

        var stgMgr = FindObjectOfType<StageManager>();
        stgMgr.OnStageStart += Spawn;
    }

    
    private void Spawn()
    {
        /*for (int i = 0; i < StageModel.Loaded.Loops; i++)
        {
            foreach (var stageObject in StageModel.Loaded.ObjectModels)
            {
                switch (stageObject.Type)
                {
                    case StageObjectType.Target:
                        SpawnTarget(stageObject);
                        break;
                    case StageObjectType.Obstacle:
                        SpawnObstacle(stageObject);
                        break;
                    case StageObjectType.Relax:
                        SpawnRelax(stageObject.PositionXSpacing);
                        break;
                }
            }
        }*/
    }
}
