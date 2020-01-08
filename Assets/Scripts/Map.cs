using System;
using UnityEngine;

[Serializable]
public class Map
{
    public Vector2Int mapSize;
    public float mapScale;
    public Column[,] columns;
}

[Serializable]
public class Column
{
    public enum Type {Ground, Building};
    
    public float height;
    public Type type;
}