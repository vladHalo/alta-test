using UnityEngine;

[ExecuteAlways]
public class PlaneUpdate : MonoBehaviour
{
    public Material oldMat;
    public Material newMat;

    void Update()
    {
        if (oldMat != null && newMat != null)
        {
            oldMat.SetVector("_PlanePosition", transform.position);
            newMat.SetVector("_PlanePosition", transform.position);
        }
    }
}
