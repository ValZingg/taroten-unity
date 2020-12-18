using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimBreathing : MonoBehaviour
{
    /*
        Ce script récupère l'image du parent, et change la taille
        en boucle pour créer l'illusion d'une animation de respiration.
     */

    public bool is_animating = false;
    public int Delay = 1; //animation delay
    public float Scale = 30.0f;
    public float PosScale = 0.0f;
    private Transform parenttransform;
    public GameObject parentobject;
    public Image parentimage;
    // Start is called before the first frame update
    void Start()
    {
        parenttransform = gameObject.transform;
        parentobject = parenttransform.gameObject;
        parentimage = parentobject.GetComponent<Image>();
        PosScale = Scale / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(parentimage.sprite != null && is_animating == false) //Only animate if the image has a sprite
        {
            is_animating = true;
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        while(true)
        {
            is_animating = true;
            parentimage.rectTransform.sizeDelta = new Vector2(parentimage.rectTransform.sizeDelta.x, parentimage.rectTransform.sizeDelta.y - Scale);
            parentimage.rectTransform.position = new Vector3(parentimage.rectTransform.position.x, parentimage.rectTransform.position.y - PosScale, parentimage.rectTransform.position.z);
            yield return new WaitForSeconds(Delay);
            parentimage.rectTransform.sizeDelta = new Vector2(parentimage.rectTransform.sizeDelta.x, parentimage.rectTransform.sizeDelta.y + Scale);
            parentimage.rectTransform.position = new Vector3(parentimage.rectTransform.position.x, parentimage.rectTransform.position.y + PosScale, parentimage.rectTransform.position.z);
            yield return new WaitForSeconds(Delay);
        }    
    }
}
