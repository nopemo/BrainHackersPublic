using UnityEngine;
using UnityEngine.EventSystems;

public class CheckClick : MonoBehaviour, IPointerClickHandler
{
    // クリックされたときに呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"オブジェクト {name} がクリックされたよ！");
    }
}
