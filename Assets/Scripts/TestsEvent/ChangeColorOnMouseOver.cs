using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorOnMouseOver : MonoBehaviour
{
    private SpriteRenderer _image;
    void Start()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = Color.blue;
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.white;
        Debug.Log("Exit");
    }
}
