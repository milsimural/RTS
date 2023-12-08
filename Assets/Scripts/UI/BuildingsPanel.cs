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
    
    // Start is called before the first frame update
    void Start()
    {
        _buildings = new List<BuildingProfile>(Resources.LoadAll<BuildingProfile>("Buildings"));

        foreach (var building in _buildings) 
        {
            var buttonGo = Instantiate(BuildingButtonTemplate, _buttonParent);
            Debug.Log(buttonGo.GetComponent<Text>());
            buttonGo.GetComponent<Text>().text = "Some";
            //buttonGo.GetComponent<Image>().sprite = building.Icon;
            //buttonGo.GetComponent<BuildingPresaenterOnButton>().Present(building);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
