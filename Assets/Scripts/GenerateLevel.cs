using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GenerateLevel : MonoBehaviour
{
    /*
        TYPES DE SALLES
        ===============

        0 = Vide
        1 = position joueur
        2 = Ennemi
        3 = Trésor
        4 = Ennemi élite
        5 = Boss et sortie
     
     */

    //=========UI=========
    public Image LoadingBar;
    //====================

    //=========OTHERS=====
    public int identifier;
    private string line_to_write;

    public const int room_amount = 14;

    public GameObject charscriptlocation;// Le "BG_Text" qui contient les scripts, qui n'a pas été supprimé la scène d'avant
    public GameObject idkeeper; //Un objet vide où on va stocker l'identifiant unique

    public SwitchScene switchscenescript;
    //====================

    // Start is called before the first frame update
    void Start()
    {
        identifier = Random.Range(0, 100000); //Génération d'un identifiant unique pour la run
        idkeeper.GetComponent<KeepID>().ID = identifier; //Sauvegarde l'identifiant dans un objet
        /* Ecriture dans le fichier run */
        string filename = identifier + ".tRUN";
            //L'identifiant
        line_to_write = "identifier=" + identifier;
        System.IO.File.WriteAllText(filename, line_to_write);

        line_to_write = "\ncurrentlyonlevel=1"; //Le niveau actuel du joueur, commence toujours à 1
        System.IO.File.AppendAllText(filename, line_to_write);

        charscriptlocation = GameObject.Find("BG_Texture");
        line_to_write = "\ncharacter=" + charscriptlocation.GetComponent<LoadChar>().charname; //utilise le gameobject qui n'a pas été supprimé pour chopper le nom du perso
        System.IO.File.AppendAllText(filename, line_to_write);

        line_to_write = "\ncards="; //Les cartes
        System.IO.File.AppendAllText(filename, line_to_write);
        foreach(var card in charscriptlocation.GetComponent<LoadChar>().cards_str)
        {
            line_to_write = card + ",";
            System.IO.File.AppendAllText(filename, line_to_write);
        }

        Destroy(charscriptlocation); //le supprime après, parce que plus besoin de lui

        UpdateLoadingBar(100);

        //Génération des salles des 3 niveaux           
        for (int k = 1;k < 4;k++)
        {
            //Séparateur pour le fichier
            line_to_write = "\n------------------======= LEVEL " + k + " =======-------------------";
            System.IO.File.AppendAllText(filename, line_to_write);
            //Les salles
            int[] Rooms = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //faut le remplir de 0 sinon il veux pas
            for (int i = 0; i < room_amount; i++)
            {
                if (i == 0) Rooms[i] = 1; //Joueur est toujours a la première case
                else if (i == 13) Rooms[i] = 5; //boss toujours à la fin
                else Rooms[i] = Random.Range(2, 5); //Genère un type de salle aléatoire
                line_to_write = "\nroom" + i + "=" + Rooms[i] + ",done=false";
                System.IO.File.AppendAllText(filename, line_to_write);
            }
            UpdateLoadingBar(100);
        }
        UpdateLoadingBar(700);

        switchscenescript.SwitchSceneNow("Explore"); //Switche au jeu après le chargement

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateLoadingBar(int amount) //augemente la longueur de la barre de chargement
    {
        int temp_pushback = amount / 2; //la distance ou on la déplace vers la droite, pour donner l'illusion qu'elle reste immobile
        LoadingBar.rectTransform.sizeDelta = new Vector2(LoadingBar.rectTransform.sizeDelta.x + amount, LoadingBar.rectTransform.sizeDelta.y);
        LoadingBar.transform.position = new Vector3(LoadingBar.transform.position.x + temp_pushback, LoadingBar.transform.position.y, LoadingBar.transform.position.z);
    }
}
