using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_Instance = null;

    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<T>();

                if (s_Instance == null)
                {
                    Debug.LogError($"Cant' find object type of {typeof(T)}! Created new.");
                    s_Instance = new GameObject($"{typeof(T).ToString()} (Clone)").AddComponent<T>();
                }
            }

            return s_Instance;
        }
    }
}