using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
    public float bubble_speed_min;
    public float bubble_speed_max;
    public float bubble_disable_x;
    public float bubble_disable_y;


    GameObject player;

    float bubble_speed;
    public Vector3 bubble_direction;
    public Color bubble_color;
    public float bubble_scale;

	void Start () {
        player = GameObject.FindWithTag("Player");
        bubble_direction = player.transform.position - this.transform.position;
        bubble_direction = Vector3.Normalize(bubble_direction);
        bubble_speed = Random.Range(bubble_speed_min, bubble_speed_max);
        bubble_color = new Color(Random.Range(0, 10) / 10.0f, Random.Range(0, 10) / 10.0f, Random.Range(0, 10) / 10.0f, 1);
        bubble_scale = Random.Range(6, 15) / 10.0f;
        this.transform.localScale = new Vector3(bubble_scale,bubble_scale,bubble_scale);
        this.GetComponent<SpriteRenderer>().color=bubble_color;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(bubble_direction*bubble_speed);

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if(x>=bubble_disable_x || x<=-bubble_disable_x || y>=bubble_disable_y || y<=-bubble_disable_y){
            GameObjectPool.GetInstance().Object_Disable(this.gameObject);
        }
	}

   

}
