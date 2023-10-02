using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb = null;
    public float jumpForce = 10f;
    public float gravityModifier;
    
    //Boolean tracking if player is on groung
    public bool isOnGround = true;

    //Animation
    private Animator playerAnim;

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        { 
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

}
