using Microsoft.AspNetCore.Mvc;
using prjWordleAPI.Factory;

namespace prjWordleAPI.Controllers
{
    public class WordleController : ControllerBase
    {
        private readonly WordFactory _wordFactory;
        private readonly word _wordInstance;
        private static string _targetWord;
        private static GameState _gameState;

        public WordleController()
        {
            _wordFactory = new WordFactory();
            _wordInstance = word.getInstance();
            if (_gameState == null)
            {
                StartNewGame();
            }
           
            
        }
        private void StartNewGame()
        {
            var generateWordInstance = _wordFactory.GetWord("generate");
            var words = generateWordInstance.genWord();
            _gameState = new GameState
            {
                TargetWord = _wordInstance.Random(words),
                Attempts = 0
            };
        }

        [HttpGet("generateWord")]
        public IActionResult GenerateWord()
        {
            StartNewGame();
            return Ok(new { word = _gameState.TargetWord });
        }

        [HttpPost("checkWord")]
        public IActionResult CheckWord([FromBody] string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length != 5)
            {
                return BadRequest("Word must be 5 letters long.");
            }

            var checkWordInstance = _wordFactory.GetWord("check");
            var validWords = checkWordInstance.genWord();

            if (!validWords.Contains(word.ToLower()))
            {
                return Ok(new CheckWordResponse
                {
                    IsValid = false,
                    Feedback = null,
                    Message = "Invalid word."
                });
            }

            _gameState.Attempts++;

            var feedback = new List<CharacterFeedback>();
            bool isCorrect = true;

            for (int i = 0; i < word.Length; i++)
            {
                char guessedChar = word[i];
                char targetChar = _gameState.TargetWord[i];

                if (guessedChar == targetChar)
                {
                    feedback.Add(new CharacterFeedback
                    {
                        Character = guessedChar,
                        Feedback = "correct"
                    });
                }
                else if (_gameState.TargetWord.Contains(guessedChar))
                {
                    feedback.Add(new CharacterFeedback
                    {
                        Character = guessedChar,
                        Feedback = "present"
                    });
                    isCorrect = false;
                }
                else
                {
                    feedback.Add(new CharacterFeedback
                    {
                        Character = guessedChar,
                        Feedback = "absent"
                    });
                    isCorrect = false;
                }
            }

            if (isCorrect)
            {
                var response = new CheckWordResponse
                {
                    IsValid = true,
                    Feedback = feedback,
                    Message = $"Congratulations! You guessed the word '{_gameState.TargetWord}' in {_gameState.Attempts} attempts."
                };

                // Start a new game
                StartNewGame();

                return Ok(response);
            }

            return Ok(new CheckWordResponse
            {
                IsValid = true,
                Feedback = feedback,
                Message = $"Keep trying! Attempts: {_gameState.Attempts}"
            });
        }
    }
}
    