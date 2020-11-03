using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDialog : BaseDialog
{
    [SerializeField]
    private TextMeshProUGUI txtName;

    [SerializeField]
    private TextMeshProUGUI txtKill;

    [SerializeField]
    private TextMeshProUGUI txtCoin;

    public override void OnSetUp(DialogParam param = null)
    {
        GameOverDialogParam gameOverDialogParam = (GameOverDialogParam)param;
        txtCoin.text = gameOverDialogParam.valueCoin.ToString();
        txtKill.text = gameOverDialogParam.valueKill.ToString();

        // Get Name
        txtName.text = DataAPIManager.Instance.GetName();
        base.OnSetUp(param);
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
