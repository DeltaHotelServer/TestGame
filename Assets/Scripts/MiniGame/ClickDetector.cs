using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Punktestand um 1 erh�hen
        GameObject.FindObjectOfType<SpawnerScript>().AddPlayerScore();
        Destroy(gameObject);
    }
}