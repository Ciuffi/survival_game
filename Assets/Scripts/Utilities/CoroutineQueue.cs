using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue : MonoBehaviour
{
    Queue<IEnumerator> queue = new Queue<IEnumerator>();
    Coroutine queueRoutine;
    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (queue.Count > 0)
                yield return StartCoroutine(queue.Dequeue());
            yield return null;
        }
    }
    public void StartQueue()
    {
        StartCoroutine(CoroutineCoordinator());
    }
    public void StopQueue()
    {
        StopCoroutine(queueRoutine);
    }
    public void AddToQueue(IEnumerator coroutine)
    {
        queue.Enqueue(coroutine);
    }
}
