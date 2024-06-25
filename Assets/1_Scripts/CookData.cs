using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{

    public class CookData
    {
        public int PotatoID { get; private set; } // 0,1,2,3 //°¨ÀÚ, ÂüÄ¡¸À, °í±¸¸¶¸À, °è¶õ¸À
        public int PeelState { get; private set; } //0,1
        public int CutState { get; private set; } // 0,1, 2,3 -  x, Ã¤½ä±â,±ïµÏ½ä±â, Ÿ‡Áö½ä±â
    }
}
