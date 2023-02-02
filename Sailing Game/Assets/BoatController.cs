using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private Transform boomPivot;
    // Start is called before the first frame update
    [SerializeField] private float boomRotationSpeed = 10f;
    [SerializeField] private float boatRotationSpeed = 10f;
    
    public Rigidbody2D rb;
    
    public bool isPortTack = false;
    
    [SerializeField] private float maxAngleMagnitude = 90f;
    [SerializeField] private float minAngleMagnitude = 0f;

    [SerializeField] private Transform windIndicator;
    [SerializeField] private float windStrengthScaleFactor = 10f;

    
    
    public float windAngle;
    public float windStrength;
    
    private float deltaBoomRotationChange = 0f;
    private float deltaBoatRotationChange = 0f;

    private float maxAngle;
    private float minAngle;
    
    private float boomAngle = 0f;
    private float boatAngle = 0f;
    void Start()
    {
        maxAngle = maxAngleMagnitude;
        minAngle = minAngleMagnitude;
        boomAngle = boomPivot.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (windAngle > 360)
        {
            windAngle -= 360;
        }
        else if (windAngle < 0)
        {
            windAngle += 360;
        }
        windIndicator.rotation = Quaternion.Euler(0,0,windAngle);
        windIndicator.localScale = new Vector3(1,windStrength/windStrengthScaleFactor,1);
        int boomAngleSign = (isPortTack) ? 1 : -1;
        deltaBoomRotationChange = Input.GetAxisRaw("Vertical") * boomRotationSpeed  * boomAngleSign;
        
        deltaBoatRotationChange = Input.GetAxisRaw("Horizontal") * boatRotationSpeed; 
    }

    
    private void FixedUpdate()
    {
        /*float windAngleRadians = windAngle * Mathf.Deg2Rad;
        rb.velocity = new Vector2(-Mathf.Sin(windAngleRadians),Mathf.Cos(windAngleRadians)) * windStrength;*/
        float angleDifference = windAngle - boatAngle;
        if (angleDifference > 360)
        {
            angleDifference -= 360;
        }
        else if (angleDifference < 0)
        {
            angleDifference += 360;
        }

        isPortTack = angleDifference > 180;

        if (isPortTack)
        {
            maxAngle = maxAngleMagnitude;
            minAngle = 0;
            
        }
        else
        {
            maxAngle = 0;
            minAngle = -maxAngleMagnitude;
        }

        if (deltaBoomRotationChange != 0)
        {
            boomAngle += deltaBoomRotationChange * Time.fixedDeltaTime;
        }
        if (deltaBoatRotationChange != 0)
        {
            boatAngle += deltaBoatRotationChange * Time.fixedDeltaTime;
            if (boatAngle > 360)
            {
                boatAngle -= 360;
            }
            else if (boatAngle < 0)
            {
                boatAngle += 360;
            }
        }
        
        if (boomAngle > maxAngle)
        {
            boomAngle = maxAngle;
        }
        else if (boomAngle < minAngle)
        {
            boomAngle = minAngle;
        }
        
        transform.rotation = Quaternion.Euler(0,0,boatAngle);
        boomPivot.localRotation = Quaternion.Euler(0,0,boomAngle);
        
    }
}
