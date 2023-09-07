using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //
    // Start is called before the first frame update
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<T>();
            }
            if(instance == null)
            {
                instance = new GameObject().AddComponent<T>();
            }
            return instance;
        }
    }
}
