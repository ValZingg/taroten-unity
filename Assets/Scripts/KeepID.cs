using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepID : MonoBehaviour
{
    public int ID; //identifiant de la run

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject); //Empèche de se faire détruire au changement de scène
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
