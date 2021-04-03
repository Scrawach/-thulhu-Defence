using Cthulhu;
using EnemyLogic;
using Player;
using UI;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public CanvasFade LoseWindow;
    public CanvasFade WinWindow;
    
    private Health _home;
    private Rise _rise;
    
    public void Construct(Health home, Rise rise)
    {
        _home = home;
        _rise = rise;

        _home.Died += OnHomeDied;
        _rise.Win += OnWinHappened;
    }

    public void ForceWin()
    {
        _rise.StartAnim();
    }

    private void OnWinHappened()
    {
        WinWindow.Show();
    }

    private void OnHomeDied()
    {
        LoseWindow.Show();
    }
}