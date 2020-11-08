using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDamage : MonoBehaviour {

    EnemyControl parent_;
    public EnemyControl parent{
        get{
            if(parent_==null)
            {
                parent_ = GetComponent<EnemyControl>();
            }
            return parent_;
        }
    }
    public void ApplyDamage(int damage)
    {
        parent.Ondamage(damage);
    }
}
