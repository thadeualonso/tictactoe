using UnityEngine;
using UniRx;
using UniRx.Operators;
using UnityEngine.UI;
using UniRx.Triggers;

namespace TicTacToe.Reactive
{
    public class GameManagerRX : MonoBehaviour
    {
        [SerializeField] Button button00;

        private void Start()
        {
            var clickStream = button00.OnClickAsObservable().Select(_ => new Move(0, 0));
            clickStream.Subscribe(x => OnClick(x));
        }

        void OnClick(Move move)
        {
            Debug.Log("Move: " + move.X + " " + move.Y);
        }
    }
}