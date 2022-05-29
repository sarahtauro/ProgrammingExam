using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmbientLight : MonoBehaviour
{
    private bool ambLightEnabled;
    private UnityEngine.InputSystem.LightSensor ambLight;

    private float luxCounter = -1000f;

    // Start is called before the first frame update
    void Start()
    {
        ambLightEnabled = EnableAmbLight();
    }

    private bool EnableAmbLight()
    {
        if(UnityEngine.InputSystem.LightSensor.current != null)
        {
            ambLight = UnityEngine.InputSystem.LightSensor.current;
            InputSystem.EnableDevice(ambLight);
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
