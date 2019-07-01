using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsGenerator : MonoBehaviour
{
    public Transform prefab;
    public float coolDown = 3;
    public int count = 3;
    public int width;
    public int height;
    public int countAtStart = 100;
    private float timer;
    private float x;
    private float z;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < countAtStart; i++)
        {
            x = UnityEngine.Random.Range(-width, width);
            z = UnityEngine.Random.Range(-height, height);
            Instantiate(prefab, new Vector3(x, 0.0f, z), Quaternion.identity);
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > coolDown)
        {
            for (i = 0; i < count; i++)
            {
                x = UnityEngine.Random.Range(-width, width);
                z = UnityEngine.Random.Range(-height, height);
                Instantiate(prefab, new Vector3(x, 0.0f, z), Quaternion.identity);
                timer = 0;
            }
        }
    }
}
