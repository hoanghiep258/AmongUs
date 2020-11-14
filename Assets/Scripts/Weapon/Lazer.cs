using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
      
    private Vector2 dir;
    public LayerMask mask;
    private float speed;
    public float radius;
    [SerializeField]
    private LineRenderer lineRender;
    [SerializeField]
    private GameObject laserGO;
    // Use this for initialization
    void Awake () {
	}
	
    public void Setup(Vector3 dir, float force)
    {
        this.speed = force;

        //transform.up = dir.normalized;
        laserGO.transform.position = dir;
        lineRender.SetPositions(new Vector3[] { Vector3.zero });
        StartCoroutine(DoLaserSize(dir, MissionControl.instance.player.transform.position));
    }

    IEnumerator DoLaserSize(Vector3 startFirePoint, Vector3 posAttack)
    {
        Vector3 first, second = Vector3.zero;
        Debug.LogError(startFirePoint + " " + posAttack);
        first = startFirePoint;
        second = startFirePoint;
        lineRender.SetPositions(new Vector3[]
            {
                first,
                second
            });
        float deltaX = (posAttack.x - first.x) / 3;
        float deltaY = (posAttack.y - first.y) / 3;
        while (second != posAttack)
        {
            second.x += deltaX;
            second.y += deltaY;
            yield return new WaitForSeconds(0.01f);
            lineRender.SetPositions(new Vector3[]
            {
                first,
                second
            });
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);       
    }

    //private void Update()
    //{
       
    //    transform.localScale = new Vector3(transform.localScale.x + speed * 5 * Time.deltaTime, transform.localScale.y, 1);

    //    RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir, 1f,mask);
    //    if(hit2D.collider != null)
    //    {
    //        OnHit(hit2D);
    //    }
    //}

    //private void OnHit(RaycastHit2D hitInfo)
    //{        
    //    if (hitInfo.collider.gameObject.CompareTag("Player"))
    //    {            
    //            Destroy(gameObject);           
    //    }
          
        
    //}
    

}
