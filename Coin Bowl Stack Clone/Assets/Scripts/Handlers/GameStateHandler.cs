using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coati.CoinBowlStack.Enums;

public class GameStateHandler : MonoBehaviour
{
    private void SetGameStateTo(GameState state) => LevelManager.Instance.CurrentGameState = state;

    public void SubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay += () => SetGameStateTo(GameState.TapToPlay);
        EventManager.Instance.StateInGame += () => SetGameStateTo(GameState.InGame);
        EventManager.Instance.StateBeginingOfEndingSequance += () => SetGameStateTo(GameState.BeginningOfEndingSequance);
        EventManager.Instance.StateEndingSequance += () => SetGameStateTo(GameState.EndingSequance);
        EventManager.Instance.StateLevelEnd += () => SetGameStateTo(GameState.LevelEnd);
    }

    private void UnsubscribeEvents()
    {
        EventManager.Instance.StateTapToPlay -= () => SetGameStateTo(GameState.TapToPlay);
        EventManager.Instance.StateInGame -= () => SetGameStateTo(GameState.InGame);
        EventManager.Instance.StateBeginingOfEndingSequance -= () => SetGameStateTo(GameState.BeginningOfEndingSequance);
        EventManager.Instance.StateEndingSequance -= () => SetGameStateTo(GameState.EndingSequance);
        EventManager.Instance.StateLevelEnd -= () => SetGameStateTo(GameState.LevelEnd);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
