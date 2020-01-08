using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public Grid g;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.75F);

        for (int i = 1; i <= g.sizeGrid.x / g.sizeVoxel; i++)
        {
            for (int k = 1; k <= g.sizeGrid.z / g.sizeVoxel; k++)
            {
                Gizmos.DrawLine(transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, g.sizeGrid.y / g.sizeVoxel, k) * g.sizeVoxel, transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, 0, k) * g.sizeVoxel);
            }
        }
    }
}
