using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class GameOverDialog : BaseDialog
{
    [SerializeField]
    private TextMeshProUGUI txtName;

    [SerializeField]
    private TextMeshProUGUI txtKill;

    [SerializeField]
    private TextMeshProUGUI txtCoin;

    [SerializeField]
    private List<Sprite> lsSkin = new List<Sprite>();
    [SerializeField]
    private Image imgChar;
    [SerializeField]
    private Sprite spriteDefaultChar;

    [SerializeField]
    private Transform transPanel;

    public override void OnSetUp(DialogParam param = null)
    {
        transPanel.localScale = Vector3.zero;
        GameOverDialogParam gameOverDialogParam = (GameOverDialogParam)param;
        txtCoin.text = gameOverDialogParam.valueCoin.ToString();
        txtKill.text = gameOverDialogParam.valueKill.ToString() + " IMPOSTOR"; ;

        // Get Name
        txtName.text = DataAPIManager.Instance.GetName();
        int indexSkin = DataAPIManager.Instance.GetSkin();        
        if (indexSkin < 0)
        {
            imgChar.sprite = spriteDefaultChar;
        }
        else
        {
            imgChar.sprite = lsSkin[indexSkin];
        }
     
        DataAPIManager.Instance.AddCoin(gameOverDialogParam.valueCoin);
        transPanel.DOScale(0.85f, 0.25f).OnComplete(() =>
        {
            Time.timeScale = 0;
        });
        base.OnSetUp(param);
    }

    public void OnGotoHome()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlaySound(SoundIndex.Click);
        HubControl.instance.gameObject.SetActive(true);
        MissionControl.instance.player.gameObject.SetActive(false);
        AdManager.instance.DisplayInterstitialAD();
        
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
        DialogManager.Instance.HideDialog(this);

    }

    public void OnRestartGame()
    {
        Time.timeScale = 1;
        HubControl.instance.gameObject.SetActive(true);
        MissionControl.instance.player.gameObject.SetActive(false);
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
        DialogManager.Instance.HideDialog(this);
    }
}
