using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public GameObject IDKeeper; //L'objet qui garde L'ID

    private string[] lines;

    public int level_to_load;

    private int level1_line;
    private int level2_line;
    private int level3_line;

    // Start is called before the first frame update
    void Start()
    {
        IDKeeper = GameObject.Find("ID_Keeper");
        string filename = IDKeeper.GetComponent<KeepID>().ID + ".tRUN"; //récupère l'emplacement du fichier de sauvegarde
        lines = System.IO.File.ReadAllLines(filename); //Lis toute les lignes

        //================= CHARGEMENT DES DONNEES DE BASE ===================
        level_to_load = Int32.Parse(lines[1].Substring(16 + 1)); //Quel niveau charger?

        //================= CHARGEMENT ET AFFICHAGE DU NIVEAU =================

        int i = 0;

        // !!! ici nous cherchons chaque ligne ou débute les données du niveau, ce qui nous permet de séparer les niveaux
        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level1_line = i; //Les données du niveau 1 à partir de cette ligne
        Debug.Log("Level 1 found at line " + level1_line);

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level2_line = i; //Les données du niveau 1 à partir de cette ligne
        Debug.Log("Level 2 found at line " + level2_line);

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level3_line = i; //Les données du niveau 1 à partir de cette ligne
        Debug.Log("Level 3 found at line " + level3_line);

        //AFFICHAGE DU NIVEAU
        switch (level_to_load)
        {
            case 1:
                LoadAndShowLevel(1);
                break;

            case 2:
                LoadAndShowLevel(2);
                break;

            case 3:
                LoadAndShowLevel(3);
                break;
        }



    }

    void LoadAndShowLevel(int level)
    {
        int startingline = 0;
        switch(level) //Pour savoir de où il faut partir.
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
        //==================================
        List<int> Rooms = new List<int>();
        for(int i = startingline;i < startingline + 14;i++) //met toutes les salles dans une liste
        {
            Debug.Log((lines[i].Substring(5 + i.ToString().Length,1)));
            Rooms.Add(Int32.Parse(lines[i].Substring(5 + i.ToString().Length,1)));
            Debug.Log("Room " + i + " = type " + Rooms[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
