using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public int chairAmount = 1;
    public GameObject chair;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < chairAmount; i++) 
        {
            SpawnChair();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnChair()
    {
        var x = Random.Range(-10, 10);
        var y = Random.Range(-10, 10);

        Instantiate(chair, new Vector3 (x,y,0), Quaternion.identity);
        Debug.Log("Chair Spawned");
    }
}
