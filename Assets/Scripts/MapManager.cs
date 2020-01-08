using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    GameObject[] tiles;


    private void Start()
    {


    }

    private void GatherTiles()
    {
        tiles = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i).gameObject;
        }
    }

}
