using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSmokeBehaviour : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float resizeSpeed = 1.5f;
    public float fadeSpeed = 2.5f;
    public float aliveTime = 3f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, aliveTime);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        transform.localScale *= 1 + (resizeSpeed * deltaTime);
        transform.position += Vector3.up * moveSpeed * deltaTime;

        Color color = spriteRenderer.color;
        color.a *= 1 - (fadeSpeed * deltaTime);
        spriteRenderer.color = color;
    }
}
