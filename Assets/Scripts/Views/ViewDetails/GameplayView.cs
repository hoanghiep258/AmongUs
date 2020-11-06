using System;
using UnityEngine;
using TMPro;

public class GameplayView : BaseView
{

    [SerializeField]
    private TextMeshProUGUI txtSkillKnife;
    [SerializeField]
    private TextMeshProUGUI txtSkillHammer;
    [SerializeField]
    private TextMeshProUGUI txtSkillGun;
    [SerializeField]
    private TextMeshProUGUI txtSkillGernade;

    private int valueSkillKnife;
    private int valueSkillHammer;
    private int valueSkillGun;
    private int valueSkillGernade;

    private const int defaultSkillKnife = 5;
    private const int defaultSkillHammer = 5;
    private const int defaultSkillGun= 5;
    private const int defaultSkillGernade = 5;

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        base.OnSetUp(param, callback);
        valueSkillKnife = defaultSkillKnife;
        valueSkillHammer = defaultSkillHammer;
        valueSkillGun = defaultSkillGun;
        valueSkillGernade = defaultSkillGernade;

        txtSkillKnife.text = valueSkillKnife.ToString();
        txtSkillHammer.text = valueSkillHammer.ToString();
        txtSkillGun.text = valueSkillGun.ToString();
        txtSkillGernade.text = valueSkillGernade.ToString();
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
}
