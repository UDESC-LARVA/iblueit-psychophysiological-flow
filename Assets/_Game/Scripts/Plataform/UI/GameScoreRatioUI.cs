using Ibit.Core.Game;
using Ibit.Plataform.Manager.Score;
using UnityEngine;
using UnityEngine.UI;

namespace Ibit.Plataform.UI
{
    public class GameScoreRatioUI : MonoBehaviour
    {
        [SerializeField]
        private Text value;
        [SerializeField]
        private Image fillImage;

        private Scorer scorer;

        private void Awake() => scorer = FindObjectOfType<Scorer>();

        private void FixedUpdate()
        {
            value.text = $"{(scorer.Score / scorer.MaxScore * 100f):####}%";
            value.color = scorer.Score >= scorer.MaxScore * GameManager.LevelUnlockScoreThreshold ? Color.blue : Color.red;

            fillImage.fillAmount = scorer.Score / scorer.MaxScore;
        }
    }
}