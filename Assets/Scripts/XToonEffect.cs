﻿using UnityEngine;

public class XToonEffect : MonoBehaviour
{
    public Shader shader;

    private Material material;

    private void Start()
    {
        material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
