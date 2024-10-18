using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    [SerializeField] private bool isTrap;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool reverse;
    [SerializeField] private float knockBack = 5000;
    [SerializeField] private AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(isTrap);
    }

    // Update is called once per frame
    void Update()
    {
        RotateWheel();
    }

    private void RotateWheel()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (!reverse)
        {
            sprite.flipX = false;
            transform.Rotate(0, 0, 1 * rotateSpeed * Time.deltaTime);
        }
        else if (isTrap && reverse)
        {
            sprite.flipX = true;
            transform.Rotate(0, 0, -1 * rotateSpeed * Time.deltaTime);
        }
        else
        {
            sprite.flipX = false;
            transform.Rotate(0, 0, -1 * rotateSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTrap)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                //Player knock back
                SoundManager.PlaySound(SoundType.KnockBack);
                Debug.Log("Knock back player");
                collision.gameObject.GetComponent<ActiveRagDoll>().KnockBack(knockBack,transform.position);
            }
            if(collision.gameObject.transform.root.gameObject.GetComponent<PlayerController>())
            {
                Debug.Log("Help");
                collision.gameObject.transform.root.gameObject.GetComponent<ActiveRagDoll>().KnockBack(knockBack,transform.position, true);
            }
        }
    }
}
