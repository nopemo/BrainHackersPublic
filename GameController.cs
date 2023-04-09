using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class GameController : MonoBehaviour
{
  public int[] limitDistance = { 150, 0 };
  int currentId = -1;
  int currentState = 0;
  string currentAnswer = "It has not been set yet.";
  GameObject QuestionBalloon;
  List<int> boothIds = new List<int>();
  void Awake()
  {
    QuestionBalloon = GameObject.Find("Question");
    //shoufle the boothIds from 1 to 4 randomly
    for (int i = 1; i <= 4; i++)
    {
      boothIds.Add(i);
    }

    // Shuffle the boothIds list
    for (int i = 0; i < boothIds.Count; i++)
    {
      int temp = boothIds[i];
      int randomIndex = UnityEngine.Random.Range(i, boothIds.Count);
      boothIds[i] = boothIds[randomIndex];
      boothIds[randomIndex] = temp;
    }
  }
  void Start()
  {
    DrowInitialEdges();
    currentId = -1;
    currentState = 0;
    currentAnswer = "";
  }
  public void SetCurrentProperties(int id, int state, string answer)
  {
    currentId = id;
    currentState = state;
    currentAnswer = answer;
  }
  void DrowInitialEdges()
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
  public void CheckAnswer(string inputtext)
  {
    if (currentId != -1 && inputtext == currentAnswer)
    {
      Debug.Log("Correct! id:" + currentId.ToString() + " state:" + currentState.ToString());
      if (currentId > 90)
      {
        GameObject.Find("NodeGame (" + currentId.ToString() + ")").GetComponent<Node>().ChangeState(2);
      }
      else
      {
        GameObject.Find("NodeNorm (" + currentId.ToString() + ")").GetComponent<Node>().ChangeState(2);
      }
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "たしかに!";
      SetCurrentProperties(-1, 0, "It has not been set yet.");
    }
    else
    {
      Debug.Log("Wrong! id:" + currentId.ToString() + " state:" + currentState.ToString());
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "違う気がする...";
    }
  }
  public List<int> GetBoothIds()
  {
    return boothIds;
  }
}
