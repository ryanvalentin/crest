﻿// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

using UnityEditor;
using UnityEngine;

namespace Crest
{
    /// <summary>
    /// Tags this object as an ocean depth provider. Renders depth every frame and should only be used for dynamic objects.
    /// For static objects, use an Ocean Depth Cache.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu(MENU_PREFIX + "Sea Floor Depth Input")]
    public class RegisterSeaFloorDepthInput : RegisterLodDataInput<LodDataMgrSeaFloorDepth>
    {
        public override bool Enabled => true;

        public bool _assignOceanDepthMaterial = true;

        public override float Wavelength => 0f;

        protected override Color GizmoColor => new Color(1f, 0f, 0f, 0.5f);

        protected override string ShaderPrefix => "Crest/Inputs/Depth";

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_assignOceanDepthMaterial)
            {
                var rend = GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material = new Material(Shader.Find("Crest/Inputs/Depth/Ocean Depth From Geometry"));
                }
            }
        }

#if UNITY_EDITOR
        protected override bool FeatureEnabled(OceanRenderer ocean) => ocean.CreateSeaFloorDepthData;
        protected override string FeatureDisabledErrorMessage => "<i>Create Sea Floor Depth Data</i> must be enabled on the OceanRenderer component.";
        protected override void FixOceanFeatureDisabled(SerializedObject oceanComponent)
        {
            oceanComponent.FindProperty("_createSeaFloorDepthData").boolValue = true;
        }
#endif // UNITY_EDITOR
    }
}
