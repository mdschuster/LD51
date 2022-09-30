using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Base state class that contains the mechanisms for a state to enter, update, and exit.
    /// This class should be inherited by each concrete state class.
    /// </summary>
    public abstract class State : MonoBehaviour
    {
        
        protected enum Event
        {
            Exit, Update, Enter
        }

        protected Event stage;
        protected State nextState;
        protected State currentState;
        
        protected virtual void StateEnter()
        {
            stage = Event.Update;
        }
        protected virtual void StateUpdate()
        {
            stage = Event.Update;
        }
        protected virtual void StateExit()
        {
            stage = Event.Exit;
        }

        public State Process()
        {
            if (stage == Event.Enter) StateEnter();
            if (stage == Event.Update) StateUpdate();
            if (stage == Event.Exit)
            {
                StateExit();
                return nextState;
            }
            return this;
        }
        
        
    }
}