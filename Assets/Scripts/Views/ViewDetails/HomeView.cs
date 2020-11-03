using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HomeView : BaseView
{
    [SerializeField]
    private TMP_InputField inpName;

    private int indexColor;
    private int indexHat;
    private int indexSkin;
    private int indexPet;

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        inpName.text = DataAPIManager.Instance.GetName();

        indexColor = DataAPIManager.Instance.GetColor();
        indexHat = DataAPIManager.Instance.GetHat();
        indexSkin = DataAPIManager.Instance.GetSkin();
        indexPet = DataAPIManager.Instance.GetPet();

        base.OnSetUp(param, callback);
    }
    
    public void OnStartGamePlay()
    {
        DataAPIManager.Instance.SetName(inpName.text);

        DataAPIManager.Instance.SetColor(indexColor);
        DataAPIManager.Instance.SetHat(indexHat);
        DataAPIManager.Instance.SetSkin(indexSkin);
        DataAPIManager.Instance.SetPet(indexColor);

        // Gameplay view
        ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
    }
}
