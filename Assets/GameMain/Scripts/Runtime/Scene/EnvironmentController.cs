// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/8/31   17:25:29
// -----------------------------------------------
// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/8/31   14:14:42
// -----------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 環境光のモード定義のため利用
using UnityEngine.Rendering;

public class EnvironmentController : MonoBehaviour
{
    public float haloStrength;
    public int defaultReflectionResolution;
    public DefaultReflectionMode defaultReflectionMode;
    public int reflectionBounces;
    public float reflectionIntensity;
    public Cubemap customReflection;
    public SphericalHarmonicsL2 ambientProbe;
    public Light sun;
    public Material skybox;
    public Color subtractiveShadowColor;
    public float flareStrength;
    public Color ambientLight;
    public Color ambientGroundColor;
    public Color ambientEquatorColor;
    public Color ambientSkyColor;
    public AmbientMode ambientMode;
    public float fogDensity;
    public Color fogColor;
    public FogMode fogMode;
    public float fogEndDistance;
    public float fogStartDistance;
    public bool fog;
    public float ambientIntensity;
    public float flareFadeSpeed;

    private void Start()
    {
        if (!Application.isEditor) return; 
        RenderSettings.haloStrength = haloStrength;
        RenderSettings.defaultReflectionResolution = defaultReflectionResolution;
        RenderSettings.defaultReflectionMode = defaultReflectionMode;
        RenderSettings.reflectionBounces = reflectionBounces;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        RenderSettings.customReflection = customReflection;
        RenderSettings.ambientProbe = ambientProbe;
        RenderSettings.sun = sun;
        RenderSettings.skybox = skybox;
        RenderSettings.subtractiveShadowColor = subtractiveShadowColor;
        RenderSettings.flareStrength = flareStrength;
        RenderSettings.ambientLight = ambientLight;
        RenderSettings.ambientGroundColor = ambientGroundColor;
        RenderSettings.ambientEquatorColor = ambientEquatorColor;
        RenderSettings.ambientSkyColor = ambientSkyColor;
        RenderSettings.ambientMode = ambientMode;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogEndDistance = fogEndDistance;
        RenderSettings.fogStartDistance = fogStartDistance;
        RenderSettings.fog = fog;
        RenderSettings.ambientIntensity = ambientIntensity;
        RenderSettings.flareFadeSpeed = flareFadeSpeed;

        DynamicGI.UpdateEnvironment();
    }
}
