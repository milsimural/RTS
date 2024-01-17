using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPar : MonoBehaviour
{
    public int ParStr = 12; // Сила удара, Носимый вес, Допуск к оружию и броне
    public int ParCon = 12; // HP, Сопротивление физическим эффектам, Скорость востановления HP, Скорость отдыха
    public int ParDex = 12; // Точность дальних атак, Крафт
    public int ParInt = 12; // Сила заклинаний, Кол-во заученых заклинания
    public int ParWis = 12; // Сила заклинаний священика, Кол-во заученых заклинаний священика, Сопротивление элементальной магии
    public int ParCha = 12; // Управление NPC, Торговля, Сопротивление ментальным заклинаниям

    //==DEFENCE==//
    private float ResultHealthPoints;
    private int ArmorClassMin; // Значение минимальное 0
    private int ArmorClassMax; // Значение максимальное 90
    private int ArmorClassPircing; // Например -10 или -20 - на это значение корректируется максимальное значение AC
    private int ArmorClassSlashing;
    private int ArmorClassCrushing;
    private int ArmorClassMagic; // Базовый AС против магических атак.
    private int ArmorClassFire;
    private int ArmorClassCold;
    private int ArmorClassNature;
    private int ArmorClassEarth;
    private int ArmorClassMental;

    //==OFFENCE==//
    private string DamageType;
    private int DamageMultiplier; // Этот урон добавляется после расчета основного урона
    private int WeaponMinDamage; // Минимальное 0
    private int WeaponMaxDamage; // Максимальное 120
    private int WeaponArmorPiercing; // От 0 до 40 
    private int WeaponLastChanceDamage; // От 0 до 15

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

        int resultDamage = Random.Range(weaponMinDamage, weaponMaxDamage); // Размер повреждений без учета модификатора от силы
        resultDamage = resultDamage + damageMultiplier; // Добавление модификатора от силы
        int resultArmorClass = Random.Range(armorClassMin, armorClassMax) - weaponArmorPiercing; // Армор Класс при текущем попадании (в какое место брони мы попали) с учетом пробития оружия
        resultDamage = resultDamage - resultArmorClass; // Размер повреждений после расчета брони и пробития оружия
        if (resultDamage > 0)
            resultDamage = 0;

        // У любого оружия должен быть шанс нанести урон, дается 50% шанс на нанесение повреждений в размере от 0 до 15
        // Считаем этот урон

        if (resultDamage == 0)
            resultDamage = Random.Range(0, weaponLastChanceDamage);
        return resultDamage;
    }

}
