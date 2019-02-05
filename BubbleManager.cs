using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleManager : MonoBehaviour {

    public GameObject bubble;

    public float bubble_create_speed;
    public float bubble_create_speed_max;

    public float create_position_x;

    public float create_position_y;

    float x;
    float y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Random.Range(0,100)<=bubble_create_speed){
            if(Random.Range(0,2)<0.5f){
                y = Random.Range(0, 1);
                y = (y < 0.5f) ? create_position_y: -create_position_y;
                x = Random.Range(-create_position_x, create_position_x);
            }else{
                x = Random.Range(0, 2);
                x = (x < 0.5f) ? create_position_x : -create_position_x;
                y = Random.Range(-create_position_y, create_position_y);
            }


            GameObjectPool.GetInstance().Object_Instantiate(bubble, x, y, 0);


        }
        if(bubble_create_speed<bubble_create_speed_max)
            bubble_create_speed += 0.0002f;
        //print(bubble_create_speed);
	}


    public void LoadNewScene(){
        SceneManager.LoadScene("main");
    }
}
