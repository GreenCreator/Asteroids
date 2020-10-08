using UnityEngine;

public class ProjectTitle : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
