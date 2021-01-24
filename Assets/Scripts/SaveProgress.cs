using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class SaveProgress : MonoBehaviour
{
    public GameObject DataTracker;
    public GameObject IDTracker;

    private string[] lines;


    // Start is called before the first frame update
    void Start()
    {
        IDTracker = GameObject.Find("ID_Keeper");
        DataTracker = GameObject.Find("DataTracker");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveprogressnow()
    {
        string filepath = IDTracker.GetComponent<KeepID>().ID.ToString() + ".tRUN";
        int level_to_load = DataTracker.GetComponent<Player>().current_level;
        lines = System.IO.File.ReadAllLines(filepath);

        int i = 0;

        // !!! ici nous cherchons chaque ligne ou débute les données du niveau, ce qui nous permet de séparer les niveaux
        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        int level1_line = i; //Les données du niveau 1 à partir de cette ligne

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        int level2_line = i; //Les données du niveau 1 à partir de cette ligne

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        int level3_line = i; //Les données du niveau 1 à partir de cette ligne

        int startingline = 0;
        switch (level_to_load) //Pour savoir de où il faut partir.
        {
            case 1:
                startingline = level1_line;
                break;

            case 2:
                startingline = level2_line;
                break;

            case 3:
                startingline = level3_line;
                break;
        }

        int LineToChange = startingline + DataTracker.GetComponent<Player>().current_spot;
        int LineToChange2 = startingline + DataTracker.GetComponent<Player>().last_spot;
        lines[LineToChange] = "room" + DataTracker.GetComponent<Player>().current_spot.ToString() + "=1,done=true";      
        if(DataTracker.GetComponent<Player>().last_spot == 0) lines[LineToChange2] = "room" + DataTracker.GetComponent<Player>().last_spot.ToString() + "=6,done=true";
        else lines[LineToChange2] = "room" + DataTracker.GetComponent<Player>().last_spot.ToString() + "=0,done=true";
        System.IO.File.WriteAllLines(filepath, lines);
    }
}
