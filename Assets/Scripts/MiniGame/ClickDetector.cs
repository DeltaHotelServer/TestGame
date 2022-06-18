using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}