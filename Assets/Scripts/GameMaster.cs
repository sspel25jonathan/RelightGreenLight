using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public int UnitCount;
    public GameObject UnitPrefab;

    void Start()
    {
        for(int i  = 0; i < UnitCount; i++)
        {
            Instantiate(UnitPrefab, new Vector2(Random.Range(-20,20), -20), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
