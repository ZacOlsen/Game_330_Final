using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonXControlScript : MonoBehaviour {

    struct SimonButtonData
    {
        public string InputName;
        public int MaterialIndex;

        public float EmissionAmount;

        public SimonButtonData(string inInputName, int inMaterialIndex)
        {
            InputName = inInputName;
            MaterialIndex = inMaterialIndex;
            EmissionAmount = 0.0f;
        }
    }

    public float TiltRate = 32.0f;
    float ButtonEmissionIncreaseRate = 6.0f;
    float ButtonEmissionDecreaseRate = 18.0f;
    MeshRenderer SimonBodyMesh;
    SimonButtonData[] SimonButtons = 
        {
            new SimonButtonData("Jump", 1),
            new SimonButtonData("Fire3", 2),
            new SimonButtonData("Fire2", 3),
            new SimonButtonData("Fire1", 4)
        };
    
	void Start () {
        SimonBodyMesh = GetComponentInChildren<MeshRenderer>();

    }

    void Update()
    {
        /*float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 currEulerAngles = transform.localEulerAngles;
        currEulerAngles.z += horizontalAxis * TiltRate * Time.deltaTime;
        currEulerAngles.x += -verticalAxis * TiltRate * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(currEulerAngles);*/

        transform.Rotate(
            -Input.GetAxis("Vertical") * TiltRate * Time.deltaTime,
            0.0f,
            Input.GetAxis("Horizontal") * TiltRate * Time.deltaTime
            );

        for(int i = 0; i < SimonButtons.Length; ++i)
        {
            float targetValue = 0.0f;
            if(IsButtonDepressed(i))
            {
                targetValue = 1.0f;
            }

            float emissionChangeAmount = targetValue - SimonButtons[i].EmissionAmount;
            if(emissionChangeAmount >= 0.0f)
            {
                emissionChangeAmount *= ButtonEmissionIncreaseRate;
            }
            else
            {
                emissionChangeAmount *= ButtonEmissionDecreaseRate;
            }
            SimonButtons[i].EmissionAmount += emissionChangeAmount * Time.deltaTime;

            if(SimonButtons[i].MaterialIndex < SimonBodyMesh.materials.Length)
            {
                Material mat = SimonBodyMesh.materials[SimonButtons[i].MaterialIndex];
                Color baseColor = mat.GetColor("_Color") * 1.5f;
                Color finalColor = baseColor * Mathf.LinearToGammaSpace(SimonButtons[i].EmissionAmount);
                mat.SetColor("_EmissionColor", finalColor);
            }
            
        }
    }

    public bool IsButtonDepressed(int buttonIndex)
    {
        if(buttonIndex < SimonButtons.Length)
        {
            return Input.GetButton(SimonButtons[buttonIndex].InputName);
        }
        return false;
    }
}
