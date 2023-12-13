using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPresaenterOnButton : MonoBehaviour
{
    public Text BuildingName;
    public Image Icon;
    public Button Button;

    public void Present(BuildingProfile profile)
    {
        BuildingName.text = profile.Name;
        Icon.sprite = profile.Icon;
    }
}
