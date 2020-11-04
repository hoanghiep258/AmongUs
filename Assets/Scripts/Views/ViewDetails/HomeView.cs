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

    [Header("------------------------Item------------------------")]
    [SerializeField]
    private List<ShopItem> lsColorItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsHatItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsSkinItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsPetItems = new List<ShopItem>();

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        inpName.text = DataAPIManager.Instance.GetName();

        indexColor = DataAPIManager.Instance.GetColor();
        indexHat = DataAPIManager.Instance.GetHat();
        indexSkin = DataAPIManager.Instance.GetSkin();
        indexPet = DataAPIManager.Instance.GetPet();

        OnBtnClick(0);

        string[] lsBoughtColor = DataAPIManager.Instance.GetLsBoughtColor();
        if (lsBoughtColor != null)
        {
            for (int i = 0; i < lsBoughtColor.Length; i++)
            {

                int index = int.Parse(lsBoughtColor[i]);
                lsColorItems[index].OnSetup(true);
            }
        }
        

        string[] lsBoughtHat = DataAPIManager.Instance.GetLsBoughtHat();
        if (lsBoughtHat != null)
        {
            for (int i = 0; i < lsBoughtHat.Length; i++)
            {
                int index = int.Parse(lsBoughtHat[i]);
                lsHatItems[index].OnSetup(true);
            }
        }

        string[] lsBoughtSkin = DataAPIManager.Instance.GetLsBoughtSkin();
        if (lsBoughtSkin != null)
        {
            for (int i = 0; i < lsBoughtSkin.Length; i++)
            {
                int index = int.Parse(lsBoughtSkin[i]);
                lsSkinItems[index].OnSetup(true);
            }
        }

        string[] lsBoughtPet = DataAPIManager.Instance.GetLsBoughtPet();
        if (lsBoughtPet != null)
        {
            for (int i = 0; i < lsBoughtPet.Length; i++)
            {
                int index = int.Parse(lsBoughtPet[i]);
                lsPetItems[index].OnSetup(true);
            }
        }
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
