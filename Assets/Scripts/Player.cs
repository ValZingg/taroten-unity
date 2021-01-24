using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    //===================================
    public string char_name;

    public int current_level;
    public int current_spot;
    public int last_spot;

    public int HP;
    public int max_HP;

    public int ATK;

    public int SHIELD;

    public bool Firstload;

    public List<string> Cards;


    public string PathChosen;
    //===================================


    // Start is called before the first frame update
    void Start()
    {
        Firstload = false;
        HP = 0;
        ATK = 0;
        SHIELD = 0;
        PathChosen = "Aucun";
        current_level = 1;
        current_spot = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject); //Empèche de se faire détruire au changement de scène
    }

    public void SetTrajectory(string Trajectory)
    {
        PathChosen = Trajectory;
    }

    public void LoadDefaultCards(string charname)
    {
        string filepath = "Assets/Text/" + charname + ".txt";
        string[] lines = System.IO.File.ReadAllLines(filepath); //Gets all lines

        string startcards_raw = lines[3].Substring(lines[3].IndexOf('=') + 1);
        Cards = startcards_raw.Split(',').ToList<string>();
    }

    public void GetCharName()
    {
        string ID = GameObject.Find("ID_Keeper").GetComponent<KeepID>().ID.ToString();
        string filepath = ID + ".tRUN";
        string[] lines = System.IO.File.ReadAllLines(filepath); //Gets all lines
        char_name = lines[2].Substring(lines[2].IndexOf('=') + 1);
    }
}
