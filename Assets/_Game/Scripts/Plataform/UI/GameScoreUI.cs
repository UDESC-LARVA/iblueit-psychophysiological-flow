using Ibit.Plataform.Manager.Score;
using UnityEngine;
using UnityEngine.UI;

namespace Ibit.Plataform.UI
{
    public class GameScoreUI : MonoBehaviour
    {
        [SerializeField]
        private Text value;
        private Scorer scorer;

        private void Awake() => scorer = FindObjectOfType<Scorer>();

        private void Update() 
        {
            value.text = $"{scorer.Score:####}";
        }
    }
}