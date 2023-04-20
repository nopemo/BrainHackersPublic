using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using TMPro;
public class GameController : MonoBehaviour
{
  int currentId = -1;
  int currentState = -1;
  int currentIsGameNode = -1;
  public bool currentIsSentCorrectAnswer = false;

  DateTime startTime;
  string currentAnswer = "It has not been set yet.";

  [SerializeField] GameObject questionBalloon;
  [SerializeField] GameObject startNode;
  [SerializeField] GameObject CircleTimer;
  [SerializeField] List<GameObject> inputWordsSpaces;
  [SerializeField] float delayOfObstacleBalloonDelete = 0.4f;
  List<int> listOfClearedGameNodeIds = new List<int>();
  List<int> boothIds = new List<int>();
  // Dictionary<int,NodeProperties> nodeNormStates =new Dictionary<int,NodeProperties>

  void Awake()
  {
    boothIds = ShuffleBoothIds(4);
  }
  void Start()
  {
    SetCurrentProperties(-1, -1, -1, "it has not been set.");
    currentIsSentCorrectAnswer = false;
    startTime = DateTime.Now;
    startNode.GetComponent<Node>().ChangeState(2);
    ActivateInputWordsSpaces();
  }
  void Update()
  {
    double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;

    if (elapsedSeconds >= 40)
    {
      CloseMainGame();
    }

    CircleTimer.GetComponent<CircleTimer>().UpdateTime(elapsedSeconds);
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
    if (currentState == 1 && !currentIsSentCorrectAnswer)
    {
      if (currentIsGameNode == 1)
      {
        if (inputtext == currentAnswer)
        {
          Debug.Log("Correct! id:" + currentId.ToString() + " state:" + currentState.ToString());
          ClearMiniGame(currentId);
          currentIsSentCorrectAnswer = true;
        }
        else
        {
          Debug.Log("Wrong! id:" + currentId.ToString() + " state:" + currentState.ToString());
        }
      }
      else if (currentIsGameNode == 0)
      {
        if (inputtext == currentAnswer && currentState == 1)
        {
          Debug.Log("Correct! id:" + currentId.ToString() + " state:" + currentState.ToString());
          questionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "たしかに!";
          currentIsSentCorrectAnswer = true;
        }
        else if (inputtext != currentAnswer && currentState == 1)
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
    if (!(listOfClearedGameNodeIds.Contains(id)))
    {
      listOfClearedGameNodeIds.Add(id);
    }
    ActivateInputWordsSpaces();
    // DeleteObstacleBalloons(id);
  }
  List<int> ShuffleBoothIds(int numOfBooths)
  {
    List<int> _boothIds = new List<int>();

    //shoufle the boothIds from 1 to 4 randomly
    for (int _i = 1; _i <= numOfBooths; _i++)
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
    for (int _i = 0; _i < inputWordsSpaces.Count; _i++)
    {
      if (_i < listOfClearedGameNodeIds.Count + 1)
      {
        inputWordsSpaces[_i].SetActive(true);
      }
      else
      {
        inputWordsSpaces[_i].SetActive(false);
      }
    }
  }

  void DeleteObstacleBalloons(int _id)
  {
    List<GameObject> _targets;
    if (_id > 90)
    {
      _targets = GameObject.Find("NodeGame (" + currentId.ToString() + ")").GetComponent<Node>().deleteObstacleBalloons;
    }
    else
    {
      _targets = GameObject.Find("NodeNorm (" + currentId.ToString() + ")").GetComponent<Node>().deleteObstacleBalloons;
    }
    foreach (GameObject _target in _targets)
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
  public void PlayAnimations()
  {
    GameObject _targetNode = GameObject.Find($"Node{(currentId > 90 ? "Game" : "Norm")} ({currentId})");
    _targetNode.GetComponent<Node>().ChangeState(2);

    float delay = delayOfObstacleBalloonDelete;
    bool _isFirst = true;
    foreach (GameObject _targetObstacleBalloon in _targetNode.GetComponent<Node>().deleteObstacleBalloons)
    {
      Debug.Log(_targetObstacleBalloon.name + " is playing animation");
      if (_targetObstacleBalloon == null || !_targetObstacleBalloon.activeSelf)
        continue;
      if (_isFirst)
      {
        _targetObstacleBalloon.GetComponent<ObstacleBalloon>().DisactivateAnimation();
        _isFirst = false;
      }
      else
      {
        StartCoroutine(PlayAnimationWithDelay(_targetObstacleBalloon, delay));
        delay += delayOfObstacleBalloonDelete;
      }
    }
  }
  IEnumerator PlayAnimationWithDelay(GameObject _targetObstacleBalloon, float delay)
  {
    yield return new WaitForSeconds(delay);
    _targetObstacleBalloon.GetComponent<ObstacleBalloon>().DisactivateAnimation();
  }
}
