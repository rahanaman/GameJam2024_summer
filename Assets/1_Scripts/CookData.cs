using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{

    public class CookData
    {
        public int PotatoID { get; private set; } // 0,1,2,3 //����, ��ġ��, ������, �����
        public int PeelState { get; private set; } //0,1
        public int CutState { get; private set; } // 0,1, 2,3 -  x, ä���,��Ͻ��, �������
    }
}
