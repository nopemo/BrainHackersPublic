using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[ExecuteInEditMode]
public class TestEditMode : MonoBehaviour

{
  [SerializeField] int[] limitDistance;
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
  public void DrowInitialEdges()
  {
    string _tmpName;
    GameObject Edge = Resources.Load("Edge") as GameObject;
    List<GameObject> nodeNorms = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeNorm"));
    for (int i = 0; i < nodeNorms.Count - 1; i++)
    {
      for (int j = i + 1; j < nodeNorms.Count; j++)
      {
        if (Vector3.Distance(nodeNorms[i].transform.position, nodeNorms[j].transform.position) < limitDistance[0])
        {
          GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity, GameObject.Find("Edges").transform);
          line.GetComponent<Renderer>().sortingLayerName = "Edge";
          LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
          lineRenderer.SetPosition(0, nodeNorms[i].transform.position);
          lineRenderer.SetPosition(1, nodeNorms[j].transform.position);
          _tmpName = "Edge (" + nodeNorms[i].GetComponent<Node>().id.ToString("00") + "," + nodeNorms[j].GetComponent<Node>().id.ToString("00") + ")";
          if (GameObject.Find(_tmpName))
          {
            Destroy(GameObject.Find(_tmpName));
          }
          line.name = _tmpName;
          line.tag = "Edge";
        }
      }
    }
    List<GameObject> nodeGames = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeGame"));
    for (int i = 0; i < nodeGames.Count; i++)
    {
      for (int j = 0; j < nodeNorms.Count; j++)
      {
        if (Vector3.Distance(nodeGames[i].transform.position, nodeNorms[j].transform.position) < limitDistance[1])
        {
          GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity, GameObject.Find("Edges").transform);
          line.GetComponent<Renderer>().sortingLayerName = "Edge";
          LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
          lineRenderer.SetPosition(0, nodeGames[i].transform.position);
          lineRenderer.SetPosition(1, nodeNorms[j].transform.position);

          _tmpName = "Edge (" + nodeGames[i].GetComponent<Node>().id.ToString("00") + "," + nodeNorms[j].GetComponent<Node>().id.ToString("00") + ")";
          if (GameObject.Find(_tmpName))
          {
            Destroy(GameObject.Find(_tmpName));
          }
          line.name = _tmpName;
          line.tag = "Edge";
        }
      }
    }
  }
  public void DeleteCurrentEdges()
  {
    GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");
    for (int i = 0; i < edges.Length; i++)
    {
      DestroyImmediate(edges[i]);
    }
  }
}
