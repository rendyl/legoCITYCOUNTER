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
        meshColumns = CreateVoxelColumnsFromJson(jsonFile.text);
        voxelMesh.vertices = GetVertices(meshColumns);
        voxelMesh.triangles = GetTriangles(meshColumns);
        voxelMesh.RecalculateNormals();

        meshFilter.mesh = voxelMesh;
    }

    private static MeshColumn[,] CreateVoxelColumnsFromJson(string json)
    {
        Map map = JsonUtility.FromJson<Map>(json);
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
        Vector3[] vertices = new Vector3[meshColumns.Length * 4];
        foreach (MeshColumn meshColumn in meshColumns)
        {
            for (int i = 0; i < 4; i++)
            {
                vertices[meshColumn.id * 4 + i] = meshColumn.vertices[i];
            }
        }

        return vertices;
    }

    private static int[] GetTriangles(MeshColumn[,] meshColumns)
    {
        int[] triangles = new int[meshColumns.Length * 6];

        foreach (MeshColumn meshColumn in meshColumns)
        {
            int offset = meshColumn.id * 4;
            triangles[meshColumn.id * 6] = offset;
            triangles[meshColumn.id * 6 + 1] = offset + 1;
            triangles[meshColumn.id * 6 + 2] = offset + 2;
            triangles[meshColumn.id * 6 + 3] = offset;
            triangles[meshColumn.id * 6 + 4] = offset + 2;
            triangles[meshColumn.id * 6 + 5] = offset + 3;
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
        this.vertices = new Vector3[4];
        this.vertices[0] = new Vector3(position.x, height, position.y);
        this.vertices[1] = new Vector3(position.x, height, position.y + 1f);
        this.vertices[2] = new Vector3(position.x + 1f, height, position.y + 1f);
        this.vertices[3] = new Vector3(position.x + 1f, height, position.y);
        this.type = type;
    }

    public void SetHeight(float value)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = value;
        }
    }
}
