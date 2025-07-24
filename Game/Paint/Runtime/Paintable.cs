using UnityEngine;

public class Paintable : MonoBehaviour
{
    public int tex_powOfTwo = 10;
    public FilterMode filterMode = FilterMode.Bilinear;

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
        int textureSize = Toolbox.Math.Exponent.TwoPowX(tex_powOfTwo);
        maskRenderTexture = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        extendIslandsRenderTexture = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        uvIslandsRenderTexture = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        supportTexture = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.Instance.InitTextures(this);
    }

    private void OnDisable()
    {
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }
}