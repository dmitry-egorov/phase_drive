using UnityEngine;
using UnityEngine.Serialization;

public class GeneratedPlanet : MonoBehaviour
{
    [Range(2, 256)]
    [FormerlySerializedAs("resolution")]public int mesh_resolution;
    public int texture_resolution;
    public float size = 1f;
    public Material generator_material;
    public Material material;

    void Start()
    {
        GeneratePlanet();
    }

    public void GeneratePlanet()
    {

        GenerateTexture();
        GenerateMesh();
    }

    public void GenerateTexture()
    {
        var rt = new RenderTexture(6 * texture_resolution, texture_resolution, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(null, rt, generator_material, -1);
        material.SetTexture("_MainTex", rt);
    }

    public void GenerateMesh()
    {
        var meshObj = GenerateMeshObject();
        
        meshObj.GetComponent<MeshRenderer>().sharedMaterial = material;

        var mesh = meshObj.GetComponent<MeshFilter>().sharedMesh;

        var directions = new[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        var verticesPerSideCount = mesh_resolution * mesh_resolution;
        var verticesCount = verticesPerSideCount * directions.Length;
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
                    Vector2 uv = Vector2.Scale(percent + new Vector2(i, 0f), new Vector2(1.0f / directions.Length, 1.0f));
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

    private GameObject GenerateMeshObject()
    {
        if (transform.childCount == 1)
            return transform.GetChild(0).gameObject;

        var meshObj = new GameObject("model");
        meshObj.layer = gameObject.layer;
        meshObj.transform.SetParent(transform, false);

        meshObj.AddComponent<MeshRenderer>();

        var meshFilters = meshObj.AddComponent<MeshFilter>();
        var mesh = new Mesh();
        meshFilters.sharedMesh = mesh;

        return meshObj;
    }
}


