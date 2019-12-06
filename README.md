# Q-Learning-in-Unity
Template for Q-Learning in Unity with C# for developing state based games.
 
Introduction
 
The repository contains the base template for Q-learning for a maze game where the Agent has to reach the destination based on the rewards proided at each step.The algorithm is based on epsilon greedy to update the Q value of a current state based on the previous state.The Agent has to reach the end of the maze which is represnted as a set of 4X4 dimension matrix (0-3,0-3) and can have a startign point.Based on the reward probabilities at each step and the discount factor (gamma) the Agent updates its path in the training environment.

Technical Aspects

The source code has 3 major matrices mainly- transition matrix,reward matrix and q-value matrix(quality matrix).There are also variable for the number of states(goal) which can be changed ,learning rate,gamma,epsilon and also speed of the Agent.Based on the desired size of the states(possible size of the entire environment),the transition matrix can be updated to specify any non 0 finite values for movable locations(Agent),the reward matrix can also be updated at those locations based on requirements.
The train function contains the logic of the Q-learning.It first selects a possible list of maximum reward based(high probability) transition states.For ex: If Agent is in state 1 and wants to go to state 2,then it selects all the best possible reward based states from 2 to make the transition.A random specific state is chosen from the selecte states.Based on the reward at that particular state the Agent can either take that state or reject that state in the successive iterations.The q-valuematrix is update according to the standard q-value learning algorithm: Q(s,a)= (1-epsilon)Q(s,a) + epsilon(r + ymax(Q(s',a')-Q(s,a)))   . Where y is the gamma /discount rate and r is the reward matrix.
To agent_walk chooses the flexible state transition matrix which yields the best possible outcome based on the training and a container is used to store the trajectory of the learned path.The initial starting state as well as the final end state can be modified accordingly and the transition matrices should be updated accordingly.To set up the level,the planes(path steps) are either green or red,the former sigifying transitionable paths.The color and indices should be changed accordingly if the size of the maze is changed according to the transition/reward/q-value matrices.

Use cases

Provides a template for any research/optimization/game based developments in the Q-learning approach as well as model/hyperparameter tuning.
