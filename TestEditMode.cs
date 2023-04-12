using UnityEngine;
using System.Collections;
using TMPro;

[ExecuteInEditMode]
public class TestEditMode : MonoBehaviour
{

  void Start()
  {
    Debug.Log("StartTestEditMode");
  }

  void Update()
  {
    Debug.Log("Test01");
    //find the object whose name starts with "Fukidashi"
    GameObject[] fukidashi = GameObject.FindGameObjectsWithTag("Fukidashi");
    // GameObject.Find("Clock").transform.position = new Vector3(0, 0, 0);
    Debug.Log("fukidashi.Length: " + fukidashi.Length);
    for (int i = 0; i < fukidashi.Length; i++)
    {
      //change the order in layer of the fukidashi
      //fukidashi has TextMeshProUGUI component and it has canvas component
      //change the sorting layer and order in layer of the text and the fukidashi

      //find the child text in the hierarchy
      fukidashi[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 2 * i + 2;
      fukidashi[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * i + 1;
    }
  }

  void OnGUI()
  {
    Debug.Log("OnGUI");
  }

  void OnRenderObject()
  {
    Debug.Log("OnRenderObject");
  }
}
