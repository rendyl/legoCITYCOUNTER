using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int size;
    public int offset;

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
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0, 0.75F);

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                for(int k = 0; k < size; k++)
                {

                }
            }
        }
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
