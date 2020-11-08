using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHub : MonoBehaviour
{
    public Image hpProgess;
    public RectTransform rect;
    public RectTransform parentRect;
    // Use this for initialization
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        parentRect = HubControl.instance.parentRect;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnUpdatePos(Vector3 pos)
    {

        Vector2 posUI = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, posUI, null, out localPos);
        rect.anchoredPosition = localPos;
    }
    public void OnUpdateHP(int curent, int maxHP)
    {
        hpProgess.fillAmount = (float)curent / (float)maxHP;
    }
}
