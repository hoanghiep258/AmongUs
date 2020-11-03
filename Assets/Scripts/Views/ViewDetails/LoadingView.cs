using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : BaseView
{
    public Image progess;

    public override void OnSetUp(ViewParam param = null, Action callback = null)
    {
        base.OnSetUp(param, callback);
    }

    public void OnUpdateProgess(float value)
    {
        if(progess != null)
            progess.fillAmount = value;
    }

    public override void OnHide(Action callback)
    {
        base.OnHide(callback);
    }
}
