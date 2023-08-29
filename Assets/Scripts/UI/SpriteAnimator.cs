using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    public float interval = .2f;

    private float currentInterval = 0f;
    private int index = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentInterval += Time.deltaTime;
        if (currentInterval >= interval)
        {
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        currentInterval = 0;

        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }

        spriteRenderer.sprite = sprites[index];
    }
}
