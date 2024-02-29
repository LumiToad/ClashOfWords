using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMode : GameModeBase
{
    public BaseView BaseView { get; private set; }

    public override void Setup(object data, GameModeType type, BaseView view)
    {
        gameModeType = type;
        BaseView = view;
        DisableWalnutSpawner();
    }

    private void DisableWalnutSpawner()
    {
        foreach (var spawner in FindObjectsOfType<WalnutSpawner>())
        {
            spawner.gameObject.SetActive(false);
        }
    }
}
