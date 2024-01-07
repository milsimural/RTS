using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectableObject : MonoBehaviour
{
    // Можно выделить
    // Можно снять выделение
    // Реагируем на наведение курсора
    // Реагируем на покидание курсора

    public GameObject SelectionIndicator; // Индикатор того что обьект выделен
    

    // Вид объекта
    public enum _SelectableType
    {
        Unit, UnitComa, UnitWreck, Enemy, EnemyComa, EnemyWreck, Building, BuildingComa, Resouce 
    }

    public _SelectableType SelectableType;


    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public virtual void OnUnHover()
    {
        transform.localScale = Vector3.one;
    }

    public virtual void Select()
    {
        SelectionIndicator.SetActive(true);
    }

    public virtual void UnSelect()
    {
        SelectionIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector2 point)
    {

    }
}
