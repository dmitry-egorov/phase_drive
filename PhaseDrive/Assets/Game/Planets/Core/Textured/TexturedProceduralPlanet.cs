using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TexturedProceduralPlanet : MonoBehaviour
{
    [Range(2, 256)]
    public int mesh_resolution;
    public int texture_resolution;

    public float size = 1f;
    public float offset = 0f;
    public Color CloudColor;
    [Range(1.01f, 8f)]
    public float CloudLacunarity;
    [Range(.01f, 0.99f)]
    public float CloudPersistance;
    [Range(0.0f, 0.99f)]
    public float CloudWarpFactor;

    public Color continet_color;
    public Color ocean_color;
    [Range(1.01f, 8f)]
    public float lacunarity;
    [Range(.01f, 0.99f)]
    public float persistance;
    [Range(0.0f, 0.99f)]
    public float warp_factor;


    void Start()
    {
        GeneratePlanet();
    }

    public void GeneratePlanet()
    {
        var sw = new Stopwatch();

        sw.Restart();
        var modelObject = GenerateModelObject();
        var elapsedForObject = sw.ElapsedMilliseconds;

        sw.Restart();
        GenerateTexture(modelObject);
        var elapsedForTexture = sw.ElapsedMilliseconds;

        sw.Restart();
        GenerateMesh(modelObject);
        var elapsedForMesh = sw.ElapsedMilliseconds;

        Debug.Log($"Generated a planet. Object in {elapsedForObject}ms, texture in {elapsedForTexture}ms, mesh in {elapsedForMesh}ms.");
    }

    private void GenerateTexture(GameObject modelObject)
    {
        var generator = new Material(Shader.Find("Procedural Planet/Generator"));
        generator.SetFloat("_Offset", offset);
        generator.SetColor("_CloudColor", CloudColor);
        generator.SetFloat("_CloudLacunarity", CloudLacunarity);
        generator.SetFloat("_CloudPersistence", CloudPersistance);
        generator.SetFloat("_CloudWarpFactor", CloudWarpFactor);
        generator.SetColor("_ContinentColor", continet_color);
        generator.SetColor("_OceanColor", ocean_color);
        generator.SetFloat("_LandLacunarity", lacunarity);
        generator.SetFloat("_LandPersistence", persistance);
        generator.SetFloat("_LandWarpFactor", warp_factor);

        var material = modelObject.GetComponent<MeshRenderer>().sharedMaterial;

        var rt = (RenderTexture)material.GetTexture("_MainTex");

        Graphics.Blit(null, rt, generator);
    }

    private void GenerateMesh(GameObject modelObject)
    {
        var directions = new[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        var mesh = modelObject.GetComponent<MeshFilter>().sharedMesh;

        var verticesPerSideCount = mesh_resolution * mesh_resolution;
        var verticesCount = verticesPerSideCount * directions.Length;

        if (mesh.vertices.Length == verticesCount)
            return;

        var vertices = new Vector3[verticesCount];
        var uvs = new Vector2[verticesCount];
        var triangles = new int[(mesh_resolution - 1) * (mesh_resolution - 1) * 6 * directions.Length];
        var triIndex = 0;

        for (var i = 0; i < directions.Length; i++)
        {
            var localUp = directions[i];
            var axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            var axisB = Vector3.Cross(localUp, axisA);

            for (var y = 0; y < mesh_resolution; y++)
            {
                for (var x = 0; x < mesh_resolution; x++)
                {
                    var index = x + y * mesh_resolution + i * verticesPerSideCount;
                    var percent = new Vector2(x, y) / (mesh_resolution - 1);
                    var pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                    var pointOnUnitSphere = pointOnUnitCube.normalized;
                    vertices[index] = pointOnUnitSphere * size;
                    var uv = Vector2.Scale(percent + new Vector2(i, 0f), new Vector2(1.0f / directions.Length, 1.0f));
                    uvs[index] = uv;

                    if (x == mesh_resolution - 1 || y == mesh_resolution - 1)
                        continue;

                    triangles[triIndex] = index;
                    triangles[triIndex + 1] = index + mesh_resolution + 1;
                    triangles[triIndex + 2] = index + mesh_resolution;

                    triangles[triIndex + 3] = index;
                    triangles[triIndex + 4] = index + 1;
                    triangles[triIndex + 5] = index + mesh_resolution + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        
        mesh.RecalculateNormals();
    }

    private GameObject GenerateModelObject()
    {
        if (transform.childCount == 1)
            return transform.GetChild(0).gameObject;

        var meshObj = new GameObject("model");
        meshObj.layer = gameObject.layer;
        meshObj.transform.SetParent(transform, false);

        var renderer = meshObj.AddComponent<MeshRenderer>();
        var rt = new RenderTexture(6 * texture_resolution, texture_resolution, 0, RenderTextureFormat.ARGB32);
        rt.filterMode = FilterMode.Point;
        var material = new Material(Shader.Find("Procedural Planet/Runtime"));
        material.SetTexture("_MainTex", rt);
        renderer.sharedMaterial = material;

        var meshFilters = meshObj.AddComponent<MeshFilter>();
        var mesh = new Mesh();
        meshFilters.sharedMesh = mesh;

        return meshObj;
    }
}


