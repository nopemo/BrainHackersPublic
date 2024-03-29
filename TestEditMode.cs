using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//add SceneManagement to use SceneManager
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Text;

[ExecuteInEditMode]
public class TestEditMode : MonoBehaviour

{
  [SerializeField] int[] limitDistance;
  [SerializeField] List<int> targetNodes;
  [SerializeField] GameObject nodeParent;
  [SerializeField] string gameMode = "";
  void Start()
  {
    Debug.Log("StartTestEditMode");
    //search the objects with tag "ButtonWord"
  }

  void Update()
  {
    Debug.Log("Test01");
    //find the object whose name starts with "Fukidashi"
    GameObject[] fukidashi = GameObject.FindGameObjectsWithTag("Fukidashi");
    // GameObject.Find("Clock").transform.position = new Vector3(0, 0, 0);
    Debug.Log("fukidashi.Length: " + fukidashi.Length);

    // for (int i = 0; i < fukidashi.Length; i++)
    // {
    //   //change the order in layer of the fukidashi
    //   //fukidashi has TextMeshProUGUI component and it has canvas component
    //   //change the sorting layer and order in layer of the text and the fukidashi

    //   //find the child text in the hierarchy
    //   fukidashi[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * i + 1;
    //   fukidashi[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 2 * i + 2;
    //   // fukidashi[i].transform.Find("Balloon").GetComponent<SpriteRenderer>().sortingOrder = 2 * i + 1;
    //   // GameObject _tmp = fukidashi[i].transform.Find("Balloon").gameObject;
    //   // _tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 2 * i + 2;
    //   // _tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingLayerName = "DeleteObstacleBalloon";
    // }
    InitializeTextOfInputButton();

    foreach (Transform nodeTrans in nodeParent.transform)
    {
      nodeTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text = nodeTrans.GetComponent<Node>().id.ToString();
      nodeTrans.GetChild(0).GetComponent<Canvas>().sortingLayerName = "Node";
    }
  }

  void OnGUI()
  {
    Debug.Log("OnGUI");
  }

