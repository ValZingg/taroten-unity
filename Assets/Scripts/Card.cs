using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string raw_Name;
    public string Name;
    public string Desc;

    public GameObject TopHUD;

    public Image DescBG;
    public Text DescUI;

    Vector3 cachedScale;

    public bool ZoomOnHover = true;

    // Start is called before the first frame update
    void Start()
    {
        TopHUD = GameObject.Find("TopHUD");
        cachedScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ZoomOnHover) transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        DescBG.enabled = true;
        DescUI.enabled = true;
        DescUI.text = Name + "\n\n" + Desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(ZoomOnHover) transform.localScale = cachedScale;
        DescBG.enabled = false;
        DescUI.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.transform.GetChild(0).GetComponent<Image>().enabled == true)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            TopHUD.GetComponent<CombatManager>().ChosenCards_amount--;
            TopHUD.GetComponent<CombatManager>().ChosenCards.Remove(raw_Name);
        }
            
        else
        {
            if(TopHUD.GetComponent<CombatManager>().ChosenCards_amount != 3) //max 3 cartes par tour
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                TopHUD.GetComponent<CombatManager>().ChosenCards_amount++;
                TopHUD.GetComponent<CombatManager>().ChosenCards.Add(raw_Name);
            }
        }
            
    }

    public void TurnOffLights()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void LoadDesc()
    {
        string filepath = "Assets/Text/card_data/" + raw_Name + ".txt";
        string[] lines = System.IO.File.ReadAllLines(filepath);

        Name = lines[0];
        Desc = lines[1];
        Desc = Desc.Replace('|', '\n');
    }
}
