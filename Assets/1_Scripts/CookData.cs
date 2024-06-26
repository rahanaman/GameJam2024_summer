using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"감자맛 : {PotatoID} 껍질상태 : {PillState} 자른상태 : {CutState}");
            sb.Append("요리상태 : ");
            for(int i = 0; i < CookState.Count; ++i) {
                sb.Append($" {CookState[i]} ,");
            }
            return sb.ToString();
        }

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
            if(id > 3)
            {
                if(PillState == 0)
                {
                    PillState=1;
                    return;
                }
                else
                {
                    isFood=false;
                    return;
                }
            }
            if(CutState > 0)
            {
                isFood = false;
                return;
            }
            CutState = id;
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
