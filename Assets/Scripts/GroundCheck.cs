using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter,
        isGroundStay,
        isGroundExit;

    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == groundTag)
        {
            // 地面に入る
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == groundTag)
        {
            isGroundExit = true;
        }
    }
}
