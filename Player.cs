using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public GameObject player_mouse;
    public GameObject ScoreBoard;
    public GameObject GameOverBoard;
    public GameObject LifeBoard;
    public GameObject RetryBtn;
    GameObject mouse;
    bool clicked = false;
    bool move = false;
    Vector3 direction_A;
    Vector3 direction_B;
    Vector3 direction = new Vector3(0f, 0f, 0f);
    Vector3 screenPosition;
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    float v = 1f;
    float max_V = 1.5f;
    float x_Max = 44.0f;
    float y_Max = 18.0f;
    int direction_lock = 0;//1:x-Max  2: x_min  3:y_max   4:y_min
    Vector3 normal;

    float score = 0;
    int life = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            PlayerMove();
        if (clicked && !move)
            Mouse();
    }

    void PlayerMove()
    {
        this.transform.Translate(direction * v);

        if (this.transform.position.x >= x_Max && direction_lock != 1)
        {
            normal = new Vector3(-1, 0, 0);
            direction_lock = 1;
            Reflect();
        }
        else if (this.transform.position.x <= -x_Max && direction_lock != 2)
        {
            normal = new Vector3(1, 0, 0);
            direction_lock = 2;
            Reflect();
        }
        else if (this.transform.position.y >= y_Max && direction_lock != 3)
        {
            normal = new Vector3(0, -1, 0);
            direction_lock = 3;
            Reflect();
        }
        else if (this.transform.position.y <= -y_Max && direction_lock != 4)
        {
            normal = new Vector3(0, 1, 0);
            direction_lock = 4;
            Reflect();
        }
        score = score + 0.1f * v;
        string score_str = string.Format("{0:f1}", score);
        ScoreBoard.GetComponent<Text>().text = "Score: " + score_str;
        v = v - 0.01f;
        if (v <= 0)
        {
            move = false;
            direction_lock = 0;
        }

    }

    void Reflect()
    {

        direction = direction + 2 * (Vector3.Dot(-direction, normal) * normal);
        direction = Vector3.Normalize(direction);
        //print(direction);

    }

    private void OnMouseDown()
    {
        direction_A = this.transform.position;
        mouse = Instantiate(player_mouse, this.transform.position, Quaternion.identity);
        //mouse.transform.position = this.transform.position;
        //print("down");
        clicked = true;

    }


    private void OnMouseUp()
    {
        direction_B = new Vector3(mouse.transform.position.x, mouse.transform.position.y, 0);
        direction = direction_A - direction_B;
        v = 0.1f * direction.magnitude;

        if (v >= max_V)
        {
            v = max_V;
        }
        //print(v);
        direction = Vector3.Normalize(direction);
        // print("over");
        clicked = false;

        move = true;
        Destroy(mouse);
    }


    void Mouse()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePositionOnScreen = Input.mousePosition;
        //print(mousePositionOnScreen);
        mousePositionOnScreen.z = screenPosition.z;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        //print(mousePositionInWorld);
        mouse.transform.position = new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (life > 0)
        {
            life--;
            LifeBoard.GetComponent<Text>().text = "HP: " + life.ToString();
            GameObject.FindWithTag("MainCamera").GetComponent<ShakeCamera>().shake();
            print(life);
        }
        if (life <= 0)
        {
            this.gameObject.transform.localScale = Vector3.zero;
            GameOverBoard.SetActive(true);
            RetryBtn.SetActive(true);
        }
    }
}
