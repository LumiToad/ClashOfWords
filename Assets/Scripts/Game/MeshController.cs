using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> models;
    [SerializeField]
    private List<Material> materials;

    public bool ChangeMeshByKey(string key, MeshFilter meshFilter)
    {
        if (models == null) return false;
        if (meshFilter == null) return false;

        meshFilter.mesh = GetMesh(key);
        return true;
    }

    public bool ChangeMaterialByKey(string key, MeshRenderer renderer)
    {
        if (materials == null) return false;
        if (renderer == null) return false;

        renderer.material = GetMaterial(key);
        return true;
    }

    public bool ChangeMeshTo(Mesh mesh, MeshFilter meshFilter)
    {
        if (mesh == null) return false;
        if (meshFilter == null) return false;

        meshFilter.mesh = mesh;
        return true;
    }

    public bool ChangeMaterialTo(Material material, MeshRenderer renderer)
    {
        if (material == null) return false;
        if (renderer == null) return false;

        renderer.material = material;
        return true;
    }

    public Mesh GetMesh(string key)
    {
        if (models == null) return null;
        GameObject model = SearchListForName(models, key);
        MeshFilter meshFilter = model.GetComponent<MeshFilter>();

        return meshFilter.sharedMesh;
    }

    public Material GetMaterial(string key)
    {
        if (models == null) return null;

        return SearchListForName(materials, key);
    }

    public bool AddMesh(GameObject model)
    {
        if (models.Contains(model)) return false;

        models.Add(model);
        return true;
    }

    public bool AddTexture(Material material)
    {
        if (materials.Contains(material)) return false;

        materials.Add(material);
        return true;
    }

    public bool RemoveMesh(GameObject model)
    {
        if (!models.Contains(model)) return false;

        return models.Remove(model);
    }

    public bool RemoveTexture(Material material)
    {
        if (!materials.Contains(material)) return false;

        return materials.Remove(material);
    }

    public GameObject GetRandomMesh()
    {
        return models[UnityEngine.Random.Range(0, models.Count + 1)];
    }

    public Material GetRandomMaterial()
    {
        return materials[UnityEngine.Random.Range(0, materials.Count + 1)];
    }

    private GameObject SearchListForName(List<GameObject> list, string key)
    {
        foreach (GameObject model in list) 
        {
            if (model.name == key)
            {
                return model;
            }
        }
        return null;
    }

    private Material SearchListForName(List<Material> list, string key)
    {
        foreach (Material material in list)
        {
            if (material.name == key)
            {
                return material;
            }
        }
        return null;
    }

    public void TintMaterial(Color color)
    {
        
        foreach (Material material in GetComponent<MeshRenderer>().materials)
        {
            material.color = color;
        }
    }
}
