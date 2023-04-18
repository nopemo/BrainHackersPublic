using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class Node : MonoBehaviour, IPointerClickHandler
{
  public int isGameNode = 0;
  public int state = 0;
  public int id = 0;
  /* 0 = not active
   * 1 = active but not clear
   * 2 = clear
   */
  public string answer = "";
  GameObject QuestionBalloon;
  List<GameObject> nearNodes = new List<GameObject>();
  [SerializeField] List<GameObject> deleteObstacleBalloons;

  GameObject EdgeActive;
  GameObject GameController;
  void Awake()
  {
    id = ExtractNumberFromObjectName(gameObject);
    if (gameObject.name.Contains("Game"))
    {
      isGameNode = 1;
    }
    QuestionBalloon = GameObject.Find("Question");
    GameController = GameObject.Find("GameController");
    int[] limitDistance = GameObject.Find("GameController").GetComponent<GameController>().limitDistance;
    // nodeSprites = Resources.LoadAll<Sprite>("node");
    ChangeState(state);
    EdgeActive = Resources.Load("EdgeActive") as GameObject;
    nearNodes = makeNearNodes(limitDistance);
    //print my uid to the child text in the hierarchy
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = id.ToString();
    //change the sorting layer and order in layer of the text
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingLayerName = "Node";
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 1;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("Clicked on node");
    if (state >= 1)
    {
      if (isGameNode == 1)
      {
        ActivateMiniGameBalloon(state, id, answer);
      }
      else
      {
        ActivateQuestionBalloon(state, id, answer);
      }
    }
  }

  public void ChangeState(int state)
  {
    this.state = state;
    // GetComponent<SpriteRenderer>().sprite = nodeSprites[state];
    if (state == 0)
    {
      //change the color to [UIColor colorWithRed:0.286f green:0.364f blue:0.396f alpha:1.000f];
      GetComponent<SpriteRenderer>().color = new Color(0.286f, 0.364f, 0.396f);
    }
    else if (state == 1)
    {
      //change the color to [UIColor colorWithRed:0.043f green:0.843f blue:0.843f alpha:1.000f]
      GetComponent<SpriteRenderer>().color = new Color(0.043f, 0.843f, 0.843f);
      foreach (GameObject node in nearNodes)
      {
        if (node.GetComponent<Node>().state == 1)
        {
          DrowEdgeActive(gameObject, node);
        }
      }
    }
    else if (state == 2)
    {
      //change the color to [UIColor colorWithRed:1.000f green:0.388f blue:0.050f alpha:1.000f]
      GetComponent<SpriteRenderer>().color = new Color(1.000f, 0.388f, 0.050f);
      //change the near nodes to active except already active nodes
      foreach (GameObject node in nearNodes)
      {
        Debug.Log("Here is " + node.name + "");
        if (node.GetComponent<Node>().state == 0)
        {
          Debug.Log("From " + gameObject.name + " to " + node.name + "state changed to 1");
          node.GetComponent<Node>().ChangeState(1);
        }
        Debug.Log("Drow Edge from " + gameObject.name + " to " + node.name);
        DrowEdgeActive(gameObject, node);
      }
    }
  }
  void LoadImageByName(string name, GameObject obj)
  {
    // Sprite sprite = Resources.Load<Sprite>(name);
    // obj.GetComponent<SpriteRenderer>().sprite = sprite;
  }

  void DrowEdgeActive(GameObject node1, GameObject node2)
  {
    //Delete the edge if already exists
    string edgeName = "EdgeActive" + node1.GetComponent<Node>().id.ToString() + "-" + node2.GetComponent<Node>().id.ToString();
    string anotherEdgeName = "EdgeActive" + node2.GetComponent<Node>().id.ToString() + "-" + node1.GetComponent<Node>().id.ToString();
    if (GameObject.Find(edgeName))
    {
      Destroy(GameObject.Find(edgeName));
    }
    if (GameObject.Find(anotherEdgeName))
    {
      Destroy(GameObject.Find(anotherEdgeName));
    }
    GameObject edge = Instantiate(EdgeActive);
    edge.transform.SetParent(GameObject.Find("Edges").transform);
    //drow line from node1.transform.position to node2.transform.position
    edge.GetComponent<LineRenderer>().SetPosition(0, node1.transform.position);
    edge.GetComponent<LineRenderer>().SetPosition(1, node2.transform.position);
    //change name using node1 and node2
    edge.name = edgeName;
    //change color based on the state of the nodes
    edge.GetComponent<LineRenderer>().startColor = node2.GetComponent<SpriteRenderer>().color;
    Debug.Log("Start Color: " + node1.GetComponent<SpriteRenderer>().color
    + "End Color: " + node2.GetComponent<SpriteRenderer>().color
    + "Node1: " + node1.name
    + "Node2: " + node2.name);

    edge.GetComponent<LineRenderer>().endColor = node2.GetComponent<SpriteRenderer>().color;
  }

  public void ActivateQuestionBalloon(int state, int id, string answer)
  {
    // Activate the balloon
    QuestionBalloon.SetActive(true);

    // Change the image of the balloon child "QuestionImage" to /Assets/Resources/Questions/Qxx.png
    if (state == 2)
    {
      // Add the clear text below the image.
      Debug.Log("Already clear");
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "もうすでに解決している。";
    }
    else
    {
      // Add the question text below the image.
      Debug.Log("Not clear");
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "この答えなんだっけ...?";
    }
    // Get the "QuestionImage" child object of the balloon
    GameObject questionImageObj = QuestionBalloon.transform.Find("QuestionImage").gameObject;
    // Load the image from the Resources/Questions folder based on the id
    Sprite questionSprite = Resources.Load<Sprite>("Questions/Q" + id.ToString("D2"));
    // Set the loaded image to the "QuestionImage" object using SpriteRenderer
    questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
    GameController.GetComponent<GameController>().SetCurrentProperties(id, state, isGameNode, answer);
  }
  public void ActivateMiniGameBalloon(int state, int id, string answer)
  {
    // Activate the balloon
    QuestionBalloon.SetActive(true);

    // Change the image of the balloon child "QuestionImage" to /Assets/Resources/Questions/Qxx.png
    GameObject questionImageObj = QuestionBalloon.transform.Find("QuestionImage").gameObject;
    if (state == 2)
    {
      // Add the clear text below the image.
      Debug.Log("Already clear");
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "もうすでに解決している。";
      Sprite questionSprite = Resources.Load<Sprite>("Questions/MiniGameCleared");
      questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
    }
    else
    {
      // Add the question text below the image.
      Debug.Log("Not clear");
      List<int> boothIds = GameController.GetComponent<GameController>().GetBoothIds();
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "ブース" + boothIds[id % 4] + "へ向かおう!";
      Sprite questionSprite = Resources.Load<Sprite>("Questions/MiniGame" + boothIds[id % 4].ToString("D2"));
      questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
    }
    // Get the "QuestionImage" child object of the balloon
    // Load the image from the Resources/Questions folder based on the id
    // Set the loaded image to the "QuestionImage" object using SpriteRenderer
    GameController.GetComponent<GameController>().SetCurrentProperties(id, state, isGameNode, answer);
  }
  List<GameObject> makeNearNodes(int[] limitDistance)
  {
    List<GameObject> nodeNorms = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeNorm"));
    List<GameObject> nearNodes = new List<GameObject>();
    for (int i = 0; i < nodeNorms.Count; i++)
    {
      if (Vector3.Distance(transform.position, nodeNorms[i].transform.position) < limitDistance[isGameNode])
      {
        nearNodes.Add(nodeNorms[i]);
      }
    }
    List<GameObject> nodeGames = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeGame"));
    for (int i = 0; i < nodeGames.Count; i++)
    {
      if (Vector3.Distance(transform.position, nodeGames[i].transform.position) < limitDistance[1])
      {
        nearNodes.Add(nodeGames[i]);
      }
    }
    if (isGameNode == 1)
    {
      Debug.Log("This is game node of id: " + id);
      for (int i = 0; i < nearNodes.Count; i++)
      {
        Debug.Log("nearNodes[" + i + "] name: " + nearNodes[i].name);
      }
    }
    return nearNodes;
  }

  int ExtractNumberFromObjectName(GameObject obj)
  {
    string objectName = obj.name;
    // Use regular expressions to match only the digits in the object name
    Match match = Regex.Match(objectName, @"\d+");
    if (match.Success)
    {
      // If a number is found, convert it to an integer and return it
      return int.Parse(match.Value);
    }
    else
    {
      Debug.LogWarning("No number found in the object name");
      return -1;
    }
  }
}
