// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/8/31   17:25:20
// -----------------------------------------------
// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/8/31   14:37:8
// -----------------------------------------------
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(EnvironmentController))]
public class EnvironmentControllerEditor : Editor
{
    public enum EnvironmentLightMode
    {
        Skybox, Gradient, Color,
    }
    public enum ReflectionResolution
    {
        Res16, Res32, Res64, Res128, Res256, Res512, Res1024, Res2048,
    }

    EnvironmentController Environment;
    private EnvironmentLightMode sourceMode = EnvironmentLightMode.Skybox;
    private ReflectionResolution reflectionResolution = ReflectionResolution.Res128;

    private void OnEnable()
    {
        Environment = (EnvironmentController)target;
    }

    private void OnDisable()
    {
        Save();
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.indentLevel = 0;
        EditorGUILayout.LabelField("环境编辑", EditorStyles.largeLabel);
        EditorGUILayout.Separator();

        EditorGUI.indentLevel = 1;
        EditorGUILayout.LabelField("Environment", EditorStyles.boldLabel);
        OnInspectorEnvironment();

        EditorGUI.indentLevel = 1;
        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        OnInspectorOtherSettings();

        if (GUILayout.Button("记录到GameObject", EditorStyles.miniButtonMid))
        {
            Save();
        }
    }

    private void Save()
    {
        Environment.haloStrength = RenderSettings.haloStrength;
        Environment.defaultReflectionResolution = RenderSettings.defaultReflectionResolution;
        Environment.defaultReflectionMode = RenderSettings.defaultReflectionMode;
        Environment.reflectionBounces = RenderSettings.reflectionBounces;
        Environment.reflectionIntensity = RenderSettings.reflectionIntensity;
        Environment.customReflection = RenderSettings.customReflection;
        Environment.ambientProbe = RenderSettings.ambientProbe;
        Environment.sun = RenderSettings.sun;
        Environment.skybox = RenderSettings.skybox;
        Environment.subtractiveShadowColor = RenderSettings.subtractiveShadowColor;
        Environment.flareStrength = RenderSettings.flareStrength;
        Environment.ambientLight = RenderSettings.ambientLight;
        Environment.ambientGroundColor = RenderSettings.ambientGroundColor;
        Environment.ambientEquatorColor = RenderSettings.ambientEquatorColor;
        Environment.ambientSkyColor = RenderSettings.ambientSkyColor;
        Environment.ambientMode = RenderSettings.ambientMode;
        Environment.fogDensity = RenderSettings.fogDensity;
        Environment.fogColor = RenderSettings.fogColor;
        Environment.fogMode = RenderSettings.fogMode;
        Environment.fogEndDistance = RenderSettings.fogEndDistance;
        Environment.fogStartDistance = RenderSettings.fogStartDistance;
        Environment.fog = RenderSettings.fog;
        Environment.ambientIntensity = RenderSettings.ambientIntensity;
        Environment.flareFadeSpeed = RenderSettings.flareFadeSpeed;
    }

