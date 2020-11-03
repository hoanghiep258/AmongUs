using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(0.5f);
        ConfigManager.Instance.InitConfig(()=> {
            DataAPIManager.Instance.InitData(() =>
            {
                ViewManager.Instance.InitView(() =>
                {
                    DialogManager.Instance.InitDialog(() =>
                    {
                        LoadSceneManager.Instance.OnLoadScene("Buffer", (obj) =>
                        {
                            ViewManager.Instance.SwitchView(ViewIndex.HomeView);
                        });
                    });
                });
            });
        });
    }
}
