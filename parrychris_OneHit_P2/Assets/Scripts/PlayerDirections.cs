using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirections : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    private PlayerOneBehaviour P1Script;
    private PlayerTwoBehaviour P2Script;

    // Use this for initialization
    void Start()
    {
        P1Script = PlayerOne.GetComponent<PlayerOneBehaviour>();
        P2Script = PlayerTwo.GetComponent<PlayerTwoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlip();
    }


    private void CheckFlip()
    {
        //check if players have passed each other
        Vector3 position = P1Script.transform.position;
        if (P1Script.getOnRightSide() == true)
        {
            if (position.x < P2Script.transform.position.x)
            {
                Flip();
            }
        }
        else
        {
            if (position.x >= P2Script.transform.position.x)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        //flip both charcters
        P1Script.transform.Rotate(new Vector3(0, 180, 0));
        P2Script.transform.Rotate(new Vector3(0, 180, 0));
        P2Script.setOnRightSide();
        P1Script.setOnRightSide();
    }
}
