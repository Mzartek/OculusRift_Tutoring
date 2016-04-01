using UnityEngine;

public class ToonEffect : MonoBehaviour
{
    public Shader shader;

    public int grayScale;

    private Material material;

    private Texture2D texture;

    private void Start()
    {
        material = new Material(shader);
        texture = new Texture2D(grayScale, 1);

        for (int i = 0; i < grayScale; i++)
        {
            //texture.SetPixel(i, 1, );
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
