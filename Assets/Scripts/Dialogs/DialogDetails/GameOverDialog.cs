using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    public override void OnSetUp(DialogParam param = null)
    {
        GameOverDialogParam gameOverDialogParam = (GameOverDialogParam)param;
        txtCoin.text = gameOverDialogParam.valueCoin.ToString();
        txtKill.text = gameOverDialogParam.valueKill.ToString();

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
        base.OnSetUp(param);
    }

    public void OnGotoHome()
    {
        ViewManager.Instance.SwitchView(ViewIndex.HomeView);
        DialogManager.Instance.HideDialog(this);

    }

    public void OnRestartGame()
    {
        // Restart game
        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        gameplayView.OnRestartGame();
        DialogManager.Instance.HideDialog(this);
    }
}
