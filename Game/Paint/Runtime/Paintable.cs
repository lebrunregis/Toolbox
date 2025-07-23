using UnityEngine;

public class Paintable : MonoBehaviour
{
    private const int TEXTURE_SIZE = 1024;

    public float extendsIslandOffset = 1;

    private RenderTexture extendIslandsRenderTexture;
    private RenderTexture uvIslandsRenderTexture;
    private RenderTexture maskRenderTexture;
    private RenderTexture supportTexture;

    private Renderer rend;

    private readonly int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture GetMask() => maskRenderTexture;
    public RenderTexture GetUVIslands() => uvIslandsRenderTexture;
    public RenderTexture GetExtend() => extendIslandsRenderTexture;
    public RenderTexture GetSupport() => supportTexture;
    public Renderer GetRenderer() => rend;

    private void Start()
    {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.Instance.initTextures(this);
    }

    private void OnDisable()
    {
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }
}