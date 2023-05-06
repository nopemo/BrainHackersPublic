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
  bool isTouchable = true;
  /* 0 = not active
   * 1 = active but not clear
   * 2 = clear
   */
  public string questionAnswer = "アアアアモト";
  public int questionImageId = 0;
  public string questionImageName = "Q00";
  GameObject QuestionBalloon;
  public List<GameObject> nearNodes = new List<GameObject>();
  [SerializeField] public List<GameObject> deleteObstacleBalloons;

  GameObject EdgeActive;
  GameObject GameController;
  GameObject TestEditMode;
  void Awake()
  {
    id = ExtractNumberFromObjectName(gameObject);
    QuestionBalloon = GameObject.Find("Question");
    GameController = GameObject.Find("GameController");
    TestEditMode = GameObject.Find("TestEditMode");
    // TestEditMode.GetComponent<TestEditMode>().UpdateObjectsAndNearNodes("");
  }
  void Start()
  {
    EdgeActive = Resources.Load("EdgeActive") as GameObject;
    Debug.Log("nearNodes.Count:" + nearNodes.Count.ToString());
    gameObject.transform.GetChild(0).gameObject.SetActive(false);
    GameController.GetComponent<GameController>().SetAnswerToIdDictionary(questionAnswer, id);
    SetTouchable(true);
    if (gameObject.name.Contains("Game"))
    {
      isGameNode = 1;
    }
    int[] limitDistance = { 150, 200 }; // GameObject.Find("GameController").GetComponent<GameController>().limitDistance;
                                        // nodeSprites = Resources.LoadAll<Sprite>("node");
    ChangeState(state);
    if (id > 40 && isGameNode == 0)
    {
      gameObject.SetActive(false);
    }
  }

  public void OnPointerClick(PointerEventData eventData)
  // public void OnClick()
  {
    if (!isTouchable)
    {
      return;
    }
    Debug.Log("Clicked on node");
    if (state >= 1)
    {
      if (isGameNode == 1)
      {
        ActivateMiniGameBalloon(state, id, questionAnswer);
      }
      else
      {
        ActivateQuestionBalloon(state, id, questionAnswer);
      }
    }
  }

  public void ChangeState(int state)
  {
    this.state = state;
    if (!Property.Instance.GetFlag("isTutorial"))
    {
      if (isGameNode == 1)
      {
        Property.Instance.SetFlag("Game" + id.ToString(), state >= 2);
      }
      else
      {
        Property.Instance.SetFlag("Norm" + id.ToString(), state >= 2);
      }
    }
    // GetComponent<SpriteRenderer>().sprite = nodeSprites[state];
    if (state == 0)
    {
      //change the color to [UIColor colorWithRed:0.286f green:0.364f blue:0.396f alpha:1.000f];
      GetComponent<SpriteRenderer>().color = new Color(0.286f, 0.364f, 0.396f);
      //change the size of the node
      if (id != 0 && isGameNode == 0 && false)
      {
        transform.localScale = new Vector3(40f, 40f, 0.0f);
      }
    }
    else if (state == 1)
    {
      //change the color to [UIColor colorWithRed:0.043f green:0.843f blue:0.843f alpha:1.000f]
      GetComponent<SpriteRenderer>().color = new Color(1.000f, 0.388f, 0.050f);
      if (id != 0 && isGameNode == 0 && false)
      {
        transform.localScale = new Vector3(80f, 80f, 0.0f);
      }
      foreach (GameObject node in nearNodes)
      {
        if (node.GetComponent<Node>().state == 0)
        {
          DrowEdgeActive(gameObject, node);
        }
      }
    }
    else if (state == 2)
    {
      //change the color to [UIColor colorWithRed:1.000f green:0.388f blue:0.050f alpha:1.000f]
      GetComponent<SpriteRenderer>().color = new Color(0.043f, 0.843f, 0.843f);
      if (id != 0 && isGameNode == 0 && false)
      {
        transform.localScale = new Vector3(80f, 80f, 0.0f);
      }
      //change the near nodes to active except already active nodes
      foreach (GameObject node in nearNodes)
      {
        if (node == null)
        {
          continue;
        }
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
    //check if node1 and node2 are setactive(true)
    if (!node1.activeSelf || !node2.activeSelf)
    {
      return;
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
    if (node1.GetComponent<Node>().state < 2 || node2.GetComponent<Node>().state < 2)
    {
      //change the boldness
      edge.GetComponent<LineRenderer>().sortingOrder = 1;
      edge.GetComponent<LineRenderer>().startWidth = 5.1f;
      edge.GetComponent<LineRenderer>().endWidth = 5.1f;
    }
  }

  public void ActivateQuestionBalloon(int state, int id, string questionAnswer)
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
    Sprite questionSprite = Resources.Load<Sprite>("Questions/" + questionImageName);
    Debug.Log("Load image from " + "Questions/" + questionImageName);
    // Set the loaded image to the "QuestionImage" object using SpriteRenderer
    questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
    GameController.GetComponent<GameController>().SetCurrentProperties(id, state, isGameNode, questionAnswer);
    GameController.GetComponent<GameController>().currentIsSentCorrectAnswer = false;
  }
  public void ActivateMiniGameBalloon(int state, int id, string questionAnswer)
  {

    // Change the image of the balloon child "QuestionImage" to /Assets/Resources/Questions/Qxx.png
    GameObject questionImageObj = QuestionBalloon.transform.Find("QuestionImage").gameObject;
    if (state == 2)
    {
      QuestionBalloon.SetActive(true);
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "";
      Sprite questionSprite = Resources.Load<Sprite>("Questions/MiniGameCleared");
      questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
      GameController.GetComponent<GameController>().SetCurrentProperties(id, state, isGameNode, questionAnswer);
      GameController.GetComponent<GameController>().currentIsSentCorrectAnswer = false;
    }
    else
    {
      GameController.GetComponent<GameController>().ActivateMinigameWindow(id);
      // Add the question text below the image.
    }
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
  public void ActivateAnimation()
  {
    //run the animation with trigger
    gameObject.SetActive(true);
    gameObject.GetComponent<Animator>().SetTrigger("ActivateNode");
  }
  public int GetState()
  {
    return state;
  }
  public void SetTouchable(bool touchable)
  {
    isTouchable = touchable;
  }
  public bool GetTouchable()
  {
    return isTouchable;
  }
}
