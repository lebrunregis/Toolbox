using UnityEngine;
using UnityEngine.Rendering;

public class PaintManager : Singleton<PaintManager>{

    public Shader texturePaint;
    public Shader extendIslands;

    readonly int prepareUVID = Shader.PropertyToID("_PrepareUV");
    readonly int positionID = Shader.PropertyToID("_PainterPosition");
    readonly int hardnessID = Shader.PropertyToID("_Hardness");
    readonly int strengthID = Shader.PropertyToID("_Strength");
    readonly int radiusID = Shader.PropertyToID("_Radius");
    readonly int blendOpID = Shader.PropertyToID("_BlendOp");
    readonly int colorID = Shader.PropertyToID("_PainterColor");
    readonly int textureID = Shader.PropertyToID("_MainTex");
    readonly int uvOffsetID = Shader.PropertyToID("_OffsetUV");
    readonly int uvIslandsID = Shader.PropertyToID("_UVIslands");

    Material paintMaterial;
    Material extendMaterial;

    CommandBuffer command;

    public override void Awake(){
        base.Awake();
        
        paintMaterial = new Material(texturePaint);
        extendMaterial = new Material(extendIslands);
        command = new CommandBuffer
        {
            name = "CommmandBuffer - " + gameObject.name
        };
    }

    public void initTextures(Paintable paintable){
        RenderTexture mask = paintable.GetMask();
        RenderTexture uvIslands = paintable.GetUVIslands();
        RenderTexture extend = paintable.GetExtend();
        RenderTexture support = paintable.GetSupport();
        Renderer rend = paintable.GetRenderer();

        command.SetRenderTarget(mask);
        command.SetRenderTarget(extend);
        command.SetRenderTarget(support);

        paintMaterial.SetFloat(prepareUVID, 1);
        command.SetRenderTarget(uvIslands);
        command.DrawRenderer(rend, paintMaterial, 0);

        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }


    public void Paint(Paintable paintable, Vector3 pos, float radius = 1f, float hardness = .5f, float strength = .5f, Color? color = null){
        RenderTexture mask = paintable.GetMask();
        RenderTexture uvIslands = paintable.GetUVIslands();
        RenderTexture extend = paintable.GetExtend();
        RenderTexture support = paintable.GetSupport();
        Renderer rend = paintable.GetRenderer();

        paintMaterial.SetFloat(prepareUVID, 0);
        paintMaterial.SetVector(positionID, pos);
        paintMaterial.SetFloat(hardnessID, hardness);
        paintMaterial.SetFloat(strengthID, strength);
        paintMaterial.SetFloat(radiusID, radius);
        paintMaterial.SetTexture(textureID, support);
        paintMaterial.SetColor(colorID, color ?? Color.red);
        extendMaterial.SetFloat(uvOffsetID, paintable.extendsIslandOffset);
        extendMaterial.SetTexture(uvIslandsID, uvIslands);

        command.SetRenderTarget(mask);
        command.DrawRenderer(rend, paintMaterial, 0);

        command.SetRenderTarget(support);
        command.Blit(mask, support);

        command.SetRenderTarget(extend);
        command.Blit(mask, extend, extendMaterial);

        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }

}
