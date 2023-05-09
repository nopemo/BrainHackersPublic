using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;

public class HoldButtonScript : MonoBehaviour
{

  [SerializeField] float holdSeconds; // �������ɂ����鎞�ԁB�ύX���Ă��������B

  [SerializeField] GameObject spriteMask;
  Transform spriteMaskTransfrom;

  [SerializeField] GameObject barSprite;
  Transform barSpriteTransform;

  float pressingTime = 0.0f;
  bool isPressing = false;
  bool isEnableHold = true;

  [SerializeField] int BUTTON_WIDTH = 1050; // �Œ�
  [SerializeField] float BAR_OFFSET = 0.03f; // �_�̈ʒu���ς��B�K�v�ɉ����ĕύX���Ă��������B
  [SerializeField] string scenename = "GameScene"; // �V�[���������Ă��������B
  float BAR_START_POS;
  float BAR_RANGE;

  void Start()
  {
    spriteMaskTransfrom = spriteMask.GetComponent<Transform>();
    barSpriteTransform = barSprite.GetComponent<Transform>();

    BAR_RANGE = (float)BUTTON_WIDTH / 100;
    BAR_START_POS = -(BAR_RANGE / 2) - BAR_OFFSET;
  }

  void Update()
  {
    if (isEnableHold)
    {
      if (isPressing)
      {
        barSprite.SetActive(true);
        pressingTime += Time.deltaTime;
        float chargeRate = Math.Min(pressingTime / holdSeconds, 1.0f);

        spriteMaskTransfrom.localScale = new Vector3(chargeRate, 1, 1);
        barSpriteTransform.localPosition = new Vector3(BAR_START_POS + BAR_RANGE * chargeRate, -0.03f, 0);

        if (chargeRate >= 1.0f) EndHold();
      }
      else
      {
        pressingTime = 0.0f;
        spriteMaskTransfrom.localScale = new Vector3(0, 1, 1);
        barSpriteTransform.localPosition = new Vector3(BAR_START_POS, -0.03f, 0);
      }
    }
  }

  void EndHold()
  {
    isEnableHold = false;
    barSprite.SetActive(false);

    //Load scene
    SceneManager.LoadScene(scenename);
    // �{�^���𒷉������I��������̏����������ɏ����Ă��������B
    Debug.Log("EndHold");
  }

  public void OnPointerDown()
  {
    isPressing = true;
  }

  public void OnPointerUp()
  {
    isPressing = false;
  }

  public void OnPointerExit()
  {
    isPressing = false;
  }

  public void Reset()
  {
    isEnableHold = true;
    pressingTime = 0.0f;
    spriteMaskTransfrom.localScale = new Vector3(0, 1, 1);
    barSpriteTransform.localPosition = new Vector3(BAR_START_POS, -0.03f, 0);
  }
}
