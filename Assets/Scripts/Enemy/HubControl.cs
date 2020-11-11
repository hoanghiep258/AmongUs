using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubControl : MonoBehaviour
{
    public static HubControl instance;
    public Transform HubPrefab;
    public Transform HubPlayerPrefab;
    public RectTransform parentRect;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    public HPHub CreateHub()
    {
        Transform transHub = Instantiate(HubPrefab);
        transHub.SetParent(transform);
        return transHub.GetComponent<HPHub>();
    }

}
