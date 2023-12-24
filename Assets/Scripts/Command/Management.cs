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
    public Camera Camera; //������ �� ������
    public SelectableObject Hovered; //����������� ��� �������� ������ �� ������ � ������� ������ �����
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public static Vector2 MousePosition; // ����������� ���������� ��������

    public SelectionState CurrentSelectionState;

    private void Start()
    {
        FrameImage.enabled = false;
    }

    void Update()
    {
        MousePosition = new Vector2(Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, 0f);

        if (hit2D) // ���� ������ � ������ � �����������
        {
            if (hit2D.collider.GetComponent<SelectableColaider>()) // ���� ��� ������ � �������� "SelectableColaider"
            {
                SelectableObject hitSelectable = hit2D.collider.GetComponent<SelectableColaider>().SelectableObject; // �������� �� ����� ������ ������ �� ������ � ����� ������ �� ���� � ��������� ����������
                
                if(Hovered) // ���� ����� �� ������ ��� ����� � ���� ���������� -> ��, ����� �� �����
                {
                    if(Hovered != hitSelectable) // ���� ��� �� ����� ������ �� ������� �� ��������?  -> ���, ��� ������
                    {
                        Hovered.OnUnHover(); // ������� ��������� � ���� ������� ������� ������ � ����������
                        Hovered = hitSelectable; // �������������� � ���������� ����� ������ �� ������� ��������
                        Hovered.OnHover(); // �������� ��������� ������� � ����������
                    }
                }
                else
                {
                    Hovered = hitSelectable;
                    Hovered.OnHover();
                }
            }
            else // ���� ����� �� �� ������� � �����������
            {
                UnhoverCurrent(); // �������� ����� ����������� ���� �� ��� �� � ���������� � ��������� ��������� � ��������� �� ���������� ������
            }
        }
       




        if(Input.GetMouseButtonUp(0)) // ����� �� ��������� ������ ����
        {
            if(Hovered) // ���� �� �������� �� ������ ���������� �� �������� (�� ����� � ����������)
            {
                if(Input.GetKey(KeyCode.LeftControl) == false) // ���� �� ����� ������� �� ������� ����� ������������� ��� ������� �� �����
                {
                    UnselectAll(); // �������� ����� ������� ������� ��������� � ������� ������
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Hovered); // �������� ������ � ����� ���������
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
        

            if (Input.GetMouseButtonDown(1)) // ��� ��� ������� ���������
        {
            UnselectAll();
        }

        // ��������� ������
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
                Unit[] allUnits = FindObjectsOfType<Unit>();
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

   void Select(SelectableObject selectableObject) // ����� ���������
    {
        if (ListOfSelected.Contains(selectableObject) == false) // ��������� ���� �� ������ � ������ ��� -> ��� ��� � ������
        {
            ListOfSelected.Add(selectableObject); // ��������� ��� � ����
            selectableObject.Select(); // � �������� ����
        }
    }

    void UnselectAll() // ����� ������� ������� ��������� � ������� ������
    {
        for (int i = 0; i < ListOfSelected.Count; i++) // ������� ���������� ������ �������� � ����� 
        {
            ListOfSelected[i].UnSelect(); // ������� ��� ���������
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }

    void UnhoverCurrent() // ����� ����������� ���� �� ��� �� � ���������� � ��������� ��������� � ��������� �� ���������� ������
    {
        if (Hovered) // ���� � ���������� �������� ����� �� ������ -> ��, ����� �� �����
        {
            Hovered.OnUnHover(); // �� ������� ��� ���������
            Hovered = null; // � ��� ������� �� ����������
        }
    }
}
