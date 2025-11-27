using System.Collections;

using UnityEngine;

public class DollRaycast : MonoBehaviour
{
    public PlayerMovment playerMovment;
    public LayerMask Target;
    public LayerMask Obstacle;
    public GameObject player;
    [Range(0, 360)] public float viewAngle;
    public float viewDistance;
    public int timer;
    public float countdown;
    public bool isRed = false;


    private bool PlayerInSight = false;
    void Start()
    {
        timer = Random.Range(3, 7);
        playerMovment = player.GetComponent<PlayerMovment>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

    }
    public IEnumerator FOVRoutine()
    {

        while (true)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            countdown = (int)Time.time + timer;
            isRed = false;
            while ((int)Time.time < countdown)
            {

                yield return null;
            }
            timer = Random.Range(3, 7);
            GetComponent<SpriteRenderer>().color = Color.red;
            countdown = (int)Time.time + timer;
            isRed = true;
            while ((int)Time.time < countdown)
            {

                FieldOfViewCheck();

                yield return null;
            }
            timer = Random.Range(3, 7);

        }

    }
    private void FieldOfViewCheck()
    {

        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, viewDistance, Target);

        if (rangeCheck.Length > 0)
        {

            Transform Target = rangeCheck[0].transform;
            Vector2 directionToTarget = (Target.position - transform.position).normalized;
            if (Vector2.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {

                float distanceToTarget = Vector2.Distance(transform.position, Target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, Obstacle))
                {

                    PlayerInSight = true;
                    if (playerMovment.isMoving == true)
                    {
                        Debug.Log("Player caught");
                    }
                }
                else
                {
                    PlayerInSight = false;
                }

            }
            else
            {
                PlayerInSight = false;
            }


        }
        else if (PlayerInSight)
        {
            PlayerInSight = false;

        }
    }

}
