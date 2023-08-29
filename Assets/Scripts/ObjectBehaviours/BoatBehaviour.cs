using UnityEngine;

public class BoatBehaviour : MonoBehaviour
{
    public GameObject smokePrefab;
    public float interval;

    private float currentInterval = 0;
  
    private void Update()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval >= interval)
        {
            SpawnSmoke();
        }
    }

    private void SpawnSmoke()
    {
        currentInterval = 0;
        GameObject smoke = Instantiate(smokePrefab);
        
        smoke.transform.position = transform.position;
    }
}
