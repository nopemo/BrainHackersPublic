using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
// public class NodeProperties
// {
//     int isGameNode = -1;
//     int state = -1;
//     int id = -1;
//     string answer = "It has not been set yet";
//     void NodeProperties(int _isGameNode, int _state, int _id, string _answer)
//     {
//       this.isGameNode=_isGameNode;
//       this.state=_state;
//       this.id=_id;
//       this.answer=_answer;
//     }
//     void Reset()
//     {
//       isGameNode = -1;
//       state = -1;
//       id = -1;
//       string answer = "It has not been set yet";
//     }
//     void Set(int _isGameNode, int _state, int _id, string _answer)
//     {
//       isGameNode=_isGameNode;
//       state=_state;
//       id=_id;
//       answer=_answer;
//     }
// }

public class GameController : MonoBehaviour
{
  int currentId = -1;
  int currentState = -1;
  int currentIsGameNode = -1;

  string startTime;
  string currentAnswer = "It has not been set yet.";

  [SerializeField] GameObject questionBalloon;
  [SerializeField] GameObject startNode;
  [SerializeField] GameObject CircleTimer;
  [SerializeField] List<GameObject> inputWordsSpaces;
  List<int> listOfClearedGameNodeIds=new List<int>();
  List<int> boothIds = new List<int>();
  // Dictionary<int,NodeProperties> nodeNormStates =new Dictionary<int,NodeProperties>

  void Awake()
  {
    boothIds=ShuffleBoothIds(4);
  }
  void Start()
  {
    SetCurrentProperties(-1,-1,-1,"it has not been set.");
    startTime=DateTime.Now.ToLongTimeString();
    startNode.GetComponent<Node>().ChangeState(2);
  }
  void Update()
  {
    if (DateTime.Now.ToLongTimeString-startTime<40*60*60)
    {
      CloseMainGame();
    }
    CircleTimer.GetComponent<CircleTimer>.UpdateTime(DateTime.Now.ToLongTimeString-startTime);
  }
  public void SetCurrentProperties(int id, int state, int isGameNode, string answer)
  {
    currentId = id;
    currentState = state;
    currentIsGameNode = isGameNode;
    currentAnswer = answer;
  }
  public void CheckAnswer(string inputtext)
  {
    if (currentState==1)
    {
    if (currentIsGameNode == 1)
    {
      if (inputtext == currentAnswer)
      {
        Debug.Log("Correct! id:" + currentId.ToString() + " state:" + currentState.ToString());
        GameObject.Find("NodeGame (" + currentId.ToString() + ")").GetComponent<Node>().ChangeState(2);
        ClearMiniGame(currentId);
        SetCurrentProperties(-1, -1, -1, "It has not been set yet.");
      }
      else
      {
        Debug.Log("Wrong! id:" + currentId.ToString() + " state:" + currentState.ToString());
      }
    }
    else if (currentIsGameNode == 0)
    {
      if (inputtext == currentAnswer && currentState==1)
      {
        Debug.Log("Correct! id:" + currentId.ToString() + " state:" + currentState.ToString());
        GameObject.Find("NodeNorm (" + currentId.ToString() + ")").GetComponent<Node>().ChangeState(2);
        questionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "たしかに!";
        SetCurrentProperties(-1, -1, -1, "It has not been set yet.");
      }
      else if(inputtext != currentAnswer && currentState==1)
      {
        Debug.Log("Wrong! id:" + currentId.ToString() + " state:" + currentState.ToString());
        questionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "違う気がする...";
      }
    }
    }
  }
  public List<int> GetBoothIds()
  {
    return boothIds;
  }

  void ClearMiniGame(int id)
  {
    if(!(listOfClearedGameNodeIds.Contains(id)))
    {
      listOfClearedGameNodeIds.Add(id);
    }
    ActivateInputWordsSpaces();
    DeleteObstacleBalloons(id);
  }
  List<int> ShuffleBoothIds(int numOfBooths)
  {
    List<int> _boothIds = new List<int>();

        //shoufle the boothIds from 1 to 4 randomly
    for (int _i = 1; i <= numOfBooths; _i++)
    {
      _boothIds.Add(_i);
    }

    // Shuffle the boothIds list
    for (int _i = 0; _i < boothIds.Count; _i++)
    {
      int temp = boothIds[_i];
      int randomIndex = UnityEngine.Random.Range(_i, _boothIds.Count);
      _boothIds[_i] = boothIds[randomIndex];
      _boothIds[randomIndex] = temp;
    }

    return _boothIds;
  }
  void ActivateInputWordsSpaces()
  {
    for(int _i; (_i<listOfClearedGameNodeIds.Count) && (_i<InputWordsSpaces.Count); _i++)
    {
      InputWordsSpaces[i].SetActive(true);
    }
  }

  void DeleteObstacleBalloons(int _id)
  {
    List<GameObject> _targets;
    if (_id>90)
    {
      _targets=GameObject.Find("NodeGame (" + currentId.ToString() + ")").GetComponent<Node>.deleteObstacleBalloons;
    }
    else
    {
      _targets=GameObject.Find("NodeNorm (" + currentId.ToString() + ")").GetComponent<Node>.deleteObstacleBalloons;
    }
    foreach(GameObject _target in _targets)
    {
      _target.SetActive(false);
    }
  }
  void CloseMainGame()
  {
    Debug.Log("WRITE ME GameController/CloseMainGame");
    //Change Scene
    //Save the current properties
    //Prepare for the result scene
  }
}
