using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Paint.Runtime
{


    public class PaintManager : Singleton<PaintManager>
    {

        public Shader texturePaint;

        private readonly int test = Shader.PropertyToID("_Test");

        private Material paintMaterial;

        private CommandBuffer command;

        public override void Awake()
        {
            base.Awake();
            if (texturePaint != null)
            {
                paintMaterial = new Material(texturePaint);
                command = new CommandBuffer
                {
                    name = "CommmandBuffer - " + gameObject.name
                };
            }
            else
            {
                Log("Shader not set!", this);
            }

        }

        public void InitTextures(Paintable paintable)
        {
            if (paintable is null)
            {
                throw new ArgumentNullException(nameof(paintable));
            }

            RenderTexture mask = paintable.GetMask();
            Renderer rend = paintable.GetRenderer();

            command.SetRenderTarget(mask);
            command.DrawRenderer(rend, paintMaterial, 0);

            Graphics.ExecuteCommandBuffer(command);
            command.Clear();
        }


        public void Paint(Paintable paintable, Color color, Vector3 pos, float radius = 1f, float hardness = .5f, float strength = .5f, float extendsIslandOffset = 1.0f)
        {

        }

    }
}
