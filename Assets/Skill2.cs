using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Run);
    }

    void Run()
    {
        animator.SetBool("Charged6", true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Run();
        }
    }
}
