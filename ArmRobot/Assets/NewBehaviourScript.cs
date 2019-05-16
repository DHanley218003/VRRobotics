using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public float speed = 1f;
    public GameObject ObjectToRotate;

    public void RotateMyObject()
    {
        float sliderValue = GetComponent<Slider>().value;
        ObjectToRotate.transform.Rotate(sliderValue * speed * Time.deltaTime, 0, 90);
    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