    private void OnInspectorEnvironment()
    {
        EditorGUI.indentLevel++;
        RenderSettings.skybox = (Material)EditorGUILayout.ObjectField("Skybox Material", RenderSettings.skybox, typeof(Material), false);
        RenderSettings.sun = (Light)EditorGUILayout.ObjectField("Sun Source", RenderSettings.sun, typeof(Light), false);
        RenderSettings.subtractiveShadowColor = EditorGUILayout.ColorField("Realtime Shadow Color", RenderSettings.subtractiveShadowColor);

        EditorGUILayout.LabelField("Environment Lighting", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        sourceMode = (EnvironmentLightMode)EditorGUILayout.EnumPopup("Source", sourceMode);
        if (sourceMode == EnvironmentLightMode.Skybox)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientLight = EditorGUILayout.ColorField(new GUIContent("Ambient Color"), RenderSettings.ambientLight, true, false, true);
        }
        else if (sourceMode == EnvironmentLightMode.Gradient)
        {
            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = EditorGUILayout.ColorField(new GUIContent("Sky Color"), RenderSettings.ambientSkyColor, true, false, true);
            RenderSettings.ambientEquatorColor = EditorGUILayout.ColorField(new GUIContent("Equator Color"), RenderSettings.ambientEquatorColor, true, false, true);
            RenderSettings.ambientGroundColor = EditorGUILayout.ColorField(new GUIContent("Ground Color"), RenderSettings.ambientGroundColor, true, false, true);
        }
        else if (sourceMode == EnvironmentLightMode.Color)
        {
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = EditorGUILayout.ColorField(new GUIContent("Ambient Color"), RenderSettings.ambientLight, true, false, true);
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.LabelField("Environment Reflection", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        RenderSettings.defaultReflectionMode = (DefaultReflectionMode)EditorGUILayout.EnumPopup("Source", RenderSettings.defaultReflectionMode);
        if (RenderSettings.defaultReflectionMode == DefaultReflectionMode.Skybox)
        {
            reflectionResolution = (ReflectionResolution)EditorGUILayout.EnumPopup("Resolution", reflectionResolution);
            switch (reflectionResolution)
            {
                case ReflectionResolution.Res16: RenderSettings.defaultReflectionResolution = 16; break;
                case ReflectionResolution.Res32: RenderSettings.defaultReflectionResolution = 16; break;
                case ReflectionResolution.Res64: RenderSettings.defaultReflectionResolution = 64; break;
                case ReflectionResolution.Res128: RenderSettings.defaultReflectionResolution = 128; break;
                case ReflectionResolution.Res256: RenderSettings.defaultReflectionResolution = 256; break;
                case ReflectionResolution.Res512: RenderSettings.defaultReflectionResolution = 512; break;
                case ReflectionResolution.Res1024: RenderSettings.defaultReflectionResolution = 1024; break;
                case ReflectionResolution.Res2048: RenderSettings.defaultReflectionResolution = 2048; break;
            }         
        }
        else
        {
            RenderSettings.customReflection = (Cubemap)EditorGUILayout.ObjectField("Cubemap", RenderSettings.customReflection, typeof(Cubemap), false);
        }

        RenderSettings.reflectionIntensity = EditorGUILayout.Slider("Intensity Multiplier", RenderSettings.reflectionIntensity, 0, 1);
        RenderSettings.reflectionBounces = EditorGUILayout.IntSlider("Bounces", RenderSettings.reflectionBounces, 1, 5);
    
    }

    private void OnInspectorOtherSettings()
    {
        EditorGUI.indentLevel++;
        RenderSettings.fog = EditorGUILayout.Toggle("Fog enabled", RenderSettings.fog);
        if (RenderSettings.fog)
        {
            EditorGUI.indentLevel++;
            RenderSettings.fogColor = EditorGUILayout.ColorField("Fog Color", RenderSettings.fogColor);
            RenderSettings.fogMode = (FogMode)EditorGUILayout.EnumPopup("Fog Mode", RenderSettings.fogMode);
            if (RenderSettings.fogMode == FogMode.Linear)
            {
                RenderSettings.fogStartDistance = EditorGUILayout.FloatField("Start", RenderSettings.fogStartDistance);
                RenderSettings.fogEndDistance = EditorGUILayout.FloatField("End", RenderSettings.fogEndDistance);
            }
            else
            {
                RenderSettings.fogDensity = EditorGUILayout.FloatField("Fog Density", RenderSettings.fogDensity);
            }
            EditorGUI.indentLevel--;
        }
        RenderSettings.haloStrength = EditorGUILayout.Slider("Halo Strength", RenderSettings.haloStrength, 0, 1);
        RenderSettings.flareFadeSpeed = EditorGUILayout.FloatField("Flare Fade Speed", RenderSettings.flareFadeSpeed);
        RenderSettings.flareStrength = EditorGUILayout.Slider("Flare Strength", RenderSettings.flareStrength, 0, 1);
    }
}
