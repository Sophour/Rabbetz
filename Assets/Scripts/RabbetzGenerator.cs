using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbetzGenerator : MonoBehaviour
{
    public Transform prefab;
    public int count;
    public int width;
    public int height;
    private float x;
    private float z;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < count; i++)
        {
            x = UnityEngine.Random.Range(-width, width);
            z = UnityEngine.Random.Range(-height, height);
            Instantiate(prefab, new Vector3(x, 0.5f, z), Quaternion.identity);
        }
    }
}
