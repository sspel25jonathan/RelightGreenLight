using UnityEngine;
using System.Collections;
using UnityEditor;
using Unity.VisualScripting;
public class UnitMover : MonoBehaviour
{
    CanvasCountDown canvasCountDown;
    DollRaycast DollRaycast;
    private int survivalChance;
    private Rigidbody2D rb;

    private int Rng;
    public float speed;
    private float unitVelocity;
    private bool unitMoveing = false;
    private bool surived = false;


    public GameObject unit;
    private bool red;
    private bool speeder = true;
    private float timer3;

    void Start()
    {

        DollRaycast = GameObject.FindGameObjectWithTag("Doll").GetComponent<DollRaycast>();
        canvasCountDown = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasCountDown>();
        rb = GetComponent<Rigidbody2D>();
        unitVelocity = rb.linearVelocity.magnitude;
        Rng = Random.Range(1, 7);
        StartCoroutine(MightMoveWhileRedLight());
        StartCoroutine(GlobalDeath());
    }

    void Update()
    {
        unitVelocity = rb.linearVelocity.magnitude;
        red = DollRaycast.isRed;

        Debug.Log(timer3);
    }

   
    private IEnumerator MightMoveWhileRedLight()
    {
        while (true)
        {

            survivalChance = Rng;

            rb.linearVelocity = Vector3.zero;
            if (survivalChance <= 3 && red == true)
            {
                while (DollRaycast.countdown > 0 && red == true)
                {
                    Debug.Log("Dead");
                    yield return null;
                }

            }
            else if (survivalChance > 3 && red == true)
            {
                while (DollRaycast.countdown > 0 && red == true)
                {
                    rb.linearVelocity = Vector3.zero;
                    if (unitVelocity >= 0  && DollRaycast.isRed == true)
                    {
                        Debug.Log("Dead2");
                        yield return null;
                    }
                    yield return null;

                }
                yield return null;
            }
            speeder = true;
            if (red == false)
            {
                
                while (DollRaycast.countdown >= 0 && red == false)
                {
                    if (speeder == true)
                    {
                        StartCoroutine(AddingForce());
                        speeder = false;
                    }

                    yield return null;
                }
            }
            yield return null;
        }
    }

    private IEnumerator AddingForce()
    {
        timer3 = 1;
        while (timer3 > 0)
        {
            rb.AddForce(transform.up * speed);
            yield return null;

            timer3 = timer3 - Time.deltaTime;
            
        }

    }
    private IEnumerator GlobalDeath()
    {

        while (true)
        {

            if (canvasCountDown.globaTimmer == 0 && surived == true)
            {

            }
            else if (canvasCountDown.globaTimmer == 0)
            {
                Destroy(this);
            }
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "line")
        {

            surived = true;
        }
    }

}
