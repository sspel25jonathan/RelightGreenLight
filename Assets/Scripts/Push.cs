using System.Collections;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Push : MonoBehaviour
{

    [Range(0, 360)] public float angle;
    public float distance;
    Vector2 direction;
    PlayerMovment playerMovment;
    public LayerMask target;
    private Vector2 origin;
    void Start()
    {
        playerMovment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        origin = transform.forward;
        float angleInRadians = angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Pushing());
        }
    }

    private IEnumerator Pushing()
    {

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, angle, transform.position);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.rigidbody && hit.collider.gameObject.tag == "Player")
            {


                Debug.Log(hit.transform.name);
                Vector2 pushDirection = (hit.transform.position - transform.position).normalized;
                hit.rigidbody.AddForce(pushDirection * 100);
                Debug.Log(pushDirection);
            }
        }

        yield return null;
    }
}
