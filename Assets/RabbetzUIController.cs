using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbetzUIController : MonoBehaviour
{
    private const string rabbetText = "Rabbet count: ";
    public Text rabbetCount;
    public GameObject[] rabbetz;
    // Update is called once per frame
    void Update()
    {
        int count;
        rabbetz = GameObject.FindGameObjectsWithTag("rabbet");
        count = 0;
        foreach (GameObject rabbet in rabbetz)
        {
            count++;
        }
        rabbetCount.text = rabbetText + count.ToString();
    }
}
