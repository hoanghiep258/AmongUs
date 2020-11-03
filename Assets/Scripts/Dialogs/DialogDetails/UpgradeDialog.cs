using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDialog : BaseDialog
{
    public override void OnSetUp(DialogParam param = null)
    {
        base.OnSetUp(param);
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public void OnClose()
    {
        DialogManager.Instance.HideDialog(this);
    }
}

