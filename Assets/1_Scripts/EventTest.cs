using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarsDonalds;

public struct TestEvent
{
    public int a;
    static TestEvent e;

    public static void Trigger(int a)
    {
        e.a = a;
        EventManager.TriggerEvent(e);
    }
}

public class EventTest : MonoBehaviour, IEventListener<TestEvent>
{
    public bool IsListening => throw new System.NotImplementedException();

    public void EventStart()
    {
        this.EventStartListening<TestEvent>();
    }

    public void EventStop()
    {
        this.EventStartListening<TestEvent>();
    }

    public void OnEvent(TestEvent e)
    {
        Debug.Log(e.a);
    }


    private void OnEnable() => EventStart();
    private void OnDisable() => EventStop();

}
