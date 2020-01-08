using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Map
{
    public Vector2Int mapSize;
    public float mapScale;
    public List<Column> columns;
}

[Serializable]
public class Column
{
    public enum Type {Ground, Building};
    
    public float height;
    public Type type;
}