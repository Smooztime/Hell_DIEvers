using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDrop : MonoBehaviour
{
    [SerializeField] bool doFall;
    [SerializeField] HingeJoint2D joint;
    Vector3 bladePos;
    // Start is called before the first frame update
    void Start()
    {
        if (doFall)
        {
            GetComponent<SpriteRenderer>().color = new Color(.8f, .9f, .8f);
        }
      bladePos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position = bladePos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (doFall) 
        {
            SoundManager.PlaySound(SoundType.BladeDrop);
            joint.enabled = false;
        }
    }
}
