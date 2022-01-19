using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform child;

    void LateUpdate()
    {
        child.transform.rotation = Quaternion.Euler(90, gameObject.transform.rotation.y * -1.0f, 0.0f);
    }
}