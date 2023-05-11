using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
  int boothNumber = 0;
  bool isExOpenned = false;
  bool isActiveStartNode = false;
  bool isMotosenshimeta = false;
  bool isCalledCloseGame = false;
  bool isAllNodesCleared = false;
  public bool currentIsSentCorrectAnswer = false;
  DateTime startTime;
  string currentAnswer = "It has not been set yet.";

  [SerializeField] int TeamNumber = 0;
  [SerializeField] GameObject questionBalloon;
  [SerializeField] GameObject startNode;
  [SerializeField] GameObject TimerObject;
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
  [SerializeField] GameObject obstacleBalloonParent;
  [SerializeField] int totalTime = 2400;
  List<int> listOfClearedGameNodeIds = new List<int>();
  List<int> boothIds = new List<int>();
  Dictionary<string, int> answerToId = new Dictionary<string, int>();
  // Dictionary<int,NodeProperties> nodeNormStates =new Dictionary<int,NodeProperties>

  void Awake()
  {
    Property.Instance.SetNumber("TotalTime", totalTime);
    Property.Instance.SetFlag("IsTutorial", SceneManager.GetActiveScene().name == "Tutorial");
    Debug.Log("This scene name is " + SceneManager.GetActiveScene().name);
    Debug.Log("IsTutorial is " + Property.Instance.GetFlag("IsTutorial"));
  }
  void Start()
  {
    SetCurrentProperties(-1, -1, -1, "it has not been set.");
    currentIsSentCorrectAnswer = false;
    isAllNodesCleared = false;
    isMotosenshimeta = false;
    startTime = DateTime.Now;
    teamNumberText.GetComponent<TextMeshProUGUI>().text = TeamNumber.ToString("D2");
    TeamNumber = Property.Instance.GetNumber("TeamNumber");
    boothNumber = Property.Instance.GetNumber("BoothNumber");
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

    if (elapsedSeconds >= totalTime && !isCalledCloseGame)
    {
      CloseMainGame();
      isCalledCloseGame = true;
    }

    TimerObject.GetComponent<BarTimer>().UpdateTime(elapsedSeconds);
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
    if (inputtext == "ニュートン")
    {
      ActivateOtherWindow("ClearFinalQ");
      Property.Instance.SetFlag("IsGameCleared", true);
      if (questionBalloon.activeSelf)
      {
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
      }
      //WRITE ME
      //Cleared text
      SetCurrentProperties(-1, -1, -1, "ニュートン");
      float _tmpdelay = 0.0f;
      foreach (Transform _tmpObstacleTransform in obstacleBalloonParent.transform.GetComponentsInChildren<Transform>())
      {
        GameObject _tmpObstacle = _tmpObstacleTransform.gameObject;
        if (_tmpObstacle.activeSelf)
        {
          StartCoroutine(PlayAnimationWithDelay(_tmpObstacle, _tmpdelay));
          _tmpdelay += delayOfObstacleBalloonDelete;
        }
      }
      currentIsSentCorrectAnswer = false;
      return;
    }
    if (inputtext == "イチイチゴ")
    {
      if (questionBalloon.activeSelf)
      {
        questionBalloon.transform.Find("DisactivateQuestion").gameObject.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
      }
      //WRITE ME
      //Cleared text
      SetCurrentProperties(-1, -1, -1, "イチイチゴ");
      currentIsSentCorrectAnswer = false;
      float _tmpdelay = 0.0f;
      foreach (Transform _tmpObstacleTransform in obstacleBalloonParent.transform.GetComponentsInChildren<Transform>())
      {
        GameObject _tmpObstacle = _tmpObstacleTransform.gameObject;
        if (_tmpObstacle.activeSelf && _tmpObstacle.GetComponent<ObstacleBalloon>() != null)
        {
          StartCoroutine(PlayAnimationWithDelay(_tmpObstacle, _tmpdelay));
          _tmpdelay += delayOfObstacleBalloonDelete;
        }
      }
      finalQButton.GetComponent<FinalQButton>().SetState(2);
    }
    if (inputtext == "セカイイッシュウ")
    {
      isMotosenshimeta = true;
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
    Debug.Log("CloseMainGame has been called and IsTutorial is " + Property.Instance.GetFlag("IsTutorial").ToString());
    if (SceneManager.GetActiveScene().name == "Tutorial")
    {
      Property.Instance.SetFlag("IsTutorial", false);
      GameObject.Find("TutorialManager").GetComponent<TutorialManager>().ForcelyMoveToFinalSection();
    }
    else
    {
      SaveControllerStateToProperty();
      ActivateOtherWindow("TimeOver");
      StartCoroutine(TimeOverSceneLoad());
    }
  }
  IEnumerator TimeOverSceneLoad()
  {
    yield return new WaitForSeconds(3.0f);
    Initiate.Fade("AfterMainGame", Color.black, 1.0f);
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
    if (_targetObstacleBalloon != null && _targetObstacleBalloon.activeSelf && _targetObstacleBalloon.GetComponent<ObstacleBalloon>() != null)
    {
      _targetObstacleBalloon.GetComponent<ObstacleBalloon>().DisactivateAnimation(); //this is line 304
    }
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
    if (_totalNumOfClearedNodes >= 45)
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
    if (_totalNumOfClearedNodes >= 55 && isAllNodesCleared == false)
    {
      isAllNodesCleared = true;
      ActivateOtherWindow("ClearedAllNodes");
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
    int _current_id = (boothNumber + _id) % 4;
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
      if (_targetObstacleBalloon == null || !_targetObstacleBalloon.activeSelf || _targetObstacleBalloon.GetComponent<ObstacleBalloon>() == null)
        continue;
      StartCoroutine(PlayAnimationWhenIrrelevantWithDelay(_targetObstacleBalloon, delay));
      delay += delayOfObstacleBalloonDelete;
    }
    if (!Property.Instance.GetFlag("IsGameCleared"))
    {
      StartCoroutine(ShowWindowWithDelay("Cleared4MiniGames", delay + 1.0f));
    }
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
    otherWindow.transform.GetChild(0).GetComponent<Image>().sprite = _sprite;
    otherWindow.SetActive(true);
    if (mode == "TimeOver")
    {
      otherWindow.transform.GetChild(1).gameObject.SetActive(false);
    }
  }
  void SaveControllerStateToProperty()
  {
    Property.Instance.SetFlag("IsExOpenned", isExOpenned);
    Property.Instance.SetFlag("IsMotosenshimeta", isMotosenshimeta);
    Property.Instance.SetNumber("TeamNumber", TeamNumber);
  }
  void LoadControllerStateFromProperty()
  {
    isExOpenned = Property.Instance.GetFlag("IsExOpenned");
    TeamNumber = Property.Instance.GetNumber("TeamNumber");
    foreach (Transform _nodeTransform in nodeParent.transform)
    {
      string _tmpname;
      if (_nodeTransform.gameObject.GetComponent<Node>().isGameNode == 1)
      {
        _tmpname = "Game" + _nodeTransform.gameObject.GetComponent<Node>().id.ToString();
      }
      else
      {
        _tmpname = "Norm" + _nodeTransform.gameObject.GetComponent<Node>().id.ToString();
      }
      if (Property.Instance.GetFlag(_tmpname))
      {
        _nodeTransform.gameObject.GetComponent<Node>().ChangeState(2);
        foreach (GameObject _obstacleBalloon in _nodeTransform.gameObject.GetComponent<Node>().deleteObstacleBalloons)
        {
          _obstacleBalloon.SetActive(false);
        }
      }
    }
  }
  public void AddRemainingTime(int _inttime)
  {
    //startTime is DateTime type
    //Add _time to startTime
    float _time = (float)_inttime;
    Debug.Log("current start time: " + startTime.ToString() + " _time: " + _time.ToString());
    startTime = startTime.AddSeconds(_time);
    Debug.Log("new start time: " + startTime.ToString());
  }
  public float GetElipsedTime()
  {
    return 1 + totalTime + (float)(startTime - DateTime.Now).TotalSeconds;
  }
}
