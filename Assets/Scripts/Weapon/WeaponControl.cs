using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeGunHandle(WeaponBehaviour weapon);
public class WeaponControl : MonoBehaviour
{
    public event ChangeGunHandle OnChangeGunHandle;
    public List<WeaponBehaviour> lsGun;
    public WeaponBehaviour currentGun;
    private int indexGun = -1;

    public List<int> lsWeaponAmo = new List<int>();
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        //foreach (ConfigGunData e in ConfigManager.configGun.records)
        //{
        //    GameObject go = Instantiate(Resources.Load("Weapon/" + e.name, typeof(GameObject))) as GameObject;
        //    go.transform.SetParent(transform);
        //    go.transform.localPosition = Vector3.zero;
        //    WeaponBehaviour wp = go.GetComponent<WeaponBehaviour>();

        //    GunData gunData = new GunData
        //    {
        //        cf = e,
        //        level = 1
        //    };
        //    wp.SetupData(gunData);
        //    lsGun.Add(wp);
        //    go.SetActive(false);
        //}

        for (int i = 0; i < lsGun.Count; i++)
        {
            lsGun[i].SetupData(new GunData { amoutAmo = lsWeaponAmo[i] });
            lsGun[i].gameObject.SetActive(false);            
        }
      
    }
    public void OnChangeGun(int indexGun)
    {
        Debug.LogError("index gun" + indexGun);

        this.indexGun = indexGun;      
        if (currentGun != null)
        {
            currentGun.gameObject.SetActive(false);

        }
        currentGun = lsGun[indexGun];

        currentGun.gameObject.SetActive(true);
        //if (OnChangeGunHandle != null)
        //{
        //    OnChangeGunHandle(currentGun);
        //}
    }
}
