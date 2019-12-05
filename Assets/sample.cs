using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    // Start is called before the first frame updatein
    int y;
    void Start()
    {
        y = cal(3, 4);
        Debug.Log(y);
        Debug.Log("Hello");
        train();

    }
    public int cal(int a, int b)
    {
        return a + b;
    }
    public void train()
    {
        Debug.Log("Training");
        int u = cal(y, 4);
        Debug.Log(u);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
