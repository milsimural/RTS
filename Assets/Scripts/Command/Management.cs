using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour
{
    public Camera Camera; //ссылка на камеру
    public SelectableObject Hovered; //переменныая для хранения ссылки на обьект в который попали лучем
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public static Vector2 MousePosition; // Статическая переменная прозапас

    public SelectionState CurrentSelectionState;

    private void Start()
    {
        FrameImage.enabled = false;
    }

    void Update()
    {
        MousePosition = new Vector2(Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, 0f);

        if (hit2D) // Если попали в объект с коллайдером
        {
            if (hit2D.collider.GetComponent<SelectableColaider>() && !hit2D.collider.GetComponent<EnemyAI>()) // ЕСЛИ ЭТО ОБЬЕКТ С скриптом "SelectableColaider"
            {
                SelectableObject hitSelectable = hit2D.collider.GetComponent<SelectableColaider>().SelectableObject; // Получаем из этого срипта ссылку на обьект и пишем ссылку на него в временную переменную
                
                if(Hovered) // Если какой то объект уже лежал в этой переменной -> ДА, КАКОЙ ТО ЛЕЖИТ
                {
                    if(Hovered != hitSelectable) // Этот тот же самый объект на который мы навелись?  -> НЕТ, ЭТО ДРУГОЙ
                    {
                        Hovered.OnUnHover(); // Убераем подсветку с того объекта который сейчас в переменной
                        Hovered = hitSelectable; // Перезаписываем в переменную новый объект на который навелись
                        Hovered.OnHover(); // Включаем подсветку объекта в переменной
                    }
                }
                else
                {
                    Hovered = hitSelectable;
                    Hovered.OnHover();
                }
            }
            else // Если поинт не на объекте с коллайдером
            {
                UnhoverCurrent(); // Вызываем метод проверяющий есть ли что то в переменной и снимающий подсветку и убирающий из переменной объект
            }
        }
       




        if(Input.GetMouseButtonUp(0)) // Когда мы отпускаем кнопку мыши
        {
            if(Hovered) // Если мы наведены на объект подходящий по условиям (он лежит в переменной)
            {
                if(Input.GetKey(KeyCode.LeftControl) == false) // Если не нажат контрол то объекты будут развыделяться при нажатии на новый
                {
                    UnselectAll(); // Вызываем метод который снимает выделения и очищает список
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Hovered); // Передаем объект в метод выделения
            }
        }

        if(CurrentSelectionState == SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit2D.collider.tag == "Ground")
                {
                    for (int i = 0; i < ListOfSelected.Count; i++)
                    {
                        ListOfSelected[i].WhenClickOnGround(hit2D.point);
                    }
                }
            }
        }
        

            if (Input.GetMouseButtonDown(1)) // При ПКМ снимаем выделение
        {
            UnselectAll();
        }

        // ВЫДЕЛЕНИЕ РАМКОЙ
        if(Input.GetMouseButtonDown(0))
        {
            _frameStart = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            
            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;
            FrameImage.rectTransform.anchoredPosition = min;

            if(size.magnitude > 10)
            {
                
                
                FrameImage.enabled = true;
                FrameImage.rectTransform.sizeDelta = size;

                Rect rect = new Rect(min, size);

                UnselectAll();
                Unit[] allUnits = FindObjectsOfType<UserUnit>();
                for (int i = 0; i < allUnits.Length; i++)
                {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Select(allUnits[i]);
                    }
                }

                CurrentSelectionState = SelectionState.Frame;
            }
            
            
        }

        if(Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;
            if(ListOfSelected.Count > 0)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentSelectionState = SelectionState.Other;
            }
        }
        

    }

    // ===================================================================================================================

   void Select(SelectableObject selectableObject) // Метод выделения
    {
        if (ListOfSelected.Contains(selectableObject) == false) // проверяем есть ли объект в списке уже -> ЕГО НЕТ В СПИСКЕ
        {
            ListOfSelected.Add(selectableObject); // добавляем это в лист
            selectableObject.Select(); // И включаем круг
        }
    }

    void UnselectAll() // Метод который снимает выделения и очищает список
    {
        for (int i = 0; i < ListOfSelected.Count; i++) // Сначала перебираем список обьектов в листе 
        {
            ListOfSelected[i].UnSelect(); // Снимаем все выделения
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }

    void UnhoverCurrent() // Метод проверяющий есть ли что то в переменной и снимающий подсветку и убирающий из переменной объект
    {
        if (Hovered) // Если в переменной хранится какой то объект -> ДА, КАКОЙ ТО ЛЕЖИТ
        {
            Hovered.OnUnHover(); // Мы убераем ему подсветку
            Hovered = null; // И все удаляем из переменной
        }
    }
}
