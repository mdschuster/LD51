using System;
using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Class that runs the state machine for any number of state classes
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        private State[] _states; //do you really need this?

        public State currentState;

        
        private void Start()
        {
            _states = GetComponents<State>();
        }

        private void Update()
        {
            currentState = currentState.Process();
        }


    }
}