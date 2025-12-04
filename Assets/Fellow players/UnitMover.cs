using UnityEngine;
using System.Collections;
using UnityEditor;
public class UnitMover : MonoBehaviour
{


    CanvasCountDown canvasCountDown;
    DollRaycast DollRaycast;
    private int survivalChance;
    private Rigidbody2D rb;

    private int time;
    private int Rng;
    public int speed;
    private float unitVelocity;
    private float timer;
    private bool surived = false;

    private float redtimer;

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

    }
    private IEnumerator MightMoveWhileRedLight()
    {
        while (true)
        {
            redtimer = DollRaycast.countdown - 1;
            survivalChance = Rng;

            if (survivalChance <= 3 && DollRaycast.isRed == true)
            {
                Debug.Log("AAAAAAAAAAAAAAAAA");
                Destroy(this);
                yield return null;
            }
            else if (survivalChance > 3 && DollRaycast.isRed == true)
            {
                while (redtimer > 0)
                {
                    StartCoroutine(AddingForce());
                    if (unitVelocity > 0)
                    {
                        Destroy(this);
                        yield return null;
                    }
                }
                yield return null;
            }
            redtimer = DollRaycast.countdown - 1;
            if (DollRaycast.isRed == false)
            {
                while (redtimer > 0)
                {
                    StartCoroutine(AddingForce());
                    yield return null;
                }
            }
            yield return null;
        }

    }

    private IEnumerator AddingForce()
    {
        float AddingTimer = 2 + Time.time;
        while (true)
        {
            rb.AddForce(transform.up * speed);
            if (AddingTimer < 0)
            {
                break;
            }
            AddingTimer = -Time.time;
        }
        yield return new WaitForSeconds(1);
        StopCoroutine(AddingForce());
    }
    private IEnumerator GlobalDeath()
    {
        Debug.Log("wow");
        while (true)
        {

            if (canvasCountDown.globaTimmer == 0 && surived == true)
            {

            }
            else if (canvasCountDown.globaTimmer == 0)
            {
                Destroy(this.gameObject);
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
