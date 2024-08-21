using UnityEngine;
using System;



public class SimulatedInput : MonoBehaviour
{
    [SerializeField]
    private float simulatedSensorValue = 0f;
    [SerializeField]
    private float inspirationAmplitude = 200f;
    [SerializeField]
    private float expirationAmplitude  = 100f;
    [SerializeField]
    [Range(0.0f, 6.0f)]
    private float frequency = 6f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float randomFactor = 1f;
    public event Action<string> OnsimulatedSerialMessageReceived;


    void Update()
    {


        inspirationAmplitude = GetRandomAmplitude(inspirationAmplitude);
        expirationAmplitude = GetRandomAmplitude(expirationAmplitude);

        float sinValue = Mathf.Sin(Time.time * frequency);
        if (sinValue > 0)
        {
            simulatedSensorValue = expirationAmplitude * sinValue;
            OnsimulatedSerialMessageReceived?.Invoke(simulatedSensorValue.ToString());
        }
        else
        {
            simulatedSensorValue = inspirationAmplitude * sinValue;
            OnsimulatedSerialMessageReceived?.Invoke(simulatedSensorValue.ToString());
        }
    }

    private float GetRandomAmplitude(float baseAmplitude)
    {
        return baseAmplitude + UnityEngine.Random.Range(-randomFactor, randomFactor);
    }

    public float GetSimulatedSensorValue()
    {
        return simulatedSensorValue;
    }
}