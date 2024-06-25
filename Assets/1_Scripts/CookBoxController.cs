using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public class CookBoxController : MonoBehaviour, IEventListener<StageTimeEvent>
    {
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
        }

        public void EventStop()
        {
            throw new System.NotImplementedException();
        }

        public void OnEvent(StageTimeEvent e)
        {
            throw new System.NotImplementedException();
        }
    }
}
