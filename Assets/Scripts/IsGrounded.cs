using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField]
    LayerMask layer;

    public bool amIGrounded;
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 15f;




    // Update is called once per frame
    void Update()
    {
        if (!Player.gameStarded)
        {
            return;
        }
        //Raycast i?lemi
        RaycastHit2D isHitting = Physics2D.Raycast(transform.position,Vector2.down,0.15f,layer);
        if (isHitting.collider != null)
        {
            amIGrounded = true;
        }
        else
        {
            amIGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && amIGrounded ==true)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }


        //collider'da çarp??malar?n alg?lanmas? için iki nesnede de collider olmal? ve ii nesneden birinde rigidbody olmal?
        //rigidbody'de çarp??malar?n alg?lanmas? için iki objede de rigidbody olmal?d?r.
    }
}
