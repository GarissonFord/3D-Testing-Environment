using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    //From GamesPlusJames tutorial
    public float moveSpeed;
    //public Rigidbody rb;
    public float jumpForce;
    public CharacterController controller;

    public Vector3 moveDirection;
    public float gravityScale;

    public float h, v;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0.0f, Input.GetAxis("Vertical") * moveSpeed);

        /*
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        */

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        isGrounded = controller.isGrounded;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * v) + (transform.right * h);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0.0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                //controller.attachedRigidbody.AddForce(transform.up * jumpForce);
            }
        }

        moveDirection.y = moveDirection.y + Physics.gravity.y * gravityScale;
        controller.Move(moveDirection * Time.deltaTime);

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) * Mathf.Abs(Input.GetAxis("Horizontal")));
    }
}