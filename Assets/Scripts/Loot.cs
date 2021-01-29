using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public int RandomLoot; //0 == card , 1 == Item

    public GameObject Card_obj;
    public GameObject Item_obj;

    public string raw_name;

    // Start is called before the first frame update
    void Start()
    {
        Card_obj = GameObject.Find("CardSlot_1");
        Item_obj = GameObject.Find("ItemSlot");

        //CARTE
        string filepath = "Assets/Text/card_data/ALL_CARDS.txt";
        string[] lines = System.IO.File.ReadAllLines(filepath);
        int randomcard = Random.Range(0, lines.Length);
        raw_name = lines[randomcard].Remove(lines[randomcard].Length - 4); //enlève le .txt
        filepath = null;
        filepath = "Assets/Text/card_data/" + lines[randomcard];
        lines = System.IO.File.ReadAllLines(filepath);
        

        Card_obj.GetComponent<Card>().raw_Name = raw_name;
        Debug.Log(raw_name);
        Card_obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cards/" + raw_name);
        Card_obj.GetComponent<Card>().LoadDesc();

        //ITEM
        filepath = "Assets/Text/Items/ALL_ITEMS.txt";
        lines = System.IO.File.ReadAllLines(filepath);
        int randomitem = Random.Range(0, lines.Length);
        raw_name = lines[randomitem].Remove(lines[randomitem].Length - 4); //enlève le .txt
        filepath = null;
        filepath = "Assets/Text/Items/" + lines[randomitem];
        lines = System.IO.File.ReadAllLines(filepath);

        Item_obj.GetComponent<Item>().raw_name = raw_name;
        Debug.Log(raw_name);
        Item_obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + raw_name);
        Item_obj.GetComponent<Item>().LoadItem(raw_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard()
    {
        GameObject.Find("DataTracker").GetComponent<Player>().Cards.Add(raw_name);

        GameObject.Find("BG_Texture").GetComponent<SaveProgress>().saveprogressnow();
        GameObject.Find("BG_Texture").GetComponent<SwitchScene>().SwitchSceneNow("Explore");
    }

    public void AddItem()
    {
        GameObject.Find("DataTracker").GetComponent<Player>().Items.Add(raw_name);

        GameObject.Find("BG_Texture").GetComponent<SaveProgress>().saveprogressnow();
        GameObject.Find("BG_Texture").GetComponent<SwitchScene>().SwitchSceneNow("Explore");
    }
}
