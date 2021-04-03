using EnemyLogic;
using Player;
using UI;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public CanvasFade LoseWindow;
    public CanvasFade WinWindow;
    
    private Health _home;
    private Score _score;
    
    public void Construct(Health home, Score score)
    {
        _home = home;
        _score = score;

        _home.Died += OnHomeDied;
        _score.Changed += OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        if (value >= _score.TargetValue)
            WinWindow.Show();
    }

    private void OnHomeDied()
    {
        LoseWindow.Show();
    }
}