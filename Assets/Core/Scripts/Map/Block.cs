using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Block : MonoBehaviour
{
    [SerializeField] Color changeColor;
    [SerializeField] float torqueForce, upForce;

    [Button("Break block", EButtonEnableMode.Playmode)]
    public void Break()
    {
        var rbs = GetComponentsInChildren<Rigidbody>();
        var meshRenderer = GetComponentsInChildren<MeshRenderer>();
        
        ChangeColor(meshRenderer);

        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
            meshRenderer[i].enabled = true;

            float randTorque = Random.Range(-torqueForce, torqueForce);

            rbs[i].AddForce(Vector3.up * upForce, ForceMode.Impulse);
            rbs[i].AddTorque(Vector3.right * randTorque + Vector3.forward * randTorque, ForceMode.Impulse);
            rbs[i].velocity = Random.onUnitSphere * randTorque;

            Destroy(gameObject, 10);
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    void ChangeColor(MeshRenderer[] mesh)
    {
        for (int i = 0; i < mesh.Length; i++)
            mesh[i].material.color = changeColor;
    }
}
