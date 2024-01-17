using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPar : MonoBehaviour
{
    public int ParStr = 12; // ���� �����, ������� ���, ������ � ������ � �����
    public int ParCon = 12; // HP, ������������� ���������� ��������, �������� ������������� HP, �������� ������
    public int ParDex = 12; // �������� ������� ����, �����
    public int ParInt = 12; // ���� ����������, ���-�� �������� ����������
    public int ParWis = 12; // ���� ���������� ���������, ���-�� �������� ���������� ���������, ������������� ������������� �����
    public int ParCha = 12; // ���������� NPC, ��������, ������������� ���������� �����������

    //==DEFENCE==//
    private float ResultHealthPoints;
    private int ArmorClassMin; // �������� ����������� 0
    private int ArmorClassMax; // �������� ������������ 90
    private int ArmorClassPircing; // �������� -10 ��� -20 - �� ��� �������� �������������� ������������ �������� AC
    private int ArmorClassSlashing;
    private int ArmorClassCrushing;
    private int ArmorClassMagic; // ������� A� ������ ���������� ����.
    private int ArmorClassFire;
    private int ArmorClassCold;
    private int ArmorClassNature;
    private int ArmorClassEarth;
    private int ArmorClassMental;

    //==OFFENCE==//
    private string DamageType;
    private int DamageMultiplier; // ���� ���� ����������� ����� ������� ��������� �����
    private int WeaponMinDamage; // ����������� 0
    private int WeaponMaxDamage; // ������������ 120
    private int WeaponArmorPiercing; // �� 0 �� 40 
    private int WeaponLastChanceDamage; // �� 0 �� 15

    private int CalculateDamageMultiplier(int parStr)
    {
        if (parStr == 3)
            return -11;
        else if (parStr == 4)
            return -9;
        else if (parStr == 5)
            return -7;
        else if (parStr == 6)
            return -5;
        else if (parStr == 7)
            return -3;
        else if (parStr == 8)
            return -2;
        else if (parStr == 9)
            return -1;
        else if (parStr == 10)
            return 0;
        else if (parStr == 11)
            return 0;
        else if (parStr == 12)
            return 0;
        else if (parStr == 13)
            return 1;
        else if (parStr == 14)
            return 2;
        else if (parStr == 15)
            return 3;
        else if (parStr == 16)
            return 4;
        else if (parStr == 17)
            return 6;
        else if (parStr == 18)
            return 8;
        else if (parStr == 19)
            return 10;
        else if (parStr == 20)
            return 14;
        else if (parStr == 21)
            return 18;
        else if (parStr == 22)
            return 22;
        else if (parStr == 23)
            return 26;
        else if (parStr == 24)
            return 30;
        else if (parStr == 25)
            return 36;
        else
            return 0;
    }

    private int CalculateDamage(int weaponMinDamage, int weaponMaxDamage, int weaponArmorPiercing, int damageMultiplier, int armorClassMin, int armorClassMax, int weaponLastChanceDamage)
    {

        int resultDamage = Random.Range(weaponMinDamage, weaponMaxDamage); // ������ ����������� ��� ����� ������������ �� ����
        resultDamage = resultDamage + damageMultiplier; // ���������� ������������ �� ����
        int resultArmorClass = Random.Range(armorClassMin, armorClassMax) - weaponArmorPiercing; // ����� ����� ��� ������� ��������� (� ����� ����� ����� �� ������) � ������ �������� ������
        resultDamage = resultDamage - resultArmorClass; // ������ ����������� ����� ������� ����� � �������� ������
        if (resultDamage > 0)
            resultDamage = 0;

        // � ������ ������ ������ ���� ���� ������� ����, ������ 50% ���� �� ��������� ����������� � ������� �� 0 �� 15
        // ������� ���� ����

        if (resultDamage == 0)
            resultDamage = Random.Range(0, weaponLastChanceDamage);
        return resultDamage;
    }

}
