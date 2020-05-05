using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

internal class MainThread : MonoBehaviour
{
    internal static MainThread thread;
    Queue<Action> cambios = new Queue<Action>();

    void Awake() //Este hace las cosas antes del start
    {
        thread = this; 
    }

    void Update()
    {
        if (cambios.Count > 0)
        {
            if (!cambios.Equals(null))
                cambios.Dequeue().Invoke();
        }
    }

    internal void AddJob(Action newcambios)
    {
        cambios.Enqueue(newcambios);
    }

    internal void clearJobs()
    {
        cambios.Clear();
    }
}
