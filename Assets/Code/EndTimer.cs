using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    [SerializeField] GameObject timerOBJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            timerOBJ.GetComponent<Timer>().timerRunning = false;
        }
    }
}
