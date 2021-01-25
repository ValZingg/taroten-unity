using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject IDKeeper; //L'objet qui garde L'ID
    public GameObject DataTracker;

    private string[] lines;
    public string PlayerName;

    public int PlayerPos;

    public int level_to_load;

    private int level1_line;
    private int level2_line;
    private int level3_line;

    public GameObject buttonPrefab;

    //===UI
    public Canvas canvas;
    public GameObject PlayerIMG;
    public GameObject PlayerTEXT;
    public GameObject PlayerHP_TEXT;

    public GameObject level_text;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //récupère le canvas
        IDKeeper = GameObject.Find("ID_Keeper");
        DataTracker = GameObject.Find("DataTracker");

        PlayerIMG = GameObject.Find("CharImage");
        PlayerTEXT = GameObject.Find("PlayerName");
        PlayerHP_TEXT = GameObject.Find("PlayerHP");
        level_text = GameObject.Find("LevelText");

        string filename = IDKeeper.GetComponent<KeepID>().ID + ".tRUN"; //récupère l'emplacement du fichier de sauvegarde
        lines = System.IO.File.ReadAllLines(filename); //Lis toute les lignes

        PlayerPos = DataTracker.GetComponent<Player>().current_spot; //position du joueur

        //================= CHARGEMENT DES DONNEES DE BASE ===================
        level_to_load = Int32.Parse(lines[1].Substring(16 + 1)); //Quel niveau charger?
        PlayerName = lines[2].Substring(lines[2].IndexOf('=') + 1);
        DataTracker.GetComponent<Player>().char_name = PlayerName;

        string[] linesplayer = System.IO.File.ReadAllLines("Assets/Text/" + PlayerName + ".txt");
        if(DataTracker.GetComponent<Player>().Firstload == false)
        {
            DataTracker.GetComponent<Player>().max_HP = Int32.Parse(linesplayer[2].Substring(linesplayer[2].IndexOf('=') + 1)); //récupère les hp max du perso
            DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
            DataTracker.GetComponent<Player>().Firstload = true;
        }

        //AFFICHAGE DES STATS SUR UI
        PlayerIMG.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + DataTracker.GetComponent<Player>().char_name + "_portrait");
        PlayerTEXT.GetComponent<Text>().text = DataTracker.GetComponent<Player>().char_name;
        PlayerHP_TEXT.GetComponent<Text>().text = "PV : " + DataTracker.GetComponent<Player>().HP.ToString() + " / " + DataTracker.GetComponent<Player>().max_HP.ToString();
        level_text.GetComponent<Text>().text = "Niveau : " + DataTracker.GetComponent<Player>().current_level.ToString();


        //================= CHARGEMENT ET AFFICHAGE DU NIVEAU =================

        int i = 0;

        // !!! ici nous cherchons chaque ligne ou débute les données du niveau, ce qui nous permet de séparer les niveaux
        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level1_line = i; //Les données du niveau 1 à partir de cette ligne

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level2_line = i; //Les données du niveau 1 à partir de cette ligne

        while (lines[i][0] != '-') i++; //Tant que la ligne ne commence pas avec un - (Séparateur), On continue de chercher
        i++; //Pour arrondir
        level3_line = i; //Les données du niveau 1 à partir de cette ligne

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
            LastButtonMade.GetComponent<ExploreSpot>().ID = i;

            //Est-ce que la salle est completée ?
            bool Is_done = bool.Parse(lines[startingline + i].Substring(lines[startingline + i].IndexOf(',') + 6));          
            if (Is_done) LastButtonMade.GetComponent<ExploreSpot>().Explored = true;
            //TODO

            //Ré-ajustation de la taille
            LastButtonMade.GetComponent<RectTransform>().sizeDelta = new Vector2(122, 118);
            LastButtonMade.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

            //Est-ce que le joueur peut y aller ?
            if (i == PlayerPos + 1 || i == PlayerPos + 7) LastButtonMade.GetComponent<ExploreSpot>().Can_Move_Here = true; //oui le joueur peut aller là

            //Position
            LastButtonMade.transform.localPosition = new Vector3(start_x,start_y,0);
            if (Rooms[i] != 5 && Rooms[i] != 6)
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
                case 0: //case vide
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 0;
                    LastButtonMade.GetComponent<ExploreSpot>().Explored = true;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/buttontexture1");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    break;

                case 1: //Joueur
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 1;
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = true;
                    break;

                case 2: //Ennemi normal
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 2;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Skool");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    break;

                case 3: //Trésor
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 3;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Chest");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    break;

                case 4: //Ennemi élite
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 4;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/RedSkool");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    break;

                case 5: //Boss et sortie
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 5;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/BossSkool");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    //Le boss est toujours tout à droite de l'écran
                    LastButtonMade.transform.localPosition = new Vector3(700.0f, 50.0f, 0);
                    break;

                case 6: //Entrée
                    LastButtonMade.GetComponent<ExploreSpot>().Type = 6;
                    LastButtonMade.GetComponent<ExploreSpot>().Explored = true;
                    LastButtonMade.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/buttontexture1");
                    LastButtonMade.GetComponent<ExploreSpot>().Player_Is_Here = false;
                    break;
            }
            if(i == 0) LastButtonMade.transform.localPosition = new Vector3(-700.0f, 50.0f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
