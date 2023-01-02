using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private bool autoUpdate = true;
    [Range(2, 256)]
    public int resolution = 10;

    [Expandable]
    [OnValueChanged("OnShapeSettingsUpadetd")]
    public ShapeSettings shapeSettings;
    [Expandable]
    [OnValueChanged("OnColorSettingsUpadetd")]
    public ColorSettings colorSettings;

    [Button("Generate Planet")]
    private void GeneratePlanetMethod() { GeneratePlanet(); }

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();
    TerrainFace[] terrainFaces;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    MeshCollider meshCollider;

    private void OnValidate()
    {
        GeneratePlanet();
    }

    public void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
        }
    }
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpadetd()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }
    public void OnColorSettingsUpadetd()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }

    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.UpdateUVs(colorGenerator);
        }
        colorGenerator.UpdateColors();
    }

}