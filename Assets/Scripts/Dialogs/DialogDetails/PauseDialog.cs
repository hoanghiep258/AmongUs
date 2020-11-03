using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
    public override void OnSetUp(DialogParam param = null)
    {
        base.OnSetUp(param);
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public void OnContinueGame()
    {
        DialogManager.Instance.HideDialog(this);
    }

    public void OnGotoHome()
    {
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
    }

    public void OnRestartGame()
    {
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
    }
}
