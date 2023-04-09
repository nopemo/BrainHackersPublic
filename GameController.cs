using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
  public int[] limitDistance = { 150, 0 };
  void Start()
  {
    drowInitialEdges();
  }
  void drowInitialEdges()
  {
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
          line.name = "Edge (" + nodeNorms[i].GetComponent<Node>().id.ToString("00") + "," + nodeNorms[j].GetComponent<Node>().id.ToString("00") + ")";
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
          //make Edge object below "Edges" object
          //Solve the error UnityException: Transform child out of bounds
          //Node.Start()(at Assets / Scripts / Node.cs:43)
          GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity, GameObject.Find("Edges").transform);

          line.GetComponent<Renderer>().sortingLayerName = "Edge";
          LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
          lineRenderer.SetPosition(0, nodeGames[i].transform.position);
          lineRenderer.SetPosition(1, nodeNorms[j].transform.position);
          line.name = "Edge (" + nodeGames[i].GetComponent<Node>().id.ToString("00") + "," + nodeNorms[j].GetComponent<Node>().id.ToString("00") + ")";
        }
      }
    }
  }
}
