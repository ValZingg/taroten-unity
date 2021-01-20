using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject IDKeeper; //L'objet qui garde L'ID

    private string[] lines;
    public string PlayerName;

    public int level_to_load;

    private int level1_line;
    private int level2_line;
    private int level3_line;

    public GameObject buttonPrefab;


    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //récupère le canvas
        IDKeeper = GameObject.Find("ID_Keeper");
        string filename = IDKeeper.GetComponent<KeepID>().ID + ".tRUN"; //récupère l'emplacement du fichier de sauvegarde
        lines = System.IO.File.ReadAllLines(filename); //Lis toute les lignes

        //================= CHARGEMENT DES DONNEES DE BASE ===================
        level_to_load = Int32.Parse(lines[1].Substring(16 + 1)); //Quel niveau charger?
        PlayerName = lines[2].Substring(lines[2].IndexOf('=') + 1);

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
            Rooms.Add(Int32.Parse(lines[i].Substring(lines[i].IndexOf('=') + 1, 1)));
        }

        //Maintenant que nous avons toutes les salles, il faut les afficher

        //positions
        float start_x = -550.0f;
        float start_y = 150.0f;
        int amountdone = 0;
        for(int i = 0;i < Rooms.Count;i++)
        {
            GameObject LastButtonMade = Instantiate(buttonPrefab); //crée une case
            LastButtonMade.transform.SetParent(canvas.transform);// le met dans le canvas, sinon il ne s'affichera pas

            //Est-ce que la salle est completée ?
            //TODO

            //Ré-ajustation de la taille
            LastButtonMade.GetComponent<RectTransform>().sizeDelta = new Vector2(122, 118);
            LastButtonMade.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

            //Position
            LastButtonMade.transform.localPosition = new Vector3(start_x,start_y,0);
            if (Rooms[i] != 5 && Rooms[i] != 1)
            {
                start_x += 215;
                amountdone++;
            }
            if (amountdone >= 6) //saut de ligne
            {
                start_x = -550.0f;
                start_y -= 150.0f;
                amountdone = 0;
            }

            //Icone du bouton
            switch(Rooms[i]) //change l'icone suivant quel type de salle c'est
            {
                case 1: //Entrée joueur
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 1;
                    //L'entrée du joueur est toujours à gauche de l'écran
                    LastButtonMade.transform.localPosition = new Vector3(-700.0f, 50.0f, 0);
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = true;
                    break;

                case 2: //Ennemi normal
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 2;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Skool");     
                    break;

                case 3: //Trésor
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 3;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Chest");
                    break;

                case 4: //Ennemi élite
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 4;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/RedSkool");
                    break;

                case 5: //Boss et sortie
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 5;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/BossSkool");
                    //Le boss est toujours tout à droite de l'écran
                    LastButtonMade.transform.localPosition = new Vector3(700.0f, 50.0f, 0);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
