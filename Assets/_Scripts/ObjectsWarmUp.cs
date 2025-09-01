using System.Collections;
using UnityEngine;

public class ObjectsWarmUp : MonoBehaviour
{
    [SerializeField] private Transform[] objects;
    IEnumerator Start()
    {
        foreach (Transform obj in objects)
        {
            Transform tmp = Instantiate(obj, new Vector3(999, 999, 999), Quaternion.identity);
            yield return null;
            Destroy(tmp.gameObject);
        }
    }

}
