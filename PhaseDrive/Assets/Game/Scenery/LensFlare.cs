using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

[Serializable]
[PostProcess(typeof(LensFlareRenderer), PostProcessEvent.BeforeStack, "Custom/LensFlare")]
public sealed class LensFlare : PostProcessEffectSettings
{
}

public sealed class LensFlareRenderer : PostProcessEffectRenderer<LensFlare>
{
    public override void Render(PostProcessRenderContext context)
    {
        if (_camera == null)
            _camera = Object.FindObjectOfType<SceneryCamera>();

        var C = _camera.camera_transform;
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/LensFlare"));
        sheet.properties.SetMatrix("_CameraTransform", C);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }

    private SceneryCamera _camera;

}