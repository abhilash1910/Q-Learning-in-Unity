using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qlearmimg : MonoBehaviour
{
    [SerializeField]
    private GameObject Agent;
    [SerializeField]
    private GameObject green_01, green_10, green_12, green_22, green_23, green_33;
    System.Random rnd = new System.Random(1);
    [SerializeField]
    static int size = 4;
    [SerializeField]
    static float learning_rate = 0.5f;
    [SerializeField]
    static float gamma = 0.5f;
    int goal = 3;
    [SerializeField]
    int max_epoch = 1000;

    float[][] transition_mat;
    float[][] reward;
    float[][] quality_mat;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 0.000001f;
    List<KeyValuePair<int, int>> cur_next = new List<KeyValuePair<int, int>>();
    void Awake()
    {

        Agent = GameObject.FindGameObjectWithTag("Player");
        green_01 = GameObject.FindGameObjectWithTag("01");
        green_10 = GameObject.FindGameObjectWithTag("10");
        green_12 = GameObject.FindGameObjectWithTag("12");
        green_22 = GameObject.FindGameObjectWithTag("22");
        green_23 = GameObject.FindGameObjectWithTag("23");
        green_33 = GameObject.FindGameObjectWithTag("33");

    }

    void Start()
    {
        transition_mat = Create(size);
        reward = create_reward(size);
        quality_mat = create_quality(size);





        StartCoroutine(Train(transition_mat, reward, quality_mat, gamma, learning_rate, goal, max_epoch));

    }
    IEnumerator Train(float[][] transition_mat, float[][] reward, float[][] quality_mat, float gamma, float learning_rate, int goal, int max_epoch)
    {
        // yield return new WaitForSeconds(0.5f);
        train(transition_mat, reward, quality_mat, gamma, learning_rate, goal, max_epoch);
        //yield return new WaitForSeconds(200.5f);

        agent_walk(1, goal, quality_mat);

        yield return new WaitForSeconds(0.5f);


    }


    void Update()
    {

    }
    public List<int> GetPossibleStates(int s, float[][] transition_mat)
    {
        List<int> states = new List<int>();
        for (int i = 0; i < transition_mat.Length; i++)
        {
            if (transition_mat[s][i] == 1f)
            {
                states.Add(i);
            }
        }

        return states;

    }
    public int GetProbNextState(int s, float[][] transition_mat)
    {
        List<int> getpossible_states = GetPossibleStates(s, transition_mat);
        int randomize = rnd.Next(0, getpossible_states.Count);
        return getpossible_states[randomize];

    }

    IEnumerator move_01()
    {
        Debug.Log("Moved to first");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_01.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_01.transform.position, step);

        }

        // Agent.transform.position = green_01.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }
    IEnumerator move_10()
    {
        Debug.Log("Moved to second");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_10.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_10.transform.position, step);

        }
        //Agent.transform.position = green_10.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }
    IEnumerator move_12()
    {
        Debug.Log("Moved to third");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_12.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_12.transform.position, step);

        }
        // Agent.transform.position = green_12.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }
    IEnumerator move_22()
    {
        Debug.Log("Moved to fourth");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_22.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_22.transform.position, step);

        }
        //Agent.transform.position = green_22.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }
    IEnumerator move_23()
    {
        Debug.Log("Moved to fifth");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_23.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_23.transform.position, step);

        }
        //Agent.transform.position = green_23.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }

    IEnumerator move_33()
    {
        Debug.Log("Moved to sixth");
        yield return new WaitForSeconds(10.5f);
        while (Agent.transform.position != green_33.transform.position)
        {
            float step = speed * Time.deltaTime;
            Agent.transform.position = Vector3.MoveTowards(Agent.transform.position, green_33.transform.position, step);

        }
        //Agent.transform.position = green_33.transform.position;
        //yield return new WaitForSeconds(20.5f);
    }



    public void train(float[][] transition_mat, float[][] reward, float[][] quality_mat, float gamma, float learning_rate, int goal, int max_epoch)
    {
        for (int k = 0; k < max_epoch; k++)
        {
            int current_state = rnd.Next(0, reward.Length);
            while (true)
            {
                Debug.Log("training started");

                int next_state = GetProbNextState(current_state, transition_mat);
                List<int> possiblenextsteps = GetPossibleStates(next_state, transition_mat);
                Debug.Log("Current" + current_state);
                Debug.Log("Next" + next_state);


                float maxq = float.MinValue;

                for (int j = 0; j < possiblenextsteps.Count; j++)
                {
                    Debug.Log(possiblenextsteps[j]);

                    int n_s = possiblenextsteps[j];
                    float qs = quality_mat[next_state][n_s];
                    if (qs > maxq)
                    {
                        maxq = qs;
                    }
                }

                quality_mat[current_state][next_state] = ((1 - learning_rate) * quality_mat[current_state][next_state]) + ((learning_rate) * (reward[current_state][next_state] + gamma * maxq));
                current_state = next_state;
                if (current_state == goal)
                {
                    //Agent.transform.position = green_33.transform.position;
                    //StartCoroutine(move_33());
                    Debug.Log("Reached");

                    break;

                }




            }
        }

    }

    public int argmax_value(float[] v)
    {
        float maxi = v[0];
        int idx = 0;
        for (int i = 0; i < v.Length; i++)
        {
            if (v[i] > maxi)
            {
                maxi = v[i];
                idx = i;
            }
        }
        return idx;
    }
    public void agent_walk(int start, int goal, float[][] quality_mat)
    {
        Debug.Log("walking");

        int cur = start;
        int next;
        int current_state, next_state;
        Debug.Log(cur);
        while (cur != goal)
        {
            next = argmax_value(quality_mat[cur]);
            Debug.Log(next);
            cur_next.Add(new KeyValuePair<int, int>(cur, next));
            cur = next;
        }
        Debug.Log("done");
        cur_next.Add(new KeyValuePair<int, int>(goal, goal));
        StartCoroutine(agent_move(cur_next));







    }
    IEnumerator agent_move(List<KeyValuePair<int, int>> cur_next)
    {
        int current_state, next_state;
        foreach (var i in cur_next)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Out_1" + i);
            current_state = i.Key;
            next_state = i.Value;
            if (current_state == 0 && next_state == 1)
            {
                StartCoroutine(move_01());
                //Agent.transform.position = green_01.transform.position;


            }
            else if (current_state == 1 && next_state == 0)
            {
                StartCoroutine(move_10());
                //Agent.transform.position = green_10.transform.position;
            }

            else if (current_state == 1 && next_state == 2)
            {
                StartCoroutine(move_12());
                //Agent.transform.position = green_12.transform.position;
            }

            else if (current_state == 2 && next_state == 2)
            {
                StartCoroutine(move_22());
                //Agent.transform.position = green_22.transform.position;
            }

            else if (current_state == 2 && next_state == 3)
            {
                StartCoroutine(move_23());
                //Agent.transform.position = green_23.transform.position;
            }

            else if (current_state == 3 && next_state == 3)
            {
                StartCoroutine(move_33());
                //Agent.transform.position = green_23.transform.position;
            }
        }

    }

    public float[][] Create(int size)
    {
        float[][] T_Mat = new float[size][];
        for (int i = 0; i < size; i++)
        {
            T_Mat[i] = new float[size];
        }

        T_Mat[2][3] = T_Mat[2][2] = T_Mat[3][3] = T_Mat[0][1] = T_Mat[1][0] = T_Mat[1][2] = 1f;



        return T_Mat;


    }


    public float[][] create_reward(int size)
    {
        float[][] R = new float[size][];
        for (int i = 0; i < size; i++)
        {
            R[i] = new float[size];

        }
        R[2][2] = R[0][1] = R[1][0] = R[1][2] = -0.1f;
        //reward
        R[2][3] = 5.0f;



        return R;

    }

    public float[][] create_quality(int size)
    {
        float[][] U = new float[size][];
        for (int i = 0; i < size; i++)
        {
            U[i] = new float[size];
        }
        return U;
    }

}

