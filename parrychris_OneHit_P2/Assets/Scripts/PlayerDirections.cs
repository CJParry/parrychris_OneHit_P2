using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirections : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    private MergedPlayerBehaviour P1Script;
    private MergedPlayerBehaviour P2Script;

    // Use this for initialization
    void Start()
    {
        P1Script = PlayerOne.GetComponent<MergedPlayerBehaviour>();
        P2Script = PlayerTwo.GetComponent<MergedPlayerBehaviour>();
        P2Script.setOnRightSide(false);
        P1Script.setOnRightSide(true);
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
                Flip(false);
            }
        }
        else
        {
            if (position.x >= P2Script.transform.position.x)
            {
                Flip(true);
            }
        }
    }

    void Flip(bool flip)
    {
        //flip both charcters
        P1Script.transform.Rotate(new Vector3(0, 180, 0));
        P2Script.transform.Rotate(new Vector3(0, 180, 0));
        P2Script.setOnRightSide(!flip);
        P1Script.setOnRightSide(flip);
        Debug.Log("Flipping: " + flip);
    }
}
