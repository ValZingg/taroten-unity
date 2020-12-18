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

        1 = Entrée joueur
        2 = Ennemi
        3 = Trésor
        4 = Ennemi élite
     
     */

    //=========UI=========
    public Image LoadingBar;
    //====================

    //=========OTHERS=====
    public int identifier;
    private string line_to_write;
    private bool has_placed_player = false;

    public const int room_amount = 15;
    //====================

    // Start is called before the first frame update
    void Start()
    {
        identifier = Random.Range(0, 100000); //Génération d'un identifiant unique pour la run
        /* Ecriture dans le fichier run */
        string filename = identifier + ".tRUN";
            //L'identifiant
        line_to_write = "identifier=" + identifier;
        System.IO.File.WriteAllText(filename, line_to_write);

             //Séparateur pour le fichier
        line_to_write = "\n------------------======= LEVEL 1 =======-------------------";
        System.IO.File.AppendAllText(filename, line_to_write);

        //Les salles
        int[] Rooms = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //faut le remplir de 0 sinon il veux pas
        has_placed_player = false;
        for (int i = 0;i < room_amount;i++)
        {
            if(!has_placed_player && i != 0) Rooms[i] = Random.Range(1, 5); //définit le type de salle
            else Rooms[i] = Random.Range(2, 5); // ne génère plus de 1 si le joueur est déjà placé

            if (Rooms[i] == 1) has_placed_player = true; //Si c'est un 1, c'est le départ du joueur, et il ne peux y en avoir qu'un
            line_to_write = "\nroom" + i + "=" + Rooms[i];
            System.IO.File.AppendAllText(filename, line_to_write);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
