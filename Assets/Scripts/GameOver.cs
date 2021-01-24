using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(GameObject.Find("DataTracker"));
        GameObject.Destroy(GameObject.Find("ID_Keeper"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
