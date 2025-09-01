using System.Collections;
using UnityEngine;

public class WarmUpManager : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private Transform[] prefabs;
    
    IEnumerator Start()
    {
        foreach (Material mat in materials)
        {
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            temp.transform.parent = transform;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponent<Renderer>().material = mat;

            yield return null;

            temp.SetActive(false);
        }

        foreach (Transform prefab in prefabs)
        {
            Transform temp = Instantiate(prefab, transform);
            
            yield return null;

            temp.gameObject.SetActive(false);
        }
    }
}
