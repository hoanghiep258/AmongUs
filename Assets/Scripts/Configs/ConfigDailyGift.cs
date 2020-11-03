using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigDailyGiftData
{
    public int day;
    //[SerializeField]
    public string rewardIcon;

    private List<REWARD_ICON> lstRewardIcon;
    public List<REWARD_ICON> RewardIcon
    {
        get
        {
            if (lstRewardIcon == null)
            {
                lstRewardIcon = new List<REWARD_ICON>();
                lstRewardIcon = rewardIcon.GetListByString<REWARD_ICON>(';');
            }
            return lstRewardIcon;
        }
    }

    //[SerializeField]
    public string valueGift;

    private List<int> lstValueGift;
    public List<int> ValueGift
    {
        get
        {
            if(lstValueGift == null)
            {
                lstValueGift = new List<int>();
                lstValueGift = valueGift.GetListByString<int>(';');
            }
            return lstValueGift;
        }
    }

    //[SerializeField]
    public string rewardType;

    private List<REWARD_TYPE> lstRewardType;
    public List<REWARD_TYPE> RewardType
    {
        get
        {
            if(lstRewardType == null) {
                lstRewardType = new List<REWARD_TYPE>();
                lstRewardType = rewardType.GetListByString<REWARD_TYPE>(';');
            }
            return lstRewardType;
        }
    }
}

public class ConfigDailyGift : ConfigDataTable<ConfigDailyGiftData>
{
    public override void AddKeySort()
    {
        OnAddKeySort(new ConfigComparePrimaryKey<ConfigDailyGiftData>("day"));
    }
}
