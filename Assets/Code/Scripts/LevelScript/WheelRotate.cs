using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateWheel();
    }

    private void RotateWheel()
    {
        wheel.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
