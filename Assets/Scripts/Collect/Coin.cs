using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {
            MissionControl.instance.AddCoin();
            transform.DOLocalRotate(new Vector3(0, 180, 0), 0.1f, RotateMode.FastBeyond360).SetLoops(10);
            transform.DOLocalMoveY(transform.position.y + 1, 1f, false).OnComplete(()=>
            {
                Destroy(gameObject);
            });
            
        }        
    }
   
}
