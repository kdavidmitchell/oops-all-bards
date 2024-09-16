using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimatorEventCalls : MonoBehaviour
{
    // Collection of various functions that animators may want for animator events

    // Used for destroyed object after animation is done.
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
