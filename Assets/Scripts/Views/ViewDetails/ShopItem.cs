using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        indexOfList = int.Parse(transform.name);

        goIsSelected = transform.Find("imgChoose").gameObject;
        goIsSelected.SetActive(false);
        if (isCoin)
        {
            transform.Find("imgLock").Find("imgFrame").Find("txtCoin").GetComponent<TextMeshProUGUI>().text = price.ToString();
        }
       
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
                DataAPIManager.Instance.AddColor(indexOfList.ToString());
                break;
            case 1:
                DataAPIManager.Instance.AddHat(indexOfList.ToString());
                break;
            case 2:
                DataAPIManager.Instance.AddSkin(indexOfList.ToString());
                break;
            case 3:
                DataAPIManager.Instance.AddPet(indexOfList.ToString());
                break;
        }
        goIsSelected.SetActive(true);
    }
}