  void OnRenderObject()
  {
    Debug.Log("OnRenderObject");
    InitializeObstacleBalloon();
  }
  public void DrowInitialEdges()
  {
    string filePath = Path.Combine(Application.dataPath, "edges" + gameMode + ".csv");

    // 既存のCSVファイルが存在するか確認
    if (File.Exists(filePath))
    {
      // ファイルを削除
      File.Delete(filePath);
    }

    string _tmpName;
    GameObject Edge = Resources.Load("Edge") as GameObject;
    // List<GameObject> nodeNorms = new List<GameObject>();
    // foreach (GameObject nodeNorm in GameObject.FindGameObjectsWithTag("NodeNorm"))
    // {
    //   nodeNorms.Add(nodeNorm);
    // }
    List<GameObject> nodeNorms = new List<GameObject>();
    //find the children of NodeParent
    foreach (Transform child in nodeParent.transform)
    {
      //if active
      if (child.gameObject.activeSelf)
      {
        nodeNorms.Add(child.gameObject);
      }
    }

    StringBuilder csvData = new StringBuilder();
    csvData.AppendLine("NodeA_ID,NodeA_IsGameNode,NodeB_ID,NodeB_IsGameNode");

    foreach (GameObject nodeNorm in nodeNorms)
    {
      foreach (GameObject nearNode in nodeNorm.GetComponent<Node>().nearNodes)
      {
        Debug.Log("nodeNorm: " + nodeNorm.GetComponent<Node>().id + ", nearNode: " + nearNode.GetComponent<Node>().id);
        csvData.AppendLine($"{nodeNorm.GetComponent<Node>().id},{nodeNorm.GetComponent<Node>().isGameNode == 1},{nearNode.GetComponent<Node>().id},{nearNode.GetComponent<Node>().isGameNode == 1}");
        GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity, GameObject.Find("Edges").transform);
        line.GetComponent<Renderer>().sortingLayerName = "Edge";
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, nodeNorm.transform.position);
        lineRenderer.SetPosition(1, nearNode.transform.position);
        _tmpName = "Edge (" + nodeNorm.GetComponent<Node>().id.ToString("00") + "," + nearNode.GetComponent<Node>().id.ToString("00") + ")";
        if (GameObject.Find(_tmpName))
        {
          DestroyImmediate(GameObject.Find(_tmpName));
        }
        line.name = _tmpName;
        line.tag = "Edge";
      }
    }

    List<GameObject> nodeGames = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeGame"));
    foreach (GameObject nodeGame in nodeGames)
    {
      foreach (GameObject nearNode in nodeGame.GetComponent<Node>().nearNodes)
      {
        csvData.AppendLine($"{nodeGame.GetComponent<Node>().id},{nodeGame.GetComponent<Node>().isGameNode == 1},{nearNode.GetComponent<Node>().id},{nearNode.GetComponent<Node>().isGameNode == 1}");
        GameObject line = Instantiate(Edge, Vector3.zero, Quaternion.identity, GameObject.Find("Edges").transform);
        line.GetComponent<Renderer>().sortingLayerName = "Edge";
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, nodeGame.transform.position);
        lineRenderer.SetPosition(1, nearNode.transform.position);
        _tmpName = "Edge (" + nodeGame.GetComponent<Node>().id.ToString("00") + "," + nearNode.GetComponent<Node>().id.ToString("00") + ")";
        if (GameObject.Find(_tmpName))
        {
          DestroyImmediate(GameObject.Find(_tmpName));
        }
        line.name = _tmpName;
        line.tag = "Edge";
      }
    }
    File.WriteAllText(filePath, csvData.ToString());
  }
  public void DeleteCurrentEdges()
  {
    GameObject[] edges = GameObject.FindGameObjectsWithTag("Edge");
    for (int i = 0; i < edges.Length; i++)
    {
      DestroyImmediate(edges[i]);
    }
  }
  private Vector3 StringToVector3(string str)
  {
    string[] vectorComponents = str.Split(';');
    return new Vector3(float.Parse(vectorComponents[0]), float.Parse(vectorComponents[1]), float.Parse(vectorComponents[2]));
  }

  private Dictionary<int, (int id, Vector3 position, bool isGameNode, int questionImageId, string questionImageName, string questionAnswer)> ReadNodeDataFromCsv(string filePath)
  {
    Dictionary<int, (int id, Vector3 position, bool isGameNode, int questionImageId, string questionImageName, string questionAnswer)> nodeData = new Dictionary<int, (int id, Vector3 position, bool isGameNode, int questionImageId, string questionImageName, string questionAnswer)>();

    using (var reader = new StreamReader(filePath, Encoding.UTF8))
    {
      reader.ReadLine(); // ヘッダー行をスキップ

      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        var values = line.Split(',');

        int nodeId = int.Parse(values[0]);
        Vector3 position = StringToVector3(values[1]);
        bool isGameNode = bool.Parse(values[2]);
        int questionImageId = int.Parse(values[3]);
        string questionImageName = values[4];
        string questionAnswer = values[5];

        nodeData.Add(nodeId, (nodeId, position, isGameNode, questionImageId, questionImageName, questionAnswer));
      }
    }

    return nodeData;
  }
  public void UpdateObjectsAndNearNodes(string _gamemode)
  {
    string edgeFilePath = Path.Combine(Application.dataPath, "edges" + _gamemode + ".csv");
    string nodeDataFilePath = Path.Combine(Application.dataPath, "node_data" + _gamemode + ".csv");

    Dictionary<int, (int id, Vector3 position, bool isGameNode, int questionImageId, string questionImageName, string questionAnswer)> nodeData = ReadNodeDataFromCsv(nodeDataFilePath);
    List<(int nodeAId, bool nodeAIsGameNode, int nodeBId, bool nodeBIsGameNode)> edgeData = ReadEdgeDataFromCsv(edgeFilePath);
    List<GameObject> allchildrennodes = new List<GameObject>();
    foreach (Transform child in nodeParent.transform)
    {
      //if active
      if (child.gameObject.activeSelf)
      {
        allchildrennodes.Add(child.gameObject);
      }
    }
    //find objects whose name starts with "NodeNorm" or "NodeGame"
    //if it contains "NodeNorm", add to the list "nodeObjectsList"
    //if it contains "NodeGame", add to the list "gameNodeObjectsList"
    //Dont use "FindGameObjectsWithTag" because it has bugs
    List<GameObject> nodeObjectsList = new List<GameObject>();
    List<GameObject> gameNodeObjectsList = new List<GameObject>();
    foreach (GameObject node in allchildrennodes)
    {
      if (node.name.Contains("NodeNorm"))
      {
        nodeObjectsList.Add(node);
      }
      else if (node.name.Contains("NodeGame"))
      {
        gameNodeObjectsList.Add(node);
      }
    }
    //convert the list to array
    GameObject[] nodeObjects = nodeObjectsList.ToArray();
    GameObject[] gameNodeObjects = gameNodeObjectsList.ToArray();


    // オブジェクトの座標を更新
    foreach (KeyValuePair<int, (int id, Vector3 position, bool isGameNode, int questionImageId, string questionImageName, string questionAnswer)> nodeEntry in nodeData)
    {
      GameObject nodeToUpdate = null;
      if (nodeEntry.Value.isGameNode)
      {
        nodeToUpdate = GameObject.Find($"NodeGame ({nodeEntry.Key})");
      }
      else
      {
        nodeToUpdate = GameObject.Find($"NodeNorm ({nodeEntry.Key})");
      }
      if (nodeToUpdate != null)
      {
        nodeToUpdate.transform.position = nodeEntry.Value.position;
        Node nodeComponent = nodeToUpdate.GetComponent<Node>();
        nodeComponent.id = nodeEntry.Value.id;
        nodeComponent.isGameNode = nodeEntry.Value.isGameNode ? 1 : 0;
        nodeComponent.questionImageId = nodeEntry.Value.questionImageId;
        nodeComponent.questionImageName = nodeEntry.Value.questionImageName;
        nodeComponent.questionAnswer = nodeEntry.Value.questionAnswer;
        Debug.Log($"Updated node {nodeEntry.Key}");
      }
    }

    // nearNodesリストを新しく作り直して更新
    foreach (GameObject nodeObject in allchildrennodes)
    {
      Node nodeComponent = nodeObject.GetComponent<Node>();
      nodeComponent.nearNodes = new List<GameObject>();
    }
    foreach ((int nodeAId, bool nodeAIsGameNode, int nodeBId, bool nodeBIsGameNode) in edgeData)
    {
      GameObject nodeA;
      GameObject nodeB;
      if (nodeAIsGameNode)
      {
        nodeA = GameObject.Find($"NodeGame ({nodeAId})");
      }
      else
      {
        nodeA = GameObject.Find($"NodeNorm ({nodeAId})");
      }
      if (nodeBIsGameNode)
      {
        nodeB = GameObject.Find($"NodeGame ({nodeBId})");
      }
      else
      {
        nodeB = GameObject.Find($"NodeNorm ({nodeBId})");
      }
      Debug.Log("nodeA's name is " + nodeA.name + " and nodeB's name is " + nodeB.name);
      // GameObject nodeA = GameObject.Find($"Node{(nodeAIsGameNode ? "Game" : "Norm")} ({nodeAId})");
      // GameObject nodeB = GameObject.Find($"Node{(nodeBIsGameNode ? "Game" : "Norm")} ({nodeBId})");

      if (nodeA != null && nodeB != null)
      {
        Node nodeAComponent = nodeA.GetComponent<Node>();
        Node nodeBComponent = nodeB.GetComponent<Node>();

        //nearNodesリストに相互に追加
        if (!nodeAComponent.nearNodes.Contains(nodeB))
        {
          nodeAComponent.nearNodes.Add(nodeB);
        }

        if (!nodeBComponent.nearNodes.Contains(nodeA))
        {
          nodeBComponent.nearNodes.Add(nodeA);
        }
      }
    }
  }
  public void SaveNodeDataCsv()
  {
    StringBuilder csvData = new StringBuilder();
    csvData.AppendLine("Node_ID,Node_Position,Node_IsGameNode,Node_QuestionImageId,Node_QuestionImageName,Node_QuestionAnswer");

    List<GameObject> allNodes = new List<GameObject>();
    //find the children of the parent "NodeParent" in serializefield
    foreach (Transform child in nodeParent.transform)
    {
      allNodes.Add(child.gameObject);
    }

    foreach (GameObject node in allNodes)
    {
      Node nodeComponent = node.GetComponent<Node>();
      csvData.AppendLine($"{nodeComponent.id},{node.transform.position.x};{node.transform.position.y};{node.transform.position.z},{node.tag == "NodeGame"},{nodeComponent.questionImageId},{nodeComponent.questionImageName},{nodeComponent.questionAnswer}");
    }

    string filePath = Path.Combine(Application.dataPath, "node_data" + gameMode + ".csv");
    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }
    File.WriteAllText(filePath, csvData.ToString(), Encoding.UTF8);
  }

  private List<(int nodeAId, bool nodeAIsGameNode, int nodeBId, bool nodeBIsGameNode)> ReadEdgeDataFromCsv(string filePath)
  {
    List<(int nodeAId, bool nodeAIsGameNode, int nodeBId, bool nodeBIsGameNode)> edgeData = new List<(int nodeAId, bool nodeAIsGameNode, int nodeBId, bool nodeBIsGameNode)>();

    using (var reader = new StreamReader(filePath, Encoding.UTF8))
    {
      reader.ReadLine(); // ヘッダー行をスキップ

      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        var values = line.Split(',');

        int nodeAId = int.Parse(values[0]);
        bool nodeAIsGameNode = bool.Parse(values[1]);
        int nodeBId = int.Parse(values[2]);
        bool nodeBIsGameNode = bool.Parse(values[3]);

        edgeData.Add((nodeAId, nodeAIsGameNode, nodeBId, nodeBIsGameNode));
      }
    }

    return edgeData;
  }
  public void ConnectOrDisconnectTargetNodes()
  {
    if (targetNodes.Count == 2)
    {
      GameObject nodeA = GameObject.Find($"Node{(targetNodes[0] > 90 ? "Game" : "Norm")} ({targetNodes[0]})");
      GameObject nodeB = GameObject.Find($"Node{(targetNodes[1] > 90 ? "Game" : "Norm")} ({targetNodes[1]})");

      Node nodeAComponent = nodeA.GetComponent<Node>();
      Node nodeBComponent = nodeB.GetComponent<Node>();

      if (nodeAComponent.nearNodes.Contains(nodeB) || nodeBComponent.nearNodes.Contains(nodeA))
      {
        // 接続済みなら切断
        nodeAComponent.nearNodes.Remove(nodeB);
        nodeBComponent.nearNodes.Remove(nodeA);
      }
      else
      {
        // 未接続なら接続
        nodeAComponent.nearNodes.Add(nodeB);
        nodeBComponent.nearNodes.Add(nodeA);
      }
    }
  }
  public void ConfirmBidirection()
  {
    List<GameObject> allNodes = new List<GameObject>();
    //find the children of the parent "NodeParent" in serializefield
    foreach (Transform child in nodeParent.transform)
    {
      allNodes.Add(child.gameObject);
    }
    foreach (GameObject node in allNodes)
    {
      Node nodeComponent = node.GetComponent<Node>();
      foreach (GameObject nearNode in nodeComponent.nearNodes)
      {
        Node nearNodeComponent = nearNode.GetComponent<Node>();
        if (!nearNodeComponent.nearNodes.Contains(node))
        {
          nearNodeComponent.nearNodes.Add(node);
        }
      }
    }
  }
  public void DisActivateFukidashi()
  {
    foreach (int _nodeid in targetNodes)
    {
      GameObject _node = GameObject.Find($"Node{(_nodeid > 90 ? "Game" : "Norm")} ({_nodeid})");
      foreach (GameObject _deleteObstacle in _node.GetComponent<Node>().deleteObstacleBalloons)
      {
        _deleteObstacle.SetActive(false);
      }
    }
  }
  public void ActivateFukidashi()
  {
    foreach (int _nodeid in targetNodes)
    {
      GameObject _node = GameObject.Find($"Node{(_nodeid > 90 ? "Game" : "Norm")} ({_nodeid})");
      foreach (GameObject _deleteObstacle in _node.GetComponent<Node>().deleteObstacleBalloons)
      {
        _deleteObstacle.SetActive(true);
      }
    }
  }
  public void InitializeTextOfInputButton()
  {
    List<GameObject> ButtonWords = new List<GameObject>();
    GameObject[] _tmp = GameObject.FindGameObjectsWithTag("ButtonWord");
    for (int i = 0; i < _tmp.Length; i++)
    {
      ButtonWords.Add(_tmp[i]);
    }
    foreach (GameObject buttonWord in ButtonWords)
    {
      buttonWord.GetComponent<InputWord>().InitializeText();
    }

  }
  public void InitializeObstacleBalloon()
  {
    List<GameObject> ObstacleBalloons = new List<GameObject>();
    GameObject[] _tmp = GameObject.FindGameObjectsWithTag("Fukidashi");
    for (int i = 0; i < _tmp.Length; i++)
    {
      ObstacleBalloons.Add(_tmp[i]);
    }
    foreach (GameObject obstacleBalloon in ObstacleBalloons)
    {
      obstacleBalloon.GetComponent<ObstacleBalloon>().InitializeTexts();
    }
  }
}
