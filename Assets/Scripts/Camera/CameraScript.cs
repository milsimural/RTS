using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float CameraMoveSpeed = 5f;
    private float CameraZoomSpeed = 500f;
    private float CameraMinHeight = 1f;
    private float CameraMaxHeight = 9f;
    Transform CameraTransform;
    public static bool CameraZoom = true, CameraMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraTransform && CameraZoom && CameraMove )
        {
            float y = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            float h = -Input.GetAxis("Mouse ScrollWheel");
            Camera.main.orthographicSize += h * CameraZoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, CameraMinHeight, CameraMaxHeight);
            CameraTransform.position = new Vector3(CameraTransform.position.x + x * CameraMoveSpeed * Time.deltaTime,
                CameraTransform.position.y + y * CameraMoveSpeed * Time.deltaTime,
                CameraTransform.position.z);
        }
        
    }
}
