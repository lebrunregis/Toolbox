using DebugBehaviour.Runtime;
using UnityEngine;

public class Paintable : VerboseMonoBehaviour
{
    public int textureSizePowOfTwo = 10;
    public FilterMode filterMode = FilterMode.Bilinear;
    public Texture2D baseMask;

    public float extendsIslandOffset = 1;

    private RenderTexture extendIslands;
    private RenderTexture uvIslands;
    private RenderTexture mask;
    private RenderTexture support;

    private Renderer rend;

    private readonly int mainTextureID = Shader.PropertyToID("_BaseMap");
    private readonly int maskTextureID = Shader.PropertyToID("_PaintMask");

    public RenderTexture GetMask() => mask;
    public RenderTexture GetUVIslands() => uvIslands;
    public RenderTexture GetExtend() => extendIslands;
    public RenderTexture GetSupport() => support;
    public Renderer GetRenderer() => rend;

    private void Start()
    {
        int textureSize = Toolbox.Math.Exponent.TwoPowX(textureSizePowOfTwo);
        if (baseMask != null)
        {
            Log("Loading custom mask texture");
            Log($"baseMask size: {baseMask.width}x{baseMask.height}, format: {baseMask.format}");

            mask = new RenderTexture(baseMask.width, baseMask.height, 0)
            {
                filterMode = filterMode
            };
            mask.Create();
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = mask;
            Graphics.Blit(baseMask, mask);
            RenderTexture.active = previous;
        }
        else
        {
            mask = new RenderTexture(textureSize, textureSize, 0)
            {
                filterMode = filterMode
            };
        }

        extendIslands = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        uvIslands = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        support = new RenderTexture(textureSize, textureSize, 0)
        {
            filterMode = filterMode
        };

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, mask);

        PaintManager.Instance.InitTextures(this);
    }

    private void OnDisable()
    {
        mask.Release();
        uvIslands.Release();
        extendIslands.Release();
        support.Release();
    }
}