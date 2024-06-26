using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

namespace MarsDonalds
{ 
    public struct MoneyEvent
    {
        static MoneyEvent e;
        public int value;
        public static void Trigger(int v)
        {
            e.value = v;
            EventManager.TriggerEvent(e);
        }
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        private readonly int START_MONEY = 10000;
        public int Money;
        public int Stage;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                Init();
                DontDestroyOnLoad(this.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            Money = START_MONEY;
            Stage = 1;
            UnityGoogleSheet.LoadAllData();
        }
    }

}
