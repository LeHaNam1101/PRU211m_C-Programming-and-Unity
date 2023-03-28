using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    public Transform playerTransform;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(playerTransform.position + offset);


    }

}
