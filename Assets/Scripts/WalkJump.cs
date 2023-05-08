using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10.0F;
    public float jumpspeed = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 atas = new Vector3(0, 100, 0);
            rb.AddForce(atas * speed, ForceMode2D.Impulse);
        }
    }
}
