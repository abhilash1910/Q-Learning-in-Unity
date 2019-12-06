//Source code template for Q-learning to implement a simple maze of 4*4 dimension.To change the dimension of the maze 
//update the size ,goal and provide the correct indices(adjacency matrix) of transition matrix,q matrix and reward matrix
//in agent_walk function put any index<size as a starting point for the Agent to reach the maze end based on the available path
//the matrices are adjacency matrices.i.e if an Agent is at state 1 wants to tmove to state 2 then the adj_mat[1][2] gets update accordingly.
//Uses the standardised Q-Learning formulation based on MDP /Bellman formulations.s


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qlearmimg : MonoBehaviour
{//variable declarations
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
    //final destination of the maze
    int goal = 3;
    [SerializeField]
    int max_epoch = 1000;
    //transition matrix
    float[][] transition_mat;
    //reward matrix
    float[][] reward;
    //q value matrix
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
    {//create the matrices with some reward probabilities
        transition_mat = Create(size);
        reward = create_reward(size);
        quality_mat = create_quality(size);




        //train the agent
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
    //select best states at a particular instance based on probabilities
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
    //get  random state from chosen best states
    public int GetProbNextState(int s, float[][] transition_mat)
    {
        List<int> getpossible_states = GetPossibleStates(s, transition_mat);
        int randomize = rnd.Next(0, getpossible_states.Count);
        return getpossible_states[randomize];

    }
    //enumerator functions to move the agent after learning
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


    //train function
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
                //choose among the best probable state which gives the max reward

                float maxq = float.MinValue;

                for (int j = 0; j < possiblenextsteps.Count; j++)
                {
                    Debug.Log(possiblenextsteps[j]);

                    int n_s = possiblenextsteps[j];
                    float qs = quality_mat[next_state][n_s];
                    //update q value matrix based on maximum value
                    if (qs > maxq)
                    {
                        maxq = qs;
                    }
                }
                // update q matrix with Q-learning algorithmic formula
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
    //function to find the max in a container/array
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
    //provide the actual path for agent to walk
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
    //move the agent along the learned path to the goal
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
    //create the adjacency matrix for transition.Choose the points where the Agent can  move byu providing any finite value greater than 0
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

    //reward matrix creation. To update rewards change the adjacency matrix cells accordingly.For example if an Agent in state 1 want to go to state 2 ,update the mat[1][2] accordingly
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
    //q value matrix
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

