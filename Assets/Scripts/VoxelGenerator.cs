using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelGenerator : MonoBehaviour
{
    public TextAsset jsonFile;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    MeshColumn[,] meshColumns;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

    public void BuildMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = new Material(Shader.Find("Standard"));

        Mesh voxelMesh = new Mesh();
        voxelMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshColumns = CreateVoxelColumnsFromJson(jsonFile.text);
        voxelMesh.vertices = GetVertices(meshColumns);
        voxelMesh.triangles = GetTriangles(meshColumns);
        voxelMesh.RecalculateNormals();
        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateTangents();

        meshFilter.mesh = voxelMesh;
    }

    private static MeshColumn[,] CreateVoxelColumnsFromJson(string json)
    {
        LegoMap map = JsonUtility.FromJson<LegoMap>(json);
        MeshColumn[,] meshColumns = new MeshColumn[map.mapSize.x, map.mapSize.y];

        for (int x = 0; x < meshColumns.GetLength(0); x++)
        {
            for (int y = 0; y < meshColumns.GetLength(1); y++)
            {
                meshColumns[x, y] = new MeshColumn(
                    x * meshColumns.GetLength(1) + y,
                    new Vector2(x, y),
                    map.columns[x * meshColumns.GetLength(1) + y].height,
                    map.columns[x * meshColumns.GetLength(1) + y].type
                    );
            }
        }
        return meshColumns;
    }

    private static Vector3[] GetVertices(MeshColumn[,] meshColumns)
    {
        Vector3[] vertices = new Vector3[meshColumns.Length * 8];
        int sideOffset = meshColumns.Length * 4;
        foreach (MeshColumn meshColumn in meshColumns)
        {
            for (int i = 0; i < 4; i++)
            {
                vertices[meshColumn.id * 4 + i] = meshColumn.vertices[i];
                vertices[sideOffset + meshColumn.id * 4 + i] = meshColumn.vertices[4 + i];
            }
        }

        return vertices;
    }

    private static int[] GetTriangles(MeshColumn[,] meshColumns)
    {
        int topCount = meshColumns.Length * 2;
        int sideCount = (4 * meshColumns.Length - 2 * (meshColumns.GetLength(0) + meshColumns.GetLength(1)));

        int sideId = topCount * 3;
        int sideOffset = meshColumns.Length * 4;

        int[] triangles = new int[3 * (topCount + sideCount)];

        for (int x = 0; x < meshColumns.GetLength(0); x++)
        {
            for (int y = 0; y < meshColumns.GetLength(1); y++)
            {
                MeshColumn meshColumn = meshColumns[x, y];
                int offset = meshColumn.id * 4;
                // Top mesh
                triangles[meshColumn.id * 6] = offset;
                triangles[meshColumn.id * 6 + 1] = offset + 1;
                triangles[meshColumn.id * 6 + 2] = offset + 2;
                triangles[meshColumn.id * 6 + 3] = offset;
                triangles[meshColumn.id * 6 + 4] = offset + 2;
                triangles[meshColumn.id * 6 + 5] = offset + 3;

                // Side mesh
                if (x != 0)
                {
                    if (meshColumns[x, y].GetHeight() > meshColumns[x - 1, y].GetHeight())
                    {
                        triangles[sideId] =  sideOffset + meshColumns[x - 1, y].id * 4 + 2;
                        triangles[sideId + 1] = sideOffset + meshColumns[x, y].id * 4 + 1;
                        triangles[sideId + 2] = sideOffset + meshColumns[x, y].id * 4;
                        triangles[sideId + 3] = sideOffset + meshColumns[x - 1, y].id * 4 + 2;
                        triangles[sideId + 4] = sideOffset + meshColumns[x, y].id * 4;
                        triangles[sideId + 5] = sideOffset + meshColumns[x - 1, y].id * 4 + 3;
                    }
                    else
                    {
                        triangles[sideId] = sideOffset + meshColumns[x, y].id * 4;
                        triangles[sideId + 1] = sideOffset + meshColumns[x - 1, y].id * 4 + 3;
                        triangles[sideId + 2] = sideOffset + meshColumns[x - 1, y].id * 4 + 2;
                        triangles[sideId + 3] = sideOffset + meshColumns[x, y].id * 4;
                        triangles[sideId + 4] = sideOffset + meshColumns[x - 1, y].id * 4 + 2;
                        triangles[sideId + 5] = sideOffset + meshColumns[x, y].id * 4 + 1;
                    }
                    sideId += 6;
                }
                if (y != 0)
                {
                    if (meshColumns[x, y].GetHeight() > meshColumns[x, y - 1].GetHeight())
                    {
                        triangles[sideId] = sideOffset + meshColumns[x, y - 1].id * 4 + 1;
                        triangles[sideId + 1] = sideOffset + meshColumns[x, y].id * 4;
                        triangles[sideId + 2] = sideOffset + meshColumns[x, y].id * 4 + 3;
                        triangles[sideId + 3] = sideOffset + meshColumns[x, y - 1].id * 4 + 1;
                        triangles[sideId + 4] = sideOffset + meshColumns[x, y].id * 4 + 3;
                        triangles[sideId + 5] = sideOffset + meshColumns[x, y - 1].id * 4 + 2;
                    }
                    else
                    {
                        triangles[sideId] = sideOffset + meshColumns[x, y].id * 4 + 3;
                        triangles[sideId + 1] = sideOffset + meshColumns[x, y - 1].id * 4 + 2;
                        triangles[sideId + 2] = sideOffset + meshColumns[x, y - 1].id * 4 + 1;
                        triangles[sideId + 3] = sideOffset + meshColumns[x, y].id * 4 + 3;
                        triangles[sideId + 4] = sideOffset + meshColumns[x, y - 1].id * 4 + 1;
                        triangles[sideId + 5] = sideOffset + meshColumns[x, y].id * 4;
                    }
                    sideId += 6;
                }
            }
        }

        return triangles;
    }
}

public class MeshColumn
{
    public int id;
    public Vector3[] vertices;
    public Column.Type type;

    public MeshColumn(int id, Vector2 position, float height, Column.Type type)
    {
        this.id = id;
        this.vertices = new Vector3[8];
        this.vertices[0] = new Vector3(position.x, height, position.y);
        this.vertices[1] = new Vector3(position.x, height, position.y + 1f);
        this.vertices[2] = new Vector3(position.x + 1f, height, position.y + 1f);
        this.vertices[3] = new Vector3(position.x + 1f, height, position.y);
        this.vertices[4] = this.vertices[0];
        this.vertices[5] = this.vertices[1];
        this.vertices[6] = this.vertices[2];
        this.vertices[7] = this.vertices[3];
        this.type = type;
    }

    public void SetHeight(float value)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = value;
        }
    }

    public float GetHeight()
    {
        return vertices[0].y;
    }
}
