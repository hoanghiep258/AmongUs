using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseDialog : BaseDialog
{
    [SerializeField]
    private TextMeshProUGUI txtName;
    [SerializeField]
    private TextMeshProUGUI txtKill;
    [SerializeField]
    private TextMeshProUGUI txtCoin;

    [SerializeField]
    private Slider sliderHP;

    [SerializeField]
    private Image imgChar;

    public override void OnSetUp(DialogParam param = null)
    {
        PauseDialogParam pauseDialogParam = (PauseDialogParam)param;
        txtKill.text = pauseDialogParam.valueKill.ToString();
        txtCoin.text = pauseDialogParam.valueCoin.ToString();
        sliderHP.value = pauseDialogParam.percentHP;

        txtName.text = DataAPIManager.Instance.GetName();
        
        Time.timeScale = 0;
        base.OnSetUp(param);
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public void OnContinueGame()
    {
        Time.timeScale = 1;
        DialogManager.Instance.HideDialog(this);
    }

    public void OnGotoHome()
    {
        Time.timeScale = 1;
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
    }

    public void OnRestartGame()
    {
        Time.timeScale = 1;
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
    }
}
