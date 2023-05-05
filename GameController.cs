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
  bool isExOpenned = false;
  bool isActiveStartNode = false;
  public bool currentIsSentCorrectAnswer = false;
  DateTime startTime;
  string currentAnswer = "It has not been set yet.";

  [SerializeField] int TeamNumber = 0;
  [SerializeField] GameObject questionBalloon;
  [SerializeField] GameObject startNode;
  [SerializeField] GameObject CircleTimer;
  [SerializeField] List<GameObject> inputWordsSpaces;
  [SerializeField] GameObject firstInputWordSpace;
  [SerializeField] float delayOfObstacleBalloonDelete = 0.4f;
  // [SerializeField] float delayOfNodeExActivate = 0.4f;
  [SerializeField] GameObject motosensimeta;
  [SerializeField] GameObject sutaato;
  [SerializeField] GameObject teamNumberText;
  [SerializeField] GameObject insideBrain;
  [SerializeField] List<GameObject> nodeExs;
  [SerializeField] GameObject nodeParent;
  [SerializeField] GameObject finalQButton;
  [SerializeField] List<GameObject> minigameWindows;
  [SerializeField] List<GameObject> irrelevantBalloons;
  [SerializeField] GameObject otherWindow;
  List<int> listOfClearedGameNodeIds = new List<int>();
  List<int> boothIds = new List<int>();
  Dictionary<string, int> answerToId = new Dictionary<string, int>();
  // Dictionary<int,NodeProperties> nodeNormStates =new Dictionary<int,NodeProperties>
  void Start()
  {
    SetCurrentProperties(-1, -1, -1, "it has not been set.");
    currentIsSentCorrectAnswer = false;
    startTime = DateTime.Now;
    teamNumberText.GetComponent<TextMeshProUGUI>().text = TeamNumber.ToString("D2");
    TeamNumber = Property.Instance.GetNumber("TeamNumber");
    Property.Instance.SetFlag("IsGameCleared", false);
    foreach (GameObject wordSpace in inputWordsSpaces)
    {
      wordSpace.SetActive(false);
    }
  }
  void Update()
  {
    if (!isActiveStartNode)
    {
      startNode.GetComponent<Node>().ChangeState(2);
      isActiveStartNode = true;
    }
    double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;

    if (elapsedSeconds >= 40 * 60)
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
    if (inputtext == "ホーキング")
    {
      Property.Instance.SetFlag("IsGameCleared", true);
      if (questionBalloon.activeSelf)
      {
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
      }
      //WRITE ME
      //Cleared text
      SetCurrentProperties(-1, -1, -1, "ホーキング");
      currentIsSentCorrectAnswer = false;
    }
    if (inputtext == "イチニサンゴ")
    {
      if (questionBalloon.activeSelf)
      {
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
      }
      //WRITE ME
      //Cleared text
      SetCurrentProperties(-1, -1, -1, "イチニサンゴ");
      currentIsSentCorrectAnswer = false;
      finalQButton.GetComponent<FinalQButton>().SetState(2);
    }
    if (inputtext == "モトセンシメタ")
    {
      if (questionBalloon.activeSelf)
      {
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
      }
      motosensimeta.GetComponent<ObstacleBalloon>().DisactivateAnimation();

    }
    if (inputtext == "スタート")
    {
      firstInputWordSpace.SetActive(false);
      ActivateInputWordsSpaces();
      sutaato.GetComponent<ObstacleBalloon>().DisactivateAnimation();

    }
    if (answerToId.ContainsKey(inputtext))
    {
      if (!isExOpenned && answerToId[inputtext] > 40)
      {
        return;
      }
      if (currentId == answerToId[inputtext] && questionBalloon.activeSelf)
      {
        if (currentIsGameNode == 0 && currentState < 2)
        {
          questionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "たしかに!";
          currentIsSentCorrectAnswer = true;
        }
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
        CalcTotalNumOfClearedNodes();
        return;
      }
      GameObject _tmpnode = GameObject.Find("NodeNorm (" + answerToId[inputtext].ToString() + ")");
      if (_tmpnode.GetComponent<Node>().GetState() == 2)
      {
        return;
      }
      SetCurrentProperties(answerToId[inputtext], 1, 1, inputtext);
      PlayAnimations();
      _tmpnode.GetComponent<Node>().ChangeState(2);
    }
    CalcTotalNumOfClearedNodes();
  }

  public void ClearMiniGame(int id)
  {
    if (!(listOfClearedGameNodeIds.Contains(id)))
    {
      listOfClearedGameNodeIds.Add(id);
    }
    ActivateInputWordsSpaces();
    if (listOfClearedGameNodeIds.Count == 4)
    {
      DeleteIrrelevantBalloons();
    }
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
  public void NodeExActivateAnimations()
  {
    List<GameObject> _allNodes = new List<GameObject>();
    //find the children of the nodeParent
    foreach (Transform _child in nodeParent.transform)
    {
      _allNodes.Add(_child.gameObject);
    }

    foreach (GameObject _targetExNode in nodeExs)
    {
      _targetExNode.SetActive(true);
    }
    foreach (GameObject _targetNode in _allNodes)
    {
      _targetNode.GetComponent<Node>().ChangeState(_targetNode.GetComponent<Node>().state);
    }
  }
  IEnumerator PlayAnimationWithDelay(GameObject _targetObstacleBalloon, float delay)
  {
    yield return new WaitForSeconds(delay);
    _targetObstacleBalloon.GetComponent<ObstacleBalloon>().DisactivateAnimation();
  }
  // IEnumerator NodeActivateAnimationWithDelay(GameObject _targetExNode, float delay)
  // {
  //   yield return new WaitForSeconds(delay);
  //   _targetExNode.GetComponent<Node>().ActivateAnimation();
  // }
  public void SetAnswerToIdDictionary(string _answer, int _id)
  {
    if (!answerToId.ContainsKey(_answer))
    {
      answerToId.Add(_answer, _id);
    }
  }
  public void CalcTotalNumOfClearedNodes()
  {
    Debug.Log("CalcTotalNumOfClearedNodes has been called");
    int _totalNumOfClearedNodes = 0;
    foreach (Transform _nodeTransform in nodeParent.transform)
    {
      if (_nodeTransform.gameObject.GetComponent<Node>().state == 2)
      {
        _totalNumOfClearedNodes++;
      }
    }
    Debug.Log("_totalNumOfClearedNodes: " + _totalNumOfClearedNodes.ToString() + " isExOpenned: " + isExOpenned.ToString() + " isGameCleared: " + Property.Instance.GetFlag("IsGameCleared").ToString());
    if (_totalNumOfClearedNodes >= 1)
    {
      if (!isExOpenned)
      {
        if (Property.Instance.GetFlag("IsGameCleared"))
        {
          //Run the Animation Trigger "ExpandTrigger"
          isExOpenned = true;
          insideBrain.GetComponent<Animator>().SetTrigger("ExpandTrigger");
          //Run NodeExActivateAnimations() after the insideBrain animation is finished
          //The animation is 0:30 seconds long
          StartCoroutine(NodeExActivateAnimationsAfterDelay(1.0f));
        }
      }
    }
  }
  IEnumerator NodeExActivateAnimationsAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    NodeExActivateAnimations();
    yield return new WaitForSeconds(delay);
    ActivateOtherWindow("Cleared40NormNodes");
  }
  public void ActivateMinigameWindow(int _id)
  {
    int _current_id = (TeamNumber + _id) % 4;
    minigameWindows[_current_id].SetActive(true);
    minigameWindows[_current_id].GetComponent<GameWindow>().SetGamenode(_id);
  }
  void DeleteIrrelevantBalloons()
  {
    //delete balloon in irrelevantBalloons like PlayAnimation()
    float delay = delayOfObstacleBalloonDelete;
    foreach (GameObject _targetObstacleBalloon in irrelevantBalloons)
    {
      Debug.Log(_targetObstacleBalloon.name + " is playing animation");
      if (_targetObstacleBalloon == null || !_targetObstacleBalloon.activeSelf)
        continue;

      StartCoroutine(PlayAnimationWhenIrrelevantWithDelay(_targetObstacleBalloon, delay));
      delay += delayOfObstacleBalloonDelete;
    }
    StartCoroutine(ShowWindowWithDelay("Cleared4MiniGames", delay + 1.0f));
  }
  IEnumerator PlayAnimationWhenIrrelevantWithDelay(GameObject _targetObstacleBalloon, float delay)
  {
    yield return new WaitForSeconds(delay);
    _targetObstacleBalloon.GetComponent<ObstacleBalloon>().DisactivateAnimationAfterAllMiniGamesCleared();
  }
  IEnumerator ShowWindowWithDelay(string _mode, float delay)
  {
    yield return new WaitForSeconds(delay);
    ActivateOtherWindow(_mode);
  }
  public void ActivateOtherWindow(string mode)
  {
    Sprite _sprite = Resources.Load<Sprite>($"OtherWindow/{mode}");
    otherWindow.SetActive(true);
  }
}
