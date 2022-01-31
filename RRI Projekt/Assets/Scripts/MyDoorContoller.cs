using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorContoller : MonoBehaviour
{
    private Animator doorAnim;

    private bool doorOpen = false;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if(PlayerController.KeyCount == 3)
        {
            if (!doorOpen)
            {
                Debug.Log("open");
                doorAnim.Play("door open", 0, 0.0f);
                doorOpen = true;
            }
            else
            {
                Debug.Log("close");
                doorAnim.Play("door close", 0, 0.0f);
                doorOpen = false;
            }
        }
    }
}
