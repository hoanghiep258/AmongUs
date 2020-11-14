using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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
    private List<Sprite> lsSkin = new List<Sprite>();
    [SerializeField]
    private Image imgChar;
    [SerializeField]
    private Sprite spriteDefaultChar;

    private PauseDialogParam pauseDialogParam;

    [SerializeField]
    private Transform transPanel;
    public override void OnSetUp(DialogParam param = null)
    {
        transPanel.localScale = Vector3.zero;
        pauseDialogParam = (PauseDialogParam)param;
        txtKill.text = pauseDialogParam.valueKill.ToString() + " IMPOSTOR";
        txtCoin.text = pauseDialogParam.valueCoin.ToString();
        sliderHP.value = pauseDialogParam.percentHP;

        txtName.text = DataAPIManager.Instance.GetName();
        transPanel.DOScale(0.9f, 0.25f).OnComplete(() =>
        {
            Time.timeScale = 0;
        });

        int indexSkin = DataAPIManager.Instance.GetSkin();
        if (indexSkin < 0)
        {
            imgChar.sprite = spriteDefaultChar;
        }
        else
        {
            imgChar.sprite = lsSkin[indexSkin];
        }

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
        AdManager.instance.DisplayInterstitialAD();
        MissionControl.instance.ClearAllEnemy();
        HubControl.instance.DeleteAllHub();
        MissionControl.instance.player.gameObject.SetActive(false);

        DataAPIManager.Instance.AddCoin(pauseDialogParam.valueCoin);
        HubControl.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        DialogManager.Instance.HideDialog(this);
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
    }

    public void OnRestartGame()
    {
        
        MissionControl.instance.ClearAllEnemy();
        HubControl.instance.DeleteAllHub();
        MissionControl.instance.player.gameObject.SetActive(false);

        DataAPIManager.Instance.AddCoin(pauseDialogParam.valueCoin);
        HubControl.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        AdManager.instance.DisplayInterstitialAD(()=>
        {
           
        });
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
        DialogManager.Instance.HideDialog(this);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
