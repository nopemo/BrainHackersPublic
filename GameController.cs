using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  void Start()
  {
    GameObject Edge = Resources.Load("Edge") as GameObject;

    // get the list of NodeNorm objects using the tag "NodeNorm"
    List<GameObject> NodeNorms = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeNorm"));
    List<Vector3> NodeNormsPos = new List<Vector3>();
    for (int i = 0; i < NodeNorms.Count; i++)
    {
      NodeNormsPos.Add(NodeNorms[i].transform.position);
    }
    for (int i = 0; i < NodeNorms.Count; i++)
    {
      for (int j = 0; j < NodeNorms.Count; j++)
      {
        if (i != j)
        {
          // I want to draw lines using line renderer.
          GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity);
          // Change the Sorting Layer of the line renderer
          line.GetComponent<Renderer>().sortingLayerName = "Edge";
          LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
          lineRenderer.SetPosition(0, NodeNormsPos[i]);
          lineRenderer.SetPosition(1, NodeNormsPos[j]);
        }
      }
    }
  }
}
