using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutBehaviour : MonoBehaviour
{
    public float fadeOutTime;
    private Timer timer;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        timer = new Timer(fadeOutTime);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
        Color tempColor = spriteRenderer.color;
        tempColor.a = timer.RemainingTime() / fadeOutTime;
        spriteRenderer.color = tempColor;
    }
}
