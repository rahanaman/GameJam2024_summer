using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarsDonalds
{
    public class UI_Title : MonoBehaviour
    {
        [SerializeField] private GameObject _ui_Help;


        public void OnClick_Start()
        {
            SceneManager.LoadScene("Stage");
        }

        public void OnClick_Help()
        {
            _ui_Help.SetActive(true);
        }

        public void OnClick_HelpExit()
        {
            _ui_Help.SetActive(false);
        }

        public void OnClick_Exit() => Application.Quit();
    }
}
