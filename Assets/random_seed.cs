using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_seed : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Random.InitState(47838);
        DontDestroyOnLoad(this);
    }
}
