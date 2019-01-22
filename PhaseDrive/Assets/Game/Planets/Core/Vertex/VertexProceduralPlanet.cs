using UnityEngine;

public class VertexProceduralPlanet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution;
    public float size = 1f;
    public Material material;
    public int noise_seed;

    public float land_frequency = 1f;
    public float land_offset = -10f;

    public Vector3 clouds_scale;
    public float clouds_offset = 10f;

    void Start()
    {
        GeneratePlanet();
    }

    public void GeneratePlanet()
    {
        var meshObj = GenerateMeshObject();

        var mesh = meshObj.GetComponent<MeshFilter>().sharedMesh;
        meshObj.GetComponent<MeshRenderer>().sharedMaterial = material;

        var directions = new[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        var verticesPerSideCount = resolution * resolution;
        var verticesCount = verticesPerSideCount * directions.Length;
        var vertices = new Vector3[verticesCount];
        var uvs = new Vector2[verticesCount];
        var triangles = new int[(resolution - 1) * (resolution - 1) * 6 * directions.Length];
        var triIndex = 0;

        var noise = new Noise(noise_seed);

        for (var i = 0; i < directions.Length; i++)
        {
            var localUp = directions[i];
            var axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            var axisB = Vector3.Cross(localUp, axisA);

            for (var y = 0; y < resolution; y++)
            {
                for (var x = 0; x < resolution; x++)
                {
                    var index = x + y * resolution + i * verticesPerSideCount;
                    var percent = new Vector2(x, y) / (resolution - 1);
                    var pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                    var pointOnUnitSphere = pointOnUnitCube.normalized;
                    vertices[index] = pointOnUnitSphere * size;
                    uvs[index].x = noise.Evaluate(pointOnUnitSphere * land_frequency + Vector3.right * land_offset);
                    uvs[index].y = noise.Evaluate(Vector3.Scale(pointOnUnitSphere, clouds_scale) + Vector3.right * clouds_offset);

                    if (x == resolution - 1 || y == resolution - 1)
                        continue;

                    triangles[triIndex] = index;
                    triangles[triIndex + 1] = index + resolution + 1;
                    triangles[triIndex + 2] = index + resolution;

                    triangles[triIndex + 3] = index;
                    triangles[triIndex + 4] = index + 1;
                    triangles[triIndex + 5] = index + resolution + 1;
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


