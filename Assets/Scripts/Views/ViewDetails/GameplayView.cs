using System;
using UnityEngine;

public class GameplayView : BaseView
{
    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        base.OnSetUp(param, callback);
    }

    public void OnRestartGame()
    {

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
