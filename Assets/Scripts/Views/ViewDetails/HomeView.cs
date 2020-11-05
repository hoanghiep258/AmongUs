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
    private Image imgChar;
    [SerializeField]
    private Image imgPet;

    [SerializeField]
    private List<Color> lsColors = new List<Color>();

    [SerializeField]
    private List<Sprite> lsHats = new List<Sprite>();
    [SerializeField]
    private List<Sprite> lsSkins = new List<Sprite>();
    [SerializeField]
    private List<Sprite> lsPets = new List<Sprite>();
    


    [Header("------------------------Button------------------------")]
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
        inpName.text = DataAPIManager.Instance.GetName();

        indexColor = DataAPIManager.Instance.GetColor();
        indexHat = DataAPIManager.Instance.GetHat();
        indexSkin = DataAPIManager.Instance.GetSkin();
        indexPet = DataAPIManager.Instance.GetPet();

        lsColorItems[indexColor].OnClick();
        lsHatItems[indexHat].OnClick();
        lsSkinItems[indexSkin].OnClick();
        lsPetItems[indexPet].OnClick();

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

    public void OnSelectItem(int indexItem, int indexOfList)
    {
        switch (indexItem)
        {
            case 0:
                indexColor = indexOfList;
                for (int i = 0; i < lsColorItems.Count; i++)
                {
                    lsColorItems[i].UnchooseItem();
                }
                imgChar.color = lsColors[indexOfList - 1];
                break;
            case 1:
                indexHat = indexOfList;
                for (int i = 0; i < lsHatItems.Count; i++)
                {
                    lsHatItems[i].UnchooseItem();
                }
                break;
            case 2:
                indexSkin = indexOfList;
                imgChar.color = Color.white;
                imgChar.sprite = lsSkins[indexOfList - 1];              
                for (int i = 0; i < lsSkinItems.Count; i++)
                {
                    lsSkinItems[i].UnchooseItem();
                }
                break;
            case 3:
                indexPet = indexOfList;
                for (int i = 0; i < lsPetItems.Count; i++)
                {
                    lsPetItems[i].UnchooseItem();
                }
                imgPet.sprite = lsPets[indexPet - 1];
                break;
        }
    }
}
