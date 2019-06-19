using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuberotation : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    float y=-90;
    float z;
    float x=-90;
    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(x, y, z);
    }

    public void AdjustSpeed(float newSpeed)
    {
        y = newSpeed;
    }
    public void AdjustSpeed2(float newSpeed)
    {
        z = newSpeed;
    }
    public void AdjustSpeed3(float newSpeed)
    {
        x = newSpeed;
    }
}
