using NetworkHelper;
using Test_IPL.Common;
using Test_IPL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Test_IPL.ViewModel
{
	public enum GameState { PreGame, Lobby, GameUnderway, Results }
	
	/// <summary>
    /// This is the ViewModel for the HostPage, it ties all the UI logic with the game logic.
    /// </summary>
    public class GameViewModel : BindableBase
	{
		/// <summary>
        /// A static helper function to run code on the UI thread. This is used in callbacks from the NetworkHelper library.
        /// </summary>
        private static Func<DispatchedHandler, Task> callOnUiThread = async (handler) => await
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, handler);
				
		/// <summary>
        /// The player names stored for the UI.
        /// </summary>
        public ObservableCollection<String> PlayerNames { get; set; }
		
		/// <summary>
        /// Maps the player names to their guids.
        /// </summary>
        private Dictionary<string, Guid> _playerToParticipantMap = new Dictionary<string, Guid>();
		
		/// <summary>
        /// The manager that sends UDP advertisement messages and manages a list of participants.
        /// </summary>
        private UdpManager _manager = new UdpManager();

        /// <summary>
        /// The list of communication channels to send messages to participants.
        /// </summary>
        private List<ICommunicationChannel> _participantCommunicationChannels = new List<ICommunicationChannel>();
		
		/// <summary>
        /// The communication channel to listen for messages from participants.
        /// </summary>
        private ICommunicationChannel _managerCommunicationChannel = new TcpCommunicationChannel();
		
		public GameViewModel()
        {
            // When a player connects to the host.
            _manager.ParticipantConnected += OnParticipantConnected;

            // When a message is received from a player.
            _managerCommunicationChannel.MessageReceived += OnMessageReceived;

            // Attach an event handler to the PropertyChanged event so that 
            // PlayerProgress and NextButtonText are updated correctly.
            /* PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals("SubmittedAnswers"))
                {
                    OnPropertyChanged(nameof(PlayerProgress));
                }
                if (e.PropertyName.Equals("CurrentQuestion") &&
                    Questions.Last() == CurrentQuestion)
                {
                    NextButtonText = "Show results";
                }
            }; */

            NextButtonText = "Next player";

            // Set up game information.
            PlayerNames = new ObservableCollection<string>();
            //SubmittedAnswers = new Dictionary<string, Dictionary<Question, int?>>();
        }
		
		/// <summary>
        /// Command for creating a game. After this command is executed, 
        /// the host will enter the lobby screen and start broadcasting itself
        /// to listening players.
        /// </summary>
        public DelegateCommand CreateGameCommand
        {
            get
            {
                return _createGameCommand ?? (_createGameCommand = new DelegateCommand(
                    async () =>
                    {
                        GameState = GameState.Lobby;
                        await StartAdvertisingGameAsync(GameName);
                    }));
            }
        }
		private DelegateCommand _createGameCommand;
		
		/// <summary>

        /// Command for starting a game. After this command is executed,

        /// the host will send the first question to all the players.

        /// </summary>

        public DelegateCommand StartGameCommand

        {
            get
            {
                return _startGameCommand ?? (_startGameCommand = new DelegateCommand(
                    async () =>
                    {
                        // Start game.
					/*	currentQuestionIndex = 0;
                        await SendCurrentQuestion();

                        // On Question changed.
                        OnPropertyChanged(nameof(CurrentQuestion));
                        OnPropertyChanged(nameof(PlayerProgress));
					*/
                        GameState = GameState.GameUnderway;
                        StopAdvertisingGame();
                    },
                    () => GameState == GameState.Lobby));
            }
        }
        private DelegateCommand _startGameCommand;

        /// <summary>
        /// The text that indicates the current state of the app displayed in the UI.
        /// </summary>
        //this indicated the state of app in a particular  instant
        public string StateName
        {
            get
            {
                return _stateName;
            }
            set
            {
                SetProperty(ref _stateName, value);
            }
        }
        private string _stateName;

        /// <summary>
        /// The current state of the game.
        /// </summary>
        public GameState GameState
        {
            get
            {
                return _gameState;
            }

            set
            {
                if (SetProperty(ref _gameState, value))
                {
                    // Alert the UI that all the visiblity properties may have changed, because the game state changed.
                    OnPropertyChanged(nameof(PreGameVisibility));
                    OnPropertyChanged(nameof(StartVisibility));
                    OnPropertyChanged(nameof(GameUnderwayVisibility));
                    //OnPropertyChanged(nameof(ResultsVisibility));
                    //OnPropertyChanged(nameof(PlayerResults));

                    StartGameCommand.RaiseCanExecuteChanged();
                    //NextQuestionCommand.RaiseCanExecuteChanged();
                    //EndGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private GameState _gameState;
	
		/// <summary>
        /// The name of the game that players will see when selecting the game they want to join.
        /// </summary>
        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                if (SetProperty(ref _gameName, value))
                {
                    OnPropertyChanged(nameof(GameName));
                    StartGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string _gameName;
		
		/// <summary>
        /// The text for the "next" button. This allows text to change based on the current
        /// game state.
        /// </summary>
        public string NextButtonText
        {
            get
            {
                return _nextButtonText;
            }
            set
            {
                SetProperty(ref _nextButtonText, value);
            }
        }
        private string _nextButtonText;
		
		/// <summary>
        /// Used to determine if the PreGame screen is visible or not.
        /// </summary>
        public Visibility PreGameVisibility =>
            GameState == GameState.PreGame ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Used to determine if the Lobby screen is visible or not.
        /// </summary>
        public Visibility StartVisibility =>
            GameState == GameState.Lobby ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Used to determine if the Questions screen is visible or not.
        /// </summary>
        public Visibility GameUnderwayVisibility =>
            GameState == GameState.GameUnderway ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
        /// Stops advertising and listening for answer
        /// </summary>
        public async Task StopListeningAsync()
        {
            _manager.StopAdvertising();
            await _managerCommunicationChannel.StopListening();
        }	
		
		/// <summary>
        /// Updates the UI when a player has left.
        /// </summary>
        /// <param name="playerName"></param>
        private void OnPlayerLeft(string playerName)
        {
            _manager.RemoveParticipant(_playerToParticipantMap[playerName]);
            _playerToParticipantMap.Remove(playerName);

            if (PlayerNames.Remove(playerName))
            {
                //SubmittedAnswers.Remove(playerName);
                OnPropertyChanged(nameof(PlayerNames));
                //OnPropertyChanged(nameof(SubmittedAnswers));
            }
        }
		
		/// <summary>
        /// Updates the UI when a player has joined.
        /// </summary>
        /// 
        private void OnPlayerJoined(PlayerMessage message, Guid guid)
        {
            Guid duplicatePlayerID;

            if (!_playerToParticipantMap.TryGetValue(message.PlayerName, out duplicatePlayerID))
            {
                _playerToParticipantMap.Add(message.PlayerName, guid);

                if (PlayerNames.Contains(message.PlayerName))
                {
                    message.PlayerName += ".";
                }

                PlayerNames.Add(message.PlayerName);

                //SubmittedAnswers.Add(message.PlayerName,
                    //new Dictionary<Question, int?>(Questions.Count));

                OnPropertyChanged(nameof(PlayerNames));
                //OnPropertyChanged(nameof(SubmittedAnswers));
            }
        }
		
		/// <summary>
        /// When a message is received from a player.
        /// </summary>
        private async void OnMessageReceived(object sender, IMessageReceivedEventArgs e)
        {
            // Deserialize the message
            object data = new PlayerMessage();
            e.GetDeserializedMessage(ref data);
            var message = data as PlayerMessage;

            switch (message.Command)
            {
             /* case PlayerMessageType.Answer:
                    await callOnUiThread(() => OnAnswerReceived(message.PlayerName, message.QuestionAnswer));
                    break;
            */
				case PlayerMessageType.Leave:
                    await callOnUiThread(() => OnPlayerLeft(message.PlayerName));
                    break;
            }
        }
		
		/// <summary>
        /// When a player has been connected to the host.
        /// </summary>
        private async void OnParticipantConnected(object sender, ParticipantConnectedEventArgs e)
        {
            // A player has joined the game.
            _participantCommunicationChannels.Add(_manager.CreateCommunicationChannel(e.Id));

            PlayerMessage data = new PlayerMessage
            {
                PlayerName = e.Message.ToString(),
                //QuestionAnswer = 0,
                Command = PlayerMessageType.Join
            };

            await callOnUiThread(() => OnPlayerJoined(data, e.Id));
        }
		
		/// <summary>
        /// Begins broadcasting the game to listening players.
        /// </summary>
        private async Task StartAdvertisingGameAsync(string name)
        {
            _manager.AdvertiserMessage = name;
            await _manager.StartAdvertisingAsync();
            await _managerCommunicationChannel.StartListeningAsync();
        }
		
		/// <summary>
        /// Stops broadcasting the game to listening players.
        /// </summary>
        private void StopAdvertisingGame() => _manager.StopAdvertising();
	}
}