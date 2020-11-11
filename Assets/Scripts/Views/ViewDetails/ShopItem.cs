using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private int price;
    [SerializeField]
    private bool isCoin;
    [SerializeField]
    private GameObject goIsSelected;

    private bool isBought;
    
    private int indexOfList;
    [SerializeField]
    private int indexItem;
    private GameObject goLock;
    private void Awake()
    {
        indexOfList = int.Parse(transform.name);

        goIsSelected = transform.Find("imgChoose").gameObject;
        goLock = transform.Find("imgLock").gameObject;
        goIsSelected.SetActive(false);
        if (isCoin)
        {
            transform.Find("imgLock").Find("imgFrame").Find("txtCoin").GetComponent<TextMeshProUGUI>().text = price.ToString();
        }

        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnClick();
        });
    }

    public void OnSetup(bool isBought)
    {
        this.isBought = isBought;
        transform.Find("imgLock").gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (isBought)
        {
            HomeView homeView = (HomeView)ViewManager.Instance.currentView;
            homeView.OnSelectItem(indexItem, indexOfList);

            goIsSelected.SetActive(true);
            return;
        }

        if (isCoin)
            {
                int curCoin = DataAPIManager.Instance.GetCoin();
                if (curCoin >= price)
                {
                    DataAPIManager.Instance.AddCoin(-price);
                    // unlock
                    UnlockItem();
                }
                else
                {
                    // show not enough
                }
        }
        else
        {
            // show ads
            UnlockItem();
        }
    }

    public void UnlockItem()
    {
        switch (indexItem)
        {
            case 0:
                DataAPIManager.Instance.AddColor((indexOfList - 1).ToString());
                break;
            case 1:
                DataAPIManager.Instance.AddHat((indexOfList - 1).ToString());
                break;
            case 2:
                DataAPIManager.Instance.AddSkin((indexOfList - 1).ToString());
                break;
            case 3:
                DataAPIManager.Instance.AddPet((indexOfList - 1).ToString());
                break;
        }
        HomeView homeView = (HomeView)ViewManager.Instance.currentView;
        homeView.OnSelectItem(indexItem, indexOfList);

        goIsSelected.SetActive(true);
        goLock.SetActive(false);
    }

    public void UnchooseItem()
    {
        goIsSelected.SetActive(false);
    }
}
