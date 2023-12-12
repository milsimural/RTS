using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Profile")]
public class BuildingProfile : ScriptableObject
{
    public GameObject BuildingView;
    public string Name;
    public Sprite Icon;
    public int PriceInGold;
    public int PriceInTimber;
    public int PriceInStone;
    public int PriceInIron;
    public float TimeToBuild;
}
