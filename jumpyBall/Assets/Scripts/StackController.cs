using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField]
    private StackPartController[] stackPartControlls = null;

    public void ShatterAllParts ()
    {

        if(transform.parent!=null)
        {
            transform.parent = null;
            FindAnyObjectByType<Player>().IncreaseBrokenStacks();
            
        }

        foreach (StackPartController item in stackPartControlls)
        {
            item.Shatter();
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts ()
    {
        yield return new WaitForSeconds(1);

        foreach (StackPartController item in stackPartControlls)
        {
            item.RemoveChildren();
        }
        Destroy(gameObject);
    }
}
