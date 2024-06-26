using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarsDonalds
{
    public class UI_GameOver : MonoBehaviour
    {
        public void OnClick_Return()
        {
            SceneManager.LoadScene("Title");
        }
    }
}
