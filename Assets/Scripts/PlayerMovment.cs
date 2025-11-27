using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{


    public float speed = 1f;
    public Rigidbody2D rb;
    public Vector2 movement;
    CanvasCountDown canvasCountDown;

    private bool surived = false;
    private float playerVelocity;
    public bool isMoving;


    void Awake()
    {
        canvasCountDown = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasCountDown>();
        StartCoroutine(GlobalDeath());
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerVelocity = rb.linearVelocity.magnitude;

        if (Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(0, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(0, -speed);
        }

        if (playerVelocity > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    private IEnumerator GlobalDeath()
    {

        while (true)
        {

            if (canvasCountDown.globaTimmer == 0 && surived == true)
            {

            } else if(canvasCountDown.globaTimmer == 0) {
                                Destroy(this.gameObject);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("nice");
        if (collision.gameObject.tag == "line")
        {
            Debug.Log("real");
            surived = true;
        }
    }



}

