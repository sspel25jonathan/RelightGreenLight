using UnityEngine;
using System.Collections;
using UnityEditor;
public class UnitMover : MonoBehaviour
{


    CanvasCountDown canvasCountDown;
    DollRaycast DollRaycast;
    private int SurvivalChance;
    private Rigidbody2D rb;

    private int time;
    private int Rng;
    public int speed;
    private float unitVelocity;
    private float timer;

    void Start()
    {
        DollRaycast = GameObject.FindGameObjectWithTag("Doll").GetComponent<DollRaycast>();
        canvasCountDown = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasCountDown>();

        rb = GetComponent<Rigidbody2D>();
        Rng = Random.Range(1, 6);
        unitVelocity = rb.linearVelocity.magnitude;

        StartCoroutine(ChangeSpeed());
        StartCoroutine(MightMoveWhileRedLight());
        StartCoroutine(GlobalDeath());
    }

    // Update is called once per frame
    void Update()
    {


    }

    private IEnumerator ChangeSpeed()
    {
        time = Rng;
        yield return new WaitForSeconds(time);
        speed = Rng;
    }


    private IEnumerator MightMoveWhileRedLight()
    {

        while (true)
        {
            SurvivalChance = Rng;
            if (SurvivalChance < 3 && DollRaycast.isRed == true)
            {
                timer = DollRaycast.timer - 1;
                StartCoroutine(AddingForce());
                if (timer > 0)
                {
                    timer = -timer - Time.time;
                }
                rb.linearVelocity = Vector2.zero;
                yield return null;
            }
            else
            {
                while (DollRaycast.isRed == true)
                {
                    rb.linearVelocity = Vector2.zero;
                    yield return null;
                }
                yield return null;
            }
            while (DollRaycast.isRed == false)
            {
                timer = DollRaycast.timer - 1;
                StartCoroutine(AddingForce());
                if (timer > 0)
                {
                    timer = -timer - Time.time;
                }
                rb.linearVelocity = Vector2.zero;
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator GlobalDeath()
    {
        while (true)
        {
            
        if (canvasCountDown.globaTimmer == 0)
        {
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator checkingunitVecloity()
    {
        if (unitVelocity > 0 && DollRaycast.isRed == true)
        {
            Destroy(this);
        }
        yield return null;
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
            AddingTimer =- Time.time;
        }
        yield return new WaitForSeconds(1);
        StopCoroutine(AddingForce());
    }
}
