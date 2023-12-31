using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsPanel : MonoBehaviour
{
    public GameObject BuildingButtonTemplate;
    [SerializeField]
    private Transform _buttonParent;
    private List<BuildingProfile> _buildings;
    

    void Start()
    {
        _buildings = new List<BuildingProfile>(Resources.LoadAll<BuildingProfile>("Buildings"));

        foreach (var building in _buildings) 
        {
            var buttonGo = Instantiate(BuildingButtonTemplate, _buttonParent);
            buttonGo.GetComponent<BuildingPresaenterOnButton>().Present(building);
        }
    }

}
