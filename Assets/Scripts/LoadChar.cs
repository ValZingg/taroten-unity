using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChar : MonoBehaviour
{
    public string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        lines = System.IO.File.ReadAllLines("Assets/Text/Maya.txt"); //Gets all lines
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
