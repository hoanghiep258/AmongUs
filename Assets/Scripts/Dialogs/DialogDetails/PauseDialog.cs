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

    private PauseDialogParam pauseDialogParam;
    public override void OnSetUp(DialogParam param = null)
    {
        pauseDialogParam = (PauseDialogParam)param;
        txtKill.text = pauseDialogParam.valueKill.ToString() + " IMPOSTOR";
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
        HubControl.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        DialogManager.Instance.HideDialog(this);
    }

    public void OnGotoHome()
    {
        DataAPIManager.Instance.AddCoin(pauseDialogParam.valueCoin);
        HubControl.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        DialogManager.Instance.HideDialog(this);
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
    }

    public void OnRestartGame()
    {
        DataAPIManager.Instance.AddCoin(pauseDialogParam.valueCoin);
        HubControl.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
        DialogManager.Instance.HideDialog(this);
    }
}
