using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class moveableObject : MonoBehaviour
{
    public float pushForce = 1;
    public float MAXDIS;
    Animator anim;
    public LayerMask layer;
    bool turn = false;
    int isPushHash;
    private ThirdPersonController thirdPersonController;

    void Start()
    {
        anim = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        isPushHash = Animator.StringToHash("Push");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(turn)
            {
                anim.SetBool(isPushHash, false);  
                turn = false;
            }
            else{
                anim.SetBool(isPushHash, true);
                turn = true;
            }
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rigg = hit.collider.attachedRigidbody;

        if(_rigg != null)
        {
            if(turn == true)
            {             
                Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                _rigg.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
            } 
        }
        
    }
}
