using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public Camera Camera; //������ �� ������
    public SelectableObject Hovered; //����������� ��� �������� ������ �� ������ � ������� ������ �����
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();


    public static Vector2 MousePosition; // ����������� ���������� ��������

    void Update()
    {
        MousePosition = new Vector2(Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, 0f);

        if (hit2D) // ���� ������ � ������ � �����������
        {
            if (hit2D.collider.GetComponent<SelectableColaider>()) // �������� ���� �� � ������� ������ "SelectableColaider"
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
        }
        else // ���� ����� �� �� ������� � �����������
        {
            UnhoverCurrent(); // �������� ����� ����������� ���� �� ��� �� � ���������� � ��������� ��������� � ��������� �� ���������� ������
        }

        if(Input.GetMouseButtonUp(0)) // ����� �� ��������� ������ ����
        {
            if(Hovered) // ���� �� �������� �� ������ ���������� �� �������� (�� ����� � ����������)
            {
                if(Input.GetKey(KeyCode.LeftControl) == false) // ���� �� ����� ������� �� ������� ����� ������������� ��� ������� �� �����
                {
                    UnselectAll(); // �������� ����� ������� ������� ��������� � ������� ������
                } 
                
                Select(Hovered); // �������� ������ � ����� ���������
            }
        }

        if(Input.GetMouseButtonDown(1)) // ��� ��� ������� ���������
        {
            UnselectAll();
        }

    }

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
