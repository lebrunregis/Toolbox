using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private EventSystem _mainEventSystem;
    [SerializeField] private GameObject _background;


    private async void Start()
    {
        BindObjects();
        //_loadingScreen.Show();
        await InitializeObjects();
        await CreateObjects();
    }

    private async UniTask CreateObjects()
    {
       // throw new NotImplementedException();
    }

    private async UniTask InitializeObjects()
    {
        //   throw new NotImplementedException();
       // await
          //     _gameInputSystem.Enabled();
    }

    private void BindObjects()
    {
       // throw new NotImplementedException();
    }
}
