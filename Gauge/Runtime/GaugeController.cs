using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Gauge.Runtime
{
    public class GaugeController : MonoBehaviour
    {
        #region Publics
        public Slider _playerGauge;
        public Slider _gameGaugeUpper;
        public Slider _gameGaugeLower;
        public Slider _lifeBar;
        public TextMeshProUGUI _scoreText;
        public GameObject _gameOverScreen;
        public enum GameState
        {
            Invalid,
            Pregame,
            Started,
            GameOver,
        }

        public enum GameBehaviour
        {
            None,
            Sinus,
            Wave,
            Squeeze,

        }
        #endregion

        #region Unity Api

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Start()
        {
            ResetGameState();
        }

        // Update is called once per frame
        private void Update()
        {
            switch (_gameState)
            {
                case GameState.Invalid:
                    break;
                case GameState.Pregame:
                    UpdatePlayer();
                    UpdateGauge();
                    CheckGameStart();
                    break;
                case GameState.Started:
                    UpdatePlayer();
                    UpdateLife();
                    UpdateScore();
                    UpdateGauge();
                    UpdateLifebar();
                    CheckGameEnd();
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Main Methods

        public void ResetGameState()
        {
            _gameOverScreen.SetActive(false);
            _gaugeValue = _gaugeStartValue;
            _score = 0;
            _playerGauge.value = _gaugeStartValue;
            _life = _maxLife;
            _gameState = GameState.Pregame;
            UpdateScore();
            UpdateGauge();
            UpdateLifebar();
        }

        #endregion


        #region Utils

        static public bool IsBetween(float value, float min, float max)
        {
            return value >= min && value <= max;
        }
        #endregion


        #region Private and Protected

        private float _gaugeRange = 0.1f;
        private float _gaugeValue = 0.0f;
        private float _gaugeValueDownSpeed = 0.2f;
        private float _gaugeValueUpSpeed = 0.3f;
        private float _gaugeStartValue = 0.5f;
        private float _gaugeTargetValue = 0.8f;
        private GameState _gameState = GameState.Invalid;
        private float _score = 0;
        private float _scoreUpSpeed = 5f;
        private int _maxLife = 1;
        private float _life = 0f;
        private float _lifeDecreaseSpeed = 1f;
        private void CheckGameStart()
        {
            if (IsBetween(_playerGauge.value, _gaugeTargetValue - _gaugeRange, _gaugeTargetValue + _gaugeRange))
            {
                _gameState = GameState.Started;
            }
        }
        private void UpdateGauge()
        {
            _playerGauge.value = _gaugeValue;
            _gameGaugeLower.value = _gaugeTargetValue - _gaugeRange;
            _gameGaugeUpper.value = _gaugeTargetValue + _gaugeRange;
        }

        private void UpdateLife()
        {
            if (!IsBetween(_playerGauge.value, _gaugeTargetValue - _gaugeRange, _gaugeTargetValue + _gaugeRange))
            {
                _life -= _lifeDecreaseSpeed * Time.deltaTime;
            }
        }

        private void UpdateLifebar()
        {
            _lifeBar.value = _life / _maxLife;
        }

        private void UpdatePlayer()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _gaugeValue += _gaugeValueUpSpeed * Time.deltaTime;
            }
            else
            {
                _gaugeValue -= _gaugeValueDownSpeed * Time.deltaTime;
            }

            if (_gaugeValue > 1)
            {
                _gaugeValue = 1;
            }

            if (_gaugeValue < 0)
            {
                _gaugeValue = 0;
            }
        }

        private void CheckGameEnd()
        {
            if (_life < 0f)
            {
                ChangeGameState(GameState.GameOver);
            }
        }

        private void ChangeGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Invalid:
                    break;
                case GameState.Pregame:
                    ResetGameState();
                    break;
                case GameState.Started:
                    break;
                case GameState.GameOver:
                    _gameOverScreen.SetActive(true);
                    break;
                default:
                    break;
            }
            _gameState = newState;
        }
        private void UpdateScore()
        {
            if (IsBetween(_playerGauge.value, _gaugeTargetValue - _gaugeRange, _gaugeTargetValue + _gaugeRange))
            {
                _score += _scoreUpSpeed * Time.deltaTime;
                _scoreText.text = _score.ToString("F0");
            }
        }
        #endregion
    }
}
