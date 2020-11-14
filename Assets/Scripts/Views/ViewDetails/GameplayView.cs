using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameplayView : BaseView
{
    [SerializeField]
    private List<TextMeshProUGUI> lsTxtSkillCounts = new List<TextMeshProUGUI>();
    private List<int> lsSkillCounts = new List<int>();
    private int[] lsDefaultSkillCounts = { 10, 20, 5, 15 };

    public VariableJoystick variableJoystick;
    public JoyStick joyStick;
    [SerializeField]
    private TextMeshProUGUI txtCoin;

    [SerializeField]
    private GameObject goWarning;

    [SerializeField]
    private List<Button> lsButtonSkills = new List<Button>();
    private bool isClick;
    [SerializeField]
    private AudioSource countdownAudioSource;

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        base.OnSetUp(param, callback);
        isClick = false;
        MissionControl.instance.player.GetComponent<CharacterDataBinding>().Attack = -1;
        lsSkillCounts.Clear();
        
        for (int i = 0; i < lsDefaultSkillCounts.Length; i++)
        {
            lsSkillCounts.Add(lsDefaultSkillCounts[i]);
            lsTxtSkillCounts[i].text = lsDefaultSkillCounts[i].ToString();
        }
        if (DataAPIManager.Instance.GetColor() < 0)
        {
            MissionControl.instance.InitMission(false);
        }
        else
        {
            MissionControl.instance.InitMission(true);
        }
        goWarning.SetActive(false);
    }

    public void OnRestartGame()
    {
        OnSetUp();
    }

    public void OnPauseGame()
    {
        SoundManager.instance.PlaySound(SoundIndex.Click);
        AdManager.instance.DisplayInterstitialAD();
        HubControl.instance.gameObject.SetActive(false);
        DialogManager.Instance.ShowDialog(DialogIndex.PauseDialog, new PauseDialogParam
        {
            percentHP = MissionControl.instance.player.GetComponent<CharacterHealth>().PercentHP(),
            valueCoin = MissionControl.instance.curCoin,
            valueKill = MissionControl.instance.totalEnemyDead
        });
    }

    public void OnClickSkill(int index)
    {
        if (isClick)
            return;

        if (lsSkillCounts[index] <= 0)
        {            
            return;
        }
            
        if (MissionControl.instance.lsEnemyControls.Count <= 0)
        {
            return;
        }
        isClick = true;
        if (lsSkillCounts[index] <= 1)
        {
            lsButtonSkills[index].interactable = false;
        }
        else
        {
            lsButtonSkills[index].interactable = true;
        }
        
        MissionControl.instance.player.GetComponent<CharacterDataBinding>().Attack = index;
        lsSkillCounts[index] = MissionControl.instance.player.GetComponent<WeaponControl>().lsGun[index].currentBullet;
        lsTxtSkillCounts[index].text = lsSkillCounts[index].ToString();
        MissionControl.instance.player.GetComponent<WeaponControl>().OnChangeGun(index);
        OnFire();
    }

    public void OnCollectSkill(int index)
    {
        lsButtonSkills[index].interactable = true;
        lsSkillCounts[index]++;
        lsTxtSkillCounts[index].text = lsSkillCounts[index].ToString();
    }

    public void OnFire()
    {
        
        StopCoroutine("DelayFire");
        StartCoroutine(DelayFire(0.5f));
    }

    private void Update()
    {
        txtCoin.text = MissionControl.instance.curCoin.ToString();
    }

    public void ShowWarning()
    {
        StartCoroutine(CountdownBoss());
    }

    IEnumerator CountdownBoss()
    {
        goWarning.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        countdownAudioSource.Play();
        yield return new WaitForSecondsRealtime(3f);
        countdownAudioSource.Pause();        
        goWarning.SetActive(false);
        MissionControl.instance.ShowBoss();

    }

    IEnumerator DelayFire(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        CharacterInput.isFire = true;
        yield return new WaitForSecondsRealtime(timer);
        MissionControl.instance.player.GetComponent<CharacterDataBinding>().Attack = -1;
        isClick = false;
    }
}
