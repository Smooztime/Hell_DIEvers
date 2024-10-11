using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.U2D.IK;

public class ActiveRagDoll : MonoBehaviour
{
    [SerializeField] private bool isRagDoll;
    [SerializeField] private HingeJoint2D[] jointHinge;
    [SerializeField] private Rigidbody2D[] jointRB;
    [SerializeField] private Collider2D[] jointCol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RagDollActive()
    {
        if(isRagDoll)
        {
            transform.GetComponent<Animator>().enabled = false;
            transform.GetComponent<IKManager2D>().enabled = false;
            transform.GetComponent<PlayerInputController>().enabled = false;
            transform.GetComponent<PlayerController>().enabled = false;
            transform.GetComponent<Rigidbody2D>().simulated = false;
            transform.GetComponent<Collider2D>().enabled = false;

            foreach(HingeJoint2D hinge in jointHinge)
            {
                hinge.enabled = true;
            }

            foreach(Rigidbody2D rb in jointRB)
            {
                rb.simulated = true;
            }

            foreach(Collider2D col in jointCol)
            {

                col.enabled = true;
            }
        }
        else
        {
            transform.GetComponent<Animator>().enabled = true;
            transform.GetComponent<IKManager2D>().enabled = true;
            transform.GetComponent<PlayerInputController>().enabled = true;
            transform.GetComponent<PlayerController>().enabled = true;
            transform.GetComponent<Rigidbody2D>().simulated = true;
            transform.GetComponent<Collider2D>().enabled = true;
            foreach (HingeJoint2D hinge in jointHinge)
            {
                hinge.enabled = false;
            }

            foreach (Rigidbody2D rb in jointRB)
            {
                rb.simulated = false;
            }

            foreach (Collider2D col in jointCol)
            {

                col.enabled = false;
            }
        }
    }

    public void SetActiveRagDoll(bool value)
    {
        isRagDoll = value;
        RagDollActive();
    }

    public bool IsRagDoll { get { return isRagDoll; } }
}
