using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

    public static Vector3 moveDir = Vector3.zero;
    public static bool isFire;
    public JoyStick joyStick;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //1. lay input
        moveDir.x= Input.GetAxisRaw("Horizontal")+joyStick.inputDir.x;
        moveDir.y =Input.GetAxisRaw("Vertical")+ joyStick.inputDir.y;
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    isFire = true;
        //}
        //else{
        //    isFire = false;
        //}
	}

}
