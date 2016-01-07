using UnityEngine;

/**
 * \file HeliRotorController.cs
 * \brief Cette classe permet de controler la rotation des rotors de l'hélicoptère
 */
public class HeliRotorController : MonoBehaviour
{
	public enum Axis
	{
		X,
		Y,
		Z,
	}
	public Axis RotateAxis;     /*!< permet de choisir un axe de rotation */
    private float _rotarSpeed;  /*!< la vitesse de rotation du rotor */
    public float RotarSpeed
    {
        get { return _rotarSpeed; }
        set { _rotarSpeed = Mathf.Clamp(value,0,3000); }
    }

    private float rotateDegree;      /*!< la rotation en degré qui est appliquée au rotor */
    private Vector3 OriginalRotate;  /*!< Orientation initiale du rotor */

    private void Start ()
	{
        OriginalRotate = transform.localEulerAngles;
	}

    /**
     * \brief Cette fonction applique une rotation au rotor sur un axe choisi préalablement
     */
    private void Update ()
	{
        rotateDegree += RotarSpeed * Time.deltaTime;
	    rotateDegree = rotateDegree%360;

		switch (RotateAxis)
		{
		    case Axis.Y:
		        transform.localRotation = Quaternion.Euler(OriginalRotate.x, rotateDegree, OriginalRotate.z);
		        break;
		    case Axis.Z:
		        transform.localRotation = Quaternion.Euler(OriginalRotate.x, OriginalRotate.y, rotateDegree);
		        break;
		    default:
		        transform.localRotation = Quaternion.Euler(rotateDegree, OriginalRotate.y, OriginalRotate.z);
		        break;
		}
	}
}
