using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

   public void ProcessLook(Vector2 input) 
   {
        float mouseX = input.x;
        float mouseY = input.y;
        // calcola la rotazione della camera
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // applicato alla Transform della Camera 
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0,0);
        // gira il player per farlo guardare a destra e a sinistra
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

   }

}
