using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float speedRun;
    public float speedSprint;
    public float rotateSpeed;

    [Header("Animation Settings:")]
    public Animator anim;

    float speed;
    Rigidbody rb;

    float xRaw;
    float zRaw;

    float x;
    float z;

    bool onTheGround;
    public static bool mov;
    public bool movi;
    public bool ifDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = speedRun;
        onTheGround = true;
        mov = true;
        ifDamage = true;
    }

    private void Update()
    {
        movi = mov;

        Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView, 50f, .1f);

    }

    private void FixedUpdate()
    {
        xRaw = Input.GetAxisRaw("Horizontal");
        zRaw = Input.GetAxisRaw("Vertical");

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


        if (mov == true)
        {
            if (onTheGround == true)
            {
                Animation();
            }
            Jump();
            Movimentation();
            Rotation();
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
        }
    }

    public void DisableAttack()
    {
        anim.SetBool("attack", false);
    }

    void Movimentation()
    {
        if (xRaw != 0 || zRaw != 0)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * transform.forward);
        }
    }

    void Rotation()
    {
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        if (zRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        }
        if (zRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);
        }
        if (xRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * 5);
        }
        if (xRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 90, 0), Time.deltaTime * 5);
        }
    }

    void Animation()
    {
        if (!xRaw.Equals(0) || !zRaw.Equals(0))
        {
            anim.SetBool("running", true);   
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    void Jump()
    {
        if (onTheGround == true && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * 4, ForceMode.Impulse);
            onTheGround = false;
        }
        if (onTheGround == false)
        {
            anim.SetBool("jump", true);
            anim.SetBool("running", false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onTheGround = true;
            anim.SetBool("jump", false);
        }
    }
}
