using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigScene : MonoBehaviour
{
    public static ConfigScene instance;
    public Transform posPlayer;
    public Transform posBoss;
    public PolyNav2D polymap;

    private void Awake()
    {
        instance = this;
    }
}
