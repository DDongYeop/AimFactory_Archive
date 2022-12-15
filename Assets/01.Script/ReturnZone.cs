using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (-155f > collision.transform.position.x || collision.transform.position.x > 155f)
        {
            print(1);
            collision.transform.position = new Vector3(-55, -2, 0);
        }
        else
        {
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 2.3f);
        }
    }
}
