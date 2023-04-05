using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject BeamPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // 頂点座標リスト
        List<Vector3> PointList = new List<Vector3>();
        PointList.Add(new Vector3(0, 0, 0));
        PointList.Add(new Vector3(1000, 1000, 0));

        // Lineオブジェクトの生成
GameObject beam = Instantiate(BeamPrefab,
                    new Vector3(0, 0, 0),
                    Quaternion.identity) as GameObject;

// LineRenderer取得
LineRenderer line = beam.GetComponent<LineRenderer>();
line.positionCount = PointList.Count;

// 各頂点の座標設定
for (int iCnt = 0; iCnt < PointList.Count; iCnt++)
{
    line.SetPosition(iCnt, PointList[iCnt]);
}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
