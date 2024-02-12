using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvokeDebug : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject debugCanvas;

    // Update is called once per fram
    public void OnClickDebugButton()
    {
        debugCanvas.SetActive(true);
    }
}
