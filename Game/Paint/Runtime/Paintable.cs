using DebugBehaviour.Runtime;
using System;
using UnityEngine;
namespace Paint.Runtime
{
    public class Paintable : VerboseMonoBehaviour
    {

        [Range(5, 12)]
        public int textureSizeExponent = 10;
        [Header("0, 16, 24, 32 are the only supported values.")]
        [Range(0, 32)]
        public int textureDepth = 0;
        [Range(0, 4)]
        public int msaaSamplesExponent = 0;
        public RenderTextureFormat format = RenderTextureFormat.Default;
        public RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
        public FilterMode filterMode = FilterMode.Bilinear;

        private RenderTextureDescriptor descriptor;

        private RenderTexture maskRenderTexture;

        private Renderer rend;

        private readonly int maskTextureID = Shader.PropertyToID("_MaskTexture");

        public RenderTexture GetMask() => maskRenderTexture;
        public Renderer GetRenderer() => rend;


        private void Start()
        {

            int textureSize = Toolbox.Math.Exponent.TwoPowX(textureSizeExponent);
            int msaaSamples = Toolbox.Math.Exponent.TwoPowX(msaaSamplesExponent);
            descriptor = new(textureSize, textureSize, format, textureDepth)
            {
                msaaSamples = msaaSamples
            };

            maskRenderTexture = new RenderTexture(descriptor)
            {
                filterMode = filterMode
            };


            rend = GetComponent<Renderer>();
            rend.material.SetTexture(maskTextureID, maskRenderTexture);

            PaintManager.Instance.InitTextures(this);
        }

        private void OnDisable()
        {
            maskRenderTexture.Release();
        }
    }
}