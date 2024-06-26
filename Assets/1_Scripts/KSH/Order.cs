using MarsDonalds.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MarsDonalds
{
    public struct OrderStartEvent
    {
        static OrderStartEvent e;
        public Order.OrderData orderData;

        public static void Trigger(Order.OrderData a)
        {
            e.orderData = a;
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderTimeEvent
    {
        static OrderTimeEvent e;
        public Order.OrderData orderData;

        public static void Trigger(Order.OrderData a)
        {
            e.orderData = a;
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderSubmitEvent
    {
        static OrderSubmitEvent e;
        public List<CookData> cookData;
        public int extraSubMenu;
        public List<int> extraSource;
        public List<int> extraDrink;
        
        public static void Trigger(
            IEnumerable<CookData> cookData,
            int extraSubMenu = 0,
            IEnumerable<int> extraSource = null, 
            IEnumerable<int> extraDrink = null)
        {
            e.cookData = cookData.ToList();
            e.extraSubMenu = extraSubMenu;
            e.extraSource = extraSource?.ToList();
            e.extraDrink = extraDrink?.ToList();
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderCancelEvent
    {
        static OrderCancelEvent e;

        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderCompleteEvent
    {
        static OrderCompleteEvent e;
        int reward;
        public static void Trigger(int reward)
        {
            e.reward = reward;
            EventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// 들어오는 주문을 control
    /// 현재 만들어야 하는 음식이 뭔지 등등
    /// </summary>
    public class Order : MonoBehaviour, 
        IEventListener<StageStartEvent>, IEventListener<StageTimeEvent>, IEventListener<StageEndEvent>,
        IEventListener<OrderSubmitEvent>, IEventListener<AnimationEndEvent>
    {
        public class MenuData
        {
            public int potatoID;
            public Recipe recipe;

            public MenuData(int potatoID, Recipe recipe)
            {
                this.potatoID = potatoID;
                this.recipe = recipe;
            }
        }
        public class OrderData
        {
            private static readonly int[] orderMenuCount = { 1, 1, 1, 2, 2, 2, 2, 2, 3, 3 };
            public List<MenuData> menuData;
            public int extraSubMenu;
            public List<int> extraSource;
            public List<int> extraDrink;

            int passedTime;
            int timeLimit;

            public OrderData()
            {
                int menuCount = orderMenuCount[Random.Range(0, orderMenuCount.Length)];
                menuData = new List<MenuData>(menuCount);
                for(int i = 0; i < menuCount; ++i) {
                    menuData.Add(
                        new MenuData(Random.Range(1, 5), 
                        Order.Instance.GetRandomRecipe()));
                }
                extraSubMenu = Random.Range(0, 100) < 50 ? 0 : 1;
                extraSource = new List<int>(3);
                for (int i = 1; i <= 3; ++i) {
                    if (Random.Range(0, 100) < 30) {
                        extraSource.Add(i);
                    }
                }
                extraDrink = new List<int>(3);
                for (int i = 1; i <= 3; ++i) {
                    if (Random.Range(0, 100) < 80) {
                        extraDrink.Add(i);
                    }
                }
                passedTime = 0;
                timeLimit = 60;
            }

            public bool IsSubmittable() => passedTime < timeLimit;
            public void TimePassed() => passedTime++;

            public int PassedTime => passedTime;
            public int RemainTime => timeLimit - passedTime;

            public float TimeRatio => (float)RemainTime / timeLimit;
        }
        public static Order Instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        private OrderData _current;
        private bool _isSubmit = false;

        private List<Recipe> _recipe;
        private int _totalWeight;

        public Recipe GetRandomRecipe()
        {
            int rand = Random.Range(0, _totalWeight);
            for(int i = 0; i < _recipe.Count; ++i) {
                if (rand < _recipe[i].weight) {
                    return _recipe[i];
                }
                rand -= _recipe[i].weight;
            }
            return null;
        }
        public void EventStart()
        {
            this.EventStartListening<StageStartEvent>();
            this.EventStartListening<StageTimeEvent>();
            this.EventStartListening<StageEndEvent>();
            this.EventStartListening<OrderSubmitEvent>();
            this.EventStartListening<AnimationEndEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<StageStartEvent>();
            this.EventStopListening<StageTimeEvent>();
            this.EventStopListening<StageEndEvent>();
            this.EventStopListening<OrderSubmitEvent>();
            this.EventStopListening<AnimationEndEvent>();
        }
        public void OnEvent(StageStartEvent e)
        {
            if (e.stageIndex > 0) {
                _recipe = Recipe.RecipeList.
                    Where(x => x.openDate <= e.stageIndex).
                    OrderBy(y => y.weight).
                    ToList();
                _totalWeight = 0;
                foreach (Recipe recipe in _recipe) {
                    _totalWeight += recipe.weight;
                }
                StartCoroutine(Routine());
            }
        }
        public void OnEvent(StageTimeEvent e)
        {
            //throw new System.NotImplementedException();
        }
        public void OnEvent(StageEndEvent e)
        {
            StopAllCoroutines();
        }
        public void OnEvent(OrderSubmitEvent e)
        {
            // 제출함.
            _isSubmit = true;
            int menuCount = 0;
            int allMenuCount = _current.menuData.Count;
            for (int i = _current.menuData.Count - 1; i >= 0; --i) {
                bool isSame = false;
                for(int j = e.cookData.Count - 1; j >= 0; --j) {
                    CookData cookData = e.cookData[j];
                    if (cookData.PotatoID != _current.menuData[i].potatoID) continue;
                    if (cookData.CutState != _current.menuData[i].recipe.cutState) continue;
                    if (cookData.PillState != _current.menuData[i].recipe.pillCheck) continue;
                    if (cookData.CookState.Count != _current.menuData[i].recipe.CookList.Count) continue;
                    int sameCount = 0;
                    for(int k = 0; k < cookData.CookState.Count; ++k) {
                        if (cookData.CookState[k] == _current.menuData[i].recipe.CookList[k]) {
                            sameCount++;
                        }
                    }
                    if (sameCount == cookData.CookState.Count) {
                        e.cookData.RemoveAt(j);
                        isSame = true;
                        break;
                    }
                }
                if (isSame) {
                    _current.menuData.RemoveAt(i);
                    menuCount++;
                }
            }

            int subMenuCount = 0;
            if(_current.extraSubMenu == e.extraSubMenu) {
                subMenuCount++;
            }

            int sourceCount = 0;
            for(int i = 0; i < _current.extraSource.Count; ++i) {
                if (e.extraSource.Contains(_current.extraSource[i])) {
                    sourceCount++;
                }
            }

            int drinkCount = 0;
            for (int i = 0; i < _current.extraDrink.Count; ++i) {
                if (e.extraDrink.Contains(_current.extraDrink[i])) {
                    drinkCount++;
                }
            }

            bool is메뉴같음 = allMenuCount == menuCount;
            bool is서브같음 = subMenuCount > 0;
            bool is소스같음 = sourceCount == _current.extraSource.Count;
            bool is음료같음 = drinkCount == _current.extraDrink.Count;

            if(is메뉴같음 && is서브같음 && is소스같음 && is음료같음) {
                _current = null;
            }
        }
        private IEnumerator Routine()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            while (Stage.Instance.IsPlay) {
                _current = new OrderData();
                _isSubmit = false;
                OrderStartEvent.Trigger(_current);
                while (_isSubmit == false &&
                    _current.IsSubmittable()) {
                    _current.TimePassed();
                    OrderTimeEvent.Trigger(_current);
                    yield return waitForSecond;
                }
                _isEnd = false;
                if (_current == null) {
                    Stage.Instance.판매(1000);
                    OrderCompleteEvent.Trigger(1000);
                }
                else {
                    _current = null;
                    Stage.Instance.폐기(500);
                    OrderCancelEvent.Trigger();
                }
                yield return new WaitUntil(() => _isEnd == true);
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();

        private bool _isEnd = false;
        public void OnEvent(AnimationEndEvent e)
        {
            if(e.key == 1) {
                _isEnd = true;
            }
        }
    }
}
