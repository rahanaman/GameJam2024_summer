using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{

    public class CookData
    {
        public bool isFood = true;
        public int PotatoID { get; private set; } // 0,1,2,3 //����, ��ġ��, ��������, �����
        public int PillState { get; private set; } //0,1
        public int CutState { get; private set; } // 0,1, 2,3 -  x, ä���,��Ͻ��, �������

        public List<int> CookState { get; private set; } //0,1,2,3,4 



        public CookData(int id)
        {
            PotatoID = id;
            PillState = 0;
            CutState = 0;
            isFood = true;
            CookState = new List<int>() {0};
        }

        public void Pill(int id)
        {
            if(PillState>0)
            {
                isFood = false;
                return;
            }
            PillState = id;
        }
        public void Cook(int id)
        {
            if(CookState.Contains(id)) { isFood = false; return; }
            if(CookState.Count==1 && CookState[0] == 0) {
                CookState[0] = id;
            }
            else CookState.Add(id);
            
        }
        public IngredientID GetIngredientID()
        {
            if (!isFood) { return IngredientID.Waste; }
            return IngredientID.SlicedPotatoes;
        }
    }
}
