using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
#if UNITY_EDITOR
        Debug.logger.logEnabled = true;
#else
  Debug.logger.logEnabled = false;
#endif
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
                            if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
                            {
                                Camera.main.orthographicSize = 7;
                            }                            
                            ViewManager.Instance.SwitchView(ViewIndex.HomeView);
                        });
                    });
                });
            });
        });
    }
}
