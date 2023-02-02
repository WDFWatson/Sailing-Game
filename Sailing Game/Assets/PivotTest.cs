using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTest : MonoBehaviour
{
    [SerializeField] private Transform windDirectionIndicator, boatDirectionIndicator;
    [SerializeField] private float windDirection, boatDirection;

    private float angleDifference;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (windDirection > 360)
        {
            windDirection -= 360;
        }
        else if (windDirection < 0)
        {
            windDirection += 360;
        }
        
        if (boatDirection > 360)
        {
            boatDirection -= 360;
        }
        else if (boatDirection < 0)
        {
            boatDirection += 360;
        }
        
        windDirectionIndicator.rotation = Quaternion.Euler(0,0,windDirection);
        boatDirectionIndicator.rotation = Quaternion.Euler(0,0,boatDirection);

        angleDifference = windDirection - boatDirection;
        
        if (angleDifference > 360)
        {
            angleDifference -= 360;
        }
        else if (angleDifference < 0)
        {
            angleDifference += 360;
        }
        
        Debug.Log(angleDifference);
    }
}
