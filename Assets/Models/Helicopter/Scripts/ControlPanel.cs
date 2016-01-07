using System.Collections.Generic;
using UnityEngine;

/**
 * \file ControlPanel.cs 
 * \brief Cette classe permet de capter un événement du clavier ou d'une manette  
 */

public class ControlPanel : MonoBehaviour
{
    public AudioSource MusicSound; /*!< la musique ou son qui est joué lorsque l'hélicoptère est en vol */

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
    
    private KeyCode[] keyCodes; /*!< le tableau des touches permettant de controler l'hélicoptère */

    public delegate void DelegateControllerUsed(
        float leftTrigger, float rightTrigger, 
        float leftYAxis, float rightXAxis,
        float leftBumper, float rightBumper);                                    /*!< prototype de la fonction attendu pour les controles de la manette */
    public delegate void DelegateKeyPressed(PressedKeyCode[] pressedKeyCode);    /*!< prototype de la fonction attendu pour les controles du clavier */

    public DelegateControllerUsed ControllerUsed; /*!< delegate sur la fonction qui gère les controles de la manette */
    public DelegateKeyPressed KeyPressed;         /*!< delagate sur la fonction qui gère les controles du clavier */
    
    /**
     *  \brief Initialise le tableau de "keyCodes" 
     */
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

    /**
     *  \brief Cette fonction appelle des fonctions, via des deleguate, si un événement est détécté
     */
    private void FixedUpdate ()
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
