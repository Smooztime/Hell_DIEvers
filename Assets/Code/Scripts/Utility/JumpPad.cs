using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float iBelieveICanFly;
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
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            SoundManager.PlaySound(SoundType.JumpPad, 0.1f);
            collision.gameObject.GetComponent<ActiveRagDoll>().IbelieveICanFly(iBelieveICanFly);
            

            Debug.Log("Bounce!");
        }
    }
}
