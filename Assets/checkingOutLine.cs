using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkingOutLine : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    public float blinkInterval = 0.5f;
    private float blinkTimer = 0f;
    private Renderer sphereRenderer;
    private Color originalColor;

    private void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        originalColor = sphereRenderer.material.color;
    }

    private void Update()
    {
        blinkTimer += Time.deltaTime;

        if (blinkTimer >= blinkInterval)
        {
            blinkTimer = 0f;

            if (sphereRenderer.material.color == originalColor)
            {
                sphereRenderer.material.color = highlightColor;
            }
            else
            {
                sphereRenderer.material.color = originalColor;
            }
        }
    }
}
