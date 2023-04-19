using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SampleButtonScript : MonoBehaviour
{
    // [Button("Test01", "Test01")]
    // public bool foo;

    [Button("Test02", "Test02")]
    public bool bar;

    // void Test01()
    // {
    //     Debug.Log("Test01");
    //     //find the object whose name starts with "Fukidashi"
    //     GameObject[] fukidashi = GameObject.FindGameObjectsWithTag("Fukidashi");
    //     for (int i = 0; i < fukidashi.Length; i++)
    //     {
    //         //change the order in layer of the fukidashi
    //         //fukidashi has TextMeshProUGUI component and it has canvas component
    //         //change the sorting layer and order in layer of the text and the fukidashi
    //         fukidashi[i].GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 2*i+2;
    //         fukidashi[i].GetComponent<SpriteRenderer>().sortingOrder = 2*i+1;
    //     }
    // }

    void Test02()
    {
        Debug.Log("Test02");

    }
}
