using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedMultiplier = 2.0f; //Hệ số tốc độ
    private bool canClick = true; //Biến kiểm tra xem người chơi có thể click vào button tiếp hay không
    public void SpeedUp()
    {
        if (canClick) //Kiểm tra xem người chơi có thể click vào button tiếp hay không
        {
            //Tăng tốc độ của object
            GetComponent<Rigidbody2D>().velocity *= speedMultiplier;

            //Khóa button trong 5 giây
            canClick = false;
            StartCoroutine(DelayClick());
        }
    }

    IEnumerator DelayClick()
    {
        yield return new WaitForSeconds(5f);
        canClick = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUp();
    }
}
