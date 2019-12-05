using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellman_equation : MonoBehaviour
{
    // Start is called before the first frame update

    private float[] transition_matrix;
    [SerializeField]
    private float gamma = 0.999f;
    [SerializeField]
    private int total_states = 12;
    [SerializeField]
    private float epsilon = 0.01f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
