using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

    public static Vector3 moveDir = Vector3.zero;
    public static bool isFire;
    public JoyStick joyStick;
    public VariableJoystick variableJoystick;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //1. lay input
        moveDir.x= Input.GetAxisRaw("Horizontal")+ variableJoystick.Direction.x;
        moveDir.y =Input.GetAxisRaw("Vertical") + variableJoystick.Direction.y;
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    isFire = true;
        //}
        //else{
        //    isFire = false;
        //}
	}

}
