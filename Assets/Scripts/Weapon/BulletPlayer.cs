using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    public Transform enemy_impact;    
    private Vector2 dir;
    public LayerMask mask;
    private float speed;
    public bool isAoe = false;
    private bool isHit = false;
    private float timer = 0;
    public float radius;
	// Use this for initialization
	void Awake () {
	}
	
    public void Setup(Vector2 dir, float force)
    {
        this.speed = force;
        this.dir = dir;
        transform.up = dir.normalized;
    }

    private void Update()
    {
        if (isHit)
        {
            timer += Time.deltaTime;
            if (timer > 0.35f)
            {
                CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
                circleCollider.radius = radius;
            }
            if (timer > 0.5f)
            {
                Destroy(gameObject);
            }
            return;
        }
            

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir, 1f,mask);
        if(hit2D.collider != null)
        {
            OnHit(hit2D);
        }
    }

    private void OnHit(RaycastHit2D hitInfo)
    {
        Transform impact = null;
        if (hitInfo.collider.gameObject.CompareTag("Enemy"))
        {
            hitInfo.collider.gameObject.GetComponent<EnemyOnDamage>().ApplyDamage(1);
            impact = Instantiate(enemy_impact);
            if (impact != null)
            {

                // set vi tri va huong
                impact.position = hitInfo.point;
                impact.forward = hitInfo.normal;
                //Quaternion q = Quaternion.Euler(90, 0, 0);
                //impact.forward = q * (-transform.up);                
            }
            if (!isAoe)
            {
                Destroy(gameObject);
                SoundManager.instance.PlaySound(SoundIndex.Enemy_kill);
            }  
            else
            {
                SoundManager.instance.PlaySound(SoundIndex.Gernade);
                isHit = true;
                
            }
        }
          
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {        
        if (coll.gameObject.CompareTag("Enemy"))
            coll.gameObject.GetComponent<EnemyOnDamage>().ApplyDamage(5); ;
    }

}
