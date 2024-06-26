using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MarsDonalds
{
    public class Cutter : MonoBehaviour
    {
        [SerializeField] CookZoneController _controller;
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] Button _button;
        [SerializeField] int _type;
        [SerializeField] int _openDay;

        private void Start()
        {
            _button.onClick.AddListener(cut);
            if (GameManager.Instance.Stage < _openDay) gameObject.SetActive(false);
        }

        private void cut()
        {
           
            if (_controller.Data == null || !_controller.Data.isFood || _controller.IsWorking) return;
            CutStartEvent.Trigger(_type);
            Vector3 pos = _rectTransform.localPosition;
            _rectTransform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() => { _rectTransform.DOLocalMove(pos, 0.5f).OnComplete(()=>CutEndEvent.Trigger()); });
        }
    }
}
