using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBasic : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canClick = true;
    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack")) //Kiểm tra xem người chơi có thể click vào button tiếp hay không
        {
            ani.SetTrigger("attack");
        }
    }
}
