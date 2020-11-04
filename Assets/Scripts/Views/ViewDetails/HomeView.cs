using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeView : BaseView
{
    [SerializeField]
    private TMP_InputField inpName;

    private int indexColor;
    private int indexHat;
    private int indexSkin;
    private int indexPet;

    [SerializeField]
    private List<Sprite> lsBtnSpriteOns = new List<Sprite>();
    [SerializeField]
    private List<Sprite> lsBtnSpriteOffs = new List<Sprite>();
    [SerializeField]
    private List<Image> lsImageBtnShops = new List<Image>();
    [SerializeField]
    private List<GameObject> lsPnlShops = new List<GameObject>();

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        inpName.text = DataAPIManager.Instance.GetName();

        indexColor = DataAPIManager.Instance.GetColor();
        indexHat = DataAPIManager.Instance.GetHat();
        indexSkin = DataAPIManager.Instance.GetSkin();
        indexPet = DataAPIManager.Instance.GetPet();

        OnBtnClick(0);
        base.OnSetUp(param, callback);
    }
    
    public void OnStartGamePlay()
    {
        DataAPIManager.Instance.SetName(inpName.text);

        DataAPIManager.Instance.SetColor(indexColor);
        DataAPIManager.Instance.SetHat(indexHat);
        DataAPIManager.Instance.SetSkin(indexSkin);
        DataAPIManager.Instance.SetPet(indexPet);
        
        // Gameplay view
        ViewManager.Instance.SwitchView(ViewIndex.GameplayView);
    }

    public void OnBtnClick(int index)
    {
        for(int i = 0; i < lsBtnSpriteOffs.Count; i++)
        {
            lsImageBtnShops[i].sprite = lsBtnSpriteOffs[i];
            lsPnlShops[i].SetActive(false);
        }

        lsImageBtnShops[index].sprite = lsBtnSpriteOns[index];
        lsPnlShops[index].SetActive(true);
    }
}
