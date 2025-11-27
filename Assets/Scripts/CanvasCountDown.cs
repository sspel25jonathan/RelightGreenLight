using UnityEngine;


using TMPro;
using System.Collections;
public class CanvasCountDown : MonoBehaviour
{

    [SerializeField] float minutes;
    private float countText;

    public TMP_Text countDownText;
    public TMP_Text GlobalCountdown;
    public int globaTimmer;
    private int hiddenTimer;
    public DollRaycast dollRaycast;
    void Start()
    {
        hiddenTimer = globaTimmer;
        dollRaycast = GameObject.FindGameObjectWithTag("Doll").GetComponent<DollRaycast>();
        StartCoroutine(Globa());

    }

    // Update is called once per frame
    void Update()
    {
        countText = dollRaycast.countdown - (int)Time.time;
        countDownText.text = countText.ToString();
        minutes = -Time.deltaTime;

        GlobalCountdown.text = globaTimmer.ToString();
        Debug.Log(globaTimmer);

    }

    private IEnumerator Globa()
    {
        while (true)
        {
            globaTimmer = hiddenTimer - (int)Time.time;

            if (globaTimmer < 0)
            {
                globaTimmer = 0;
                
                StopCoroutine(Globa());
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

}