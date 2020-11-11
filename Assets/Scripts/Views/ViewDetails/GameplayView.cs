using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameplayView : BaseView
{
    [SerializeField]
    private List<TextMeshProUGUI> lsTxtSkillCounts = new List<TextMeshProUGUI>();
    private List<int> lsSkillCounts = new List<int>();
    private int[] lsDefaultSkillCounts = { 10, 20, 5, 15 };

    public JoyStick joyStick;

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        base.OnSetUp(param, callback);
        lsSkillCounts.Clear();

        for (int i = 0; i < lsDefaultSkillCounts.Length; i++)
        {
            lsSkillCounts.Add(lsDefaultSkillCounts[i]);
            lsTxtSkillCounts[i].text = lsDefaultSkillCounts[i].ToString();
        }
        if (DataAPIManager.Instance.GetColor() <= 0)
        {
            MissionControl.instance.InitMission(false);
        }
        else
        {
            MissionControl.instance.InitMission(true);
        }
    }

    public void OnRestartGame()
    {
        OnSetUp();
    }

    public void OnPauseGame()
    {
        DialogManager.Instance.ShowDialog(DialogIndex.PauseDialog, new PauseDialogParam
        {
            percentHP = 90,
            valueCoin = 0,
            valueKill = 0
        });
    }

    public void OnClickSkill(int index)
    {
        if (lsSkillCounts[index] <= 0)
            return;
        if (MissionControl.instance.lsEnemyControls.Count <= 0)
        {
            return;
        }
        lsSkillCounts[index]--;
        lsTxtSkillCounts[index].text = lsSkillCounts[index].ToString();
        MissionControl.instance.player.GetComponent<WeaponControl>().OnChangeGun(index);
        OnFire();
    }

    public void OnCollectSkill(int index)
    {
        lsSkillCounts[index]++;
        lsTxtSkillCounts[index].text = lsSkillCounts[index].ToString();
    }

    public void OnFire()
    {
        CharacterInput.isFire = true;
    }
}
