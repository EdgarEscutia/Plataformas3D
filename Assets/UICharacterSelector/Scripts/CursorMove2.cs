using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement2 : MonoBehaviour
{

    public float speed;

    void Update()
    {

        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            y = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            y = -1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            x = -1f;
        if (Input.GetKey(KeyCode.RightArrow))
            x = 1f;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;

        Vector3 worldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -worldSize.x, worldSize.x),
            Mathf.Clamp(transform.position.y, -worldSize.y, worldSize.y),
            transform.position.z);


    }
}
