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
    [SerializeField]
    private TextMeshProUGUI txtCoin;

    private int indexColor;
    private int indexHat;
    private int indexSkin;
    private int indexPet;

    [SerializeField]
    private Image imgChar;
    [SerializeField]
    private Image imgCharSkin;
    [SerializeField]
    private Sprite defaultSpriteChar;
    [SerializeField]
    private Image imgPet;
    [SerializeField]
    private Image imgHat;

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
    private int curIndexBtn;

    [Header("------------------------Item------------------------")]
    [SerializeField]
    private List<ShopItem> lsColorItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsHatItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsSkinItems = new List<ShopItem>();
    [SerializeField]
    private List<ShopItem> lsPetItems = new List<ShopItem>();

    [Header("------------------------Panel------------------------")]
    [SerializeField]
    private RectTransform rectChar;
    [SerializeField]
    private RectTransform rectShop;
    [SerializeField]
    private RectTransform rectCoin;

    [SerializeField]
    private List<GridLayoutGroup> lsGridLayoutGroups = new List<GridLayoutGroup>();
    private bool isStart = false;
    private void Start()
    {        
        if (((float)Screen.width / (float)Screen.height) < 2)
        {
            
            rectChar.sizeDelta = new Vector2(635, rectChar.sizeDelta.y);
            rectChar.localPosition = new Vector3(-515f, 0, 0);
            rectChar.localScale = new Vector3(0.85f, 0.85f, 1);

            rectShop.sizeDelta = new Vector2(1032.5f, rectShop.sizeDelta.y);
            rectShop.localPosition = new Vector3(320f, 0, 0);
            for(int i = 0; i < lsGridLayoutGroups.Count; i++)
            {
                lsGridLayoutGroups[i].cellSize = new Vector2(210, 210);
            }
        }
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            rectCoin.localPosition = new Vector3(-350, 450, 0);
        }

        }

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        //OnBtnClick(0);            
        AdManager.instance.HideBanner();
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
        //inpName.text = DataAPIManager.Instance.GetName();

        indexColor = DataAPIManager.Instance.GetColor();
        indexHat = DataAPIManager.Instance.GetHat();
        indexSkin = DataAPIManager.Instance.GetSkin();
        indexPet = DataAPIManager.Instance.GetPet();
        Debug.LogError(indexColor + " " + indexSkin);
        if (indexColor < 0)
        {
            //indexColor = 0;
            imgHat.gameObject.SetActive(false);
        }
        else
        {
            OnBtnClick(0);
            lsColorItems[indexColor].OnClick();
        }
        
        lsHatItems[indexHat].OnClick();
        if (isStart)
        {
            if (indexSkin < 0)
            {
                //indexSkin = 0;            
                imgHat.gameObject.SetActive(true);
            }
            else
            {
                OnBtnClick(2);
                lsSkinItems[indexSkin].OnClick();
            }
        }
        else
        {
            isStart = true;
            OnBtnClick(0);
            lsColorItems[indexColor].OnClick();
        }
        
        lsPetItems[indexPet].OnClick();

        base.OnSetUp(param, callback);
    }

    public void OnStartGamePlay()
    {
        SoundManager.instance.PlaySound(SoundIndex.Click);
        if (string.IsNullOrEmpty(inpName.text))
        {
            inpName.text = "Player";
        }    
        DataAPIManager.Instance.SetName(inpName.text);
        if (curIndexBtn == 0)
        {
            DataAPIManager.Instance.SetColor(indexColor);
            DataAPIManager.Instance.SetSkin(-1);
        }
        else
        {
            DataAPIManager.Instance.SetColor(-1);
            DataAPIManager.Instance.SetSkin(indexSkin);
        }
        DataAPIManager.Instance.SetHat(indexHat);       
        DataAPIManager.Instance.SetPet(indexPet);

            Debug.LogError("gameplay view");
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

        SoundManager.instance.PlaySound(SoundIndex.Click);

        curIndexBtn = index;
        if (index > 1)
        {
            AdManager.instance.DisplayInterstitialAD();
            AdManager.instance.RequestInterstitial();
        }
        switch (index)
        {
            case 0:
                imgChar.gameObject.SetActive(true);
                imgChar.sprite = defaultSpriteChar;
                imgHat.gameObject.SetActive(true);
                imgCharSkin.gameObject.SetActive(false);
                if (indexColor < 0)
                {
                    indexColor = 0;
                }
                indexSkin = -1;
                OnSelectItem(0, 1);
                break;
            case 2:
                imgCharSkin.gameObject.SetActive(true);
                imgChar.gameObject.SetActive(false);
                imgHat.gameObject.SetActive(false);
                if (indexSkin < 0)
                {
                    indexSkin = 0;
                }
                indexColor = -1;
                OnSelectItem(2, 1);
                break;
        }
    }

    public void OnSelectItem(int indexItem, int indexOfList)
    {
        switch (indexItem)
        {
            case 0:
                indexColor = indexOfList - 1;
                for (int i = 0; i < lsColorItems.Count; i++)
                {
                    lsColorItems[i].UnchooseItem();
                }
                imgChar.color = lsColors[indexOfList - 1];
                //imgChar.SetNativeSize();
                break;
            case 1:
                indexHat = indexOfList - 1;
                for (int i = 0; i < lsHatItems.Count; i++)
                {
                    lsHatItems[i].UnchooseItem();
                }
                imgHat.sprite = lsHats[indexOfList - 1];
                break;
            case 2:
                indexSkin = indexOfList - 1;
                imgChar.color = Color.white;
                imgCharSkin.sprite = lsSkins[indexOfList - 1];
                //imgChar.SetNativeSize();
                for (int i = 0; i < lsSkinItems.Count; i++)
                {
                    lsSkinItems[i].UnchooseItem();
                }
                break;
            case 3:
                indexPet = indexOfList - 1;
                for (int i = 0; i < lsPetItems.Count; i++)
                {
                    lsPetItems[i].UnchooseItem();
                }
                imgPet.sprite = lsPets[indexOfList - 1];
                break;
        }
    }

    private void Update()
    {
        txtCoin.text = DataAPIManager.Instance.GetCoin().ToString();
    }
}
