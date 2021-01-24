using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string name;

    public int HP;
    public int max_HP;

    public int SHIELD;

    public int ATK;
    public int DEF;

    private string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEnemy(string enemyname)
    {
        string filepath = "Assets/Text/Enemies_ind/" + enemyname + ".txt";
        lines = System.IO.File.ReadAllLines(filepath);

        //nom
        name = lines[0].Substring(lines[0].IndexOf('=') + 1);

        //pv
        max_HP = Int32.Parse(lines[1].Substring(lines[1].IndexOf('=') + 1));
        HP = max_HP;

        //atk & def
        ATK = Int32.Parse(lines[2].Substring(lines[2].IndexOf('=') + 1));
        DEF = Int32.Parse(lines[3].Substring(lines[3].IndexOf('=') + 1));
    }
}
