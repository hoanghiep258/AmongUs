using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterDataBinding dataBinding;
    public AnimatorOverrideController animatorOverrideController;
    //private WeaponBehaviour weapon;
    public LayerMask layerObstacleMask;
    [SerializeField]
    private float speed;
    private void Awake()
    {
        //dataBinding.Setup(animatorOverrideController);
        //GetComponent<WeaponControl>().OnChangeGunHandle += ChangeGunHandle;
    }
    // Use this for initialization
    //void ChangeGunHandle(WeaponBehaviour weapon_){
    //    if(this.weapon!=null)
    //        this.weapon.OnShootHandle -= OnShootHandle;
    //    this.weapon = weapon_;

    //    dataBinding.Setup(weapon_.animatorOverrideController);
    //    this.weapon.OnShootHandle += OnShootHandle;
    //}
    void OnShootHandle()
    {
       
        dataBinding.Attack = true;
    }
	// Update is called once per frame
	void Update ()
	{
        //1. lay input
        float x = CharacterInput.moveDir.x;
        float y = CharacterInput.moveDir.y;

		Vector2 dir2 = new Vector2 (x, y);
        if (dir2.magnitude > 0.0f)
        {

            //1. xoay theo huong input
            //float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            //Quaternion q = Quaternion.Euler(0, 0, angle - 90);
            //transform.localRotation =
            //    Quaternion.Slerp(transform.localRotation, q, Time.deltaTime * 10f);
            //2. move thoe vector up
            RaycastHit2D hit2D= Physics2D.Raycast(transform.position, transform.up, 1, layerObstacleMask);
            if(hit2D.collider!=null)
            {
               // Debug.Log(hit2D.transform.name);
            }
            else
            {
                transform.Translate(dir2 * Time.deltaTime * speed, Space.Self);

            }
        }
        dataBinding.Speed = dir2.magnitude;
		

	}
}
