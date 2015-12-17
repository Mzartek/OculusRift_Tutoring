using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour {
    public AudioSource MusicSound;

    [SerializeField]
    KeyCode SpeedUp = KeyCode.Space;
    [SerializeField]
    KeyCode SpeedDown = KeyCode.C;
    [SerializeField]
    KeyCode Forward = KeyCode.Z;
    [SerializeField]
    KeyCode Back = KeyCode.S;
    [SerializeField]
    KeyCode Left = KeyCode.Q;
    [SerializeField]
    KeyCode Right = KeyCode.D;
    [SerializeField]
    KeyCode TurnLeft = KeyCode.A;
    [SerializeField]
    KeyCode TurnRight = KeyCode.E;
    
    private KeyCode[] keyCodes;

    public delegate void DelegateControllerUsed(
        float leftTrigger, float rightTrigger, 
        float leftYAxis, float rightXAxis,
        float leftBumper, float rightBumper);
    public delegate void DelegateKeyPressed(PressedKeyCode[] pressedKeyCode);

    public DelegateControllerUsed ControllerUsed;
    public DelegateKeyPressed KeyPressed;
    
    private void Awake()
    {
        keyCodes = new[] {
                            SpeedUp,
                            SpeedDown,
                            Forward,
                            Back,
                            Left,
                            Right,
                            TurnLeft,
                            TurnRight
                        };

    }

    void Start () {
	
	}

	void FixedUpdate ()
	{
	    var pressedKeyCode = new List<PressedKeyCode>();
	    for (int index = 0; index < keyCodes.Length; index++)
	    {
	        if (Input.GetKey(keyCodes[index]))
                pressedKeyCode.Add((PressedKeyCode)index);
	    }

        if (ControllerUsed != null)
            ControllerUsed(
                Input.GetAxis("LeftTrigger"), Input.GetAxis("RightTrigger"),
                Input.GetAxis("LeftYAxis"), Input.GetAxis("RightXAxis"),
                Input.GetAxis("LeftBumper"), Input.GetAxis("RightBumper"));

	    if (KeyPressed != null) KeyPressed(pressedKeyCode.ToArray());      
	}
}
