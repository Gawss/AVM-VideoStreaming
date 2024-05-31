using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using UnityEngine.UI;

namespace Akvfx
{

    [AddComponentMenu("VFX/Property Binders/Akvfx/Point Cloud Binder")]
    [VFXBinder("Akvfx/Point Cloud")]
    sealed class VFXPointCloudBinder : VFXBinderBase
    {
        #region VFX Binder Implementation

        public string ColorMapProperty
        {
            get => (string)_colorMapProperty;
            set => _colorMapProperty = value;
        }

        public string PositionMapProperty
        {
            get => (string)_positionMapProperty;
            set => _positionMapProperty = value;
        }

        public string WidthProperty
        {
            get => (string)_widthProperty;
            set => _widthProperty = value;
        }

        public string HeightProperty
        {
            get => (string)_heightProperty;
            set => _heightProperty = value;
        }

        [VFXPropertyBinding("UnityEngine.Texture2D"), SerializeField]
        ExposedProperty _colorMapProperty = "ColorMap";

        [VFXPropertyBinding("UnityEngine.Texture2D"), SerializeField]
        ExposedProperty _positionMapProperty = "PositionMap";

        [VFXPropertyBinding("System.UInt32"), SerializeField]
        ExposedProperty _widthProperty = "MapWidth";

        [VFXPropertyBinding("System.UInt32"), SerializeField]
        ExposedProperty _heightProperty = "MapHeight";

        public int imageWidth;

        public int imageHeight;

        public RenderTexture RT_colorMap;
        public RenderTexture RT_positionMap;

        // public RawImage IMG_colorMap;
        // public RawImage IMG_positionMap;

        public string targetName;

        public override bool IsValid(VisualEffect component)
          => RT_colorMap != null &&
                component.HasTexture(_colorMapProperty) &&
                component.HasTexture(_positionMapProperty) &&
                component.HasUInt(_widthProperty) &&
                component.HasUInt(_heightProperty);

        public override void UpdateBinding(VisualEffect component)
        {
            if (RT_colorMap == null) return;
            if (RT_positionMap == null) return;
            component.SetTexture(_colorMapProperty, RT_colorMap);
            component.SetTexture(_positionMapProperty, RT_positionMap);
            component.SetUInt(_widthProperty, (uint)imageWidth);
            component.SetUInt(_heightProperty, (uint)imageHeight);
        }

        public override string ToString()
          => "Point Cloud : " +
             $"'{_positionMapProperty}' -> {targetName ?? "(null)"}";

        #endregion
    }

}
