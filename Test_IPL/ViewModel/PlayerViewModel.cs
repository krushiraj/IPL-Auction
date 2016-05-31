using NetworkHelper;
using Test_IPL.Common;
using Test_IPL.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Test_IPL
{
    public class PlayerViewModel : BindableBase
    {
        /// <summary>
        /// A static helper function to run code on the UI thread.
        /// </summary> // as is
        private static Func<DispatchedHandler, Task> callOnUiThread = async (handler)
            => await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, handler);

        /// <summary>
        /// Represents the participant.
        /// </summary> //as is
        private UdpParticipant _participant = new UdpParticipant();

        /// <summary>
        /// The communication channel to listen for messages from the game host.
        /// </summary> //as is
        private ICommunicationChannel _participantCommunicationChannel;

        /// <summary>
        /// The communication channel at which messages are sent 
        /// </summary> //as is
        private ICommunicationChannel _managerCommunicationChannel;

        public PlayerViewModel()
        {
            //searches for a game and
            // When a game host is found.
            //this snippet creates a TcpCommunicationChannel for communication
            //between host and client
            _participant.ManagerFound += (async (sender, e) =>
            {
                var host = new GameHost() { Name = e.Message, Id = e.Id, CommChannel = _participant.CreateCommunicationChannel(e.Id) };
                await callOnUiThread(() => AvailableGames.Add(host));
            });

            _participantCommunicationChannel = new TcpCommunicationChannel();

            // When a new question is received from the game host.
            //we have to replace question by candidate
            _participantCommunicationChannel.MessageReceived += (async (sender, e) =>
               {
                   object message = new int();
                   e.GetDeserializedMessage(ref message);
                //await callOnUiThread(() => CurrentQuestion = message as Question);
            });
        }

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
        /// The name of the player.
        /// </summary>
        //Name of the player(participant)
        public string PlayerName
        {
            get
            {
                return _playerName ?? string.Empty;
            }
            set
            {
                if (SetProperty(ref _playerName, value))
                {
                    JoinGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string _playerName;

        /// <summary>
        /// Indicates whether the player can join a game. Preconditions are that an available game has been  
        /// selected and the player has entered a name.
        /// </summary>
        // bool that returns true if a host is selected and name is entered else returns false
        //this variable is used to check whether the player can join or not
        public bool CanJoin
        {
            get
            {
                return _canJoin && PlayerName != null && PlayerName.Length != 0 && SelectedGame != null;
            }
            set
            {
                if (SetProperty(ref _canJoin, value))
                {
                    OnPropertyChanged(nameof(CanJoin));
                    JoinGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private bool _canJoin;

        /// <summary>
        /// Indicates whether a player has joined a game.
        /// </summary>
        //indicated whether the player has joined a host or not
        public bool IsJoined
        {
            get
            {
                return _isJoined;
            }
            set
            {
                if (SetProperty(ref _isJoined, value))
                {
                    OnPropertyChanged(nameof(JoinVisibility));
                    OnPropertyChanged(nameof(GameUnderwayVisibility));
                    //OnPropertyChanged(nameof(QuestionAvailableVisibility));
                }
                StateName = _isJoined ? "stand by..." : "lobby";
            }
        }
        private bool _isJoined;

        /// <summary>
        /// The list of available game hosts.
        /// </summary>
        //creates a list of available hosts
        public ObservableCollection<GameHost> AvailableGames { get; } = new ObservableCollection<GameHost>();

        /// <summary>
        /// The game host that has been selected in the UI.
        /// </summary>
        //the host which is selected is being copied into selected game variable/object
        public object SelectedGame
        {
            get
            {
                return _selectedGame;
            }
            set
            {
                if (SetProperty(ref _selectedGame, value))
                {
                    OnPropertyChanged(nameof(SelectedGame));
                    CanJoin = true;
                    JoinGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private object _selectedGame;

        /// <summary>
        /// The command associated with joining a game.
        /// </summary>
        public DelegateCommand JoinGameCommand
        {
            get
            {
                //here ?? and lambda expressions are used
                //the command is calling the JoinGameAsync function
                return _joinGameCommand ?? (_joinGameCommand = new DelegateCommand(
                    async () => await JoinGameAsync(PlayerName, ((GameHost)SelectedGame).Id),
                    () => CanJoin));
            }
        }
        private DelegateCommand _joinGameCommand;

        /// <summary>
        /// The command associated with leaving a game.
        /// </summary>
        public DelegateCommand LeaveGameCommand
        {
            get
            {
                //the command is calling the LeaveGameAsync function 
                return _leaveGameCommand ?? (_leaveGameCommand = new DelegateCommand(
                    async () =>
                    {
                        await LeaveGameAsync(PlayerName);
                        IsJoined = false;
                        //CurrentQuestion = null;
                    }));
            }
        }
        private DelegateCommand _leaveGameCommand;

        /// <summary>
        /// Indicates whether the join game screen is visible in the UI.
        /// </summary>
        public Visibility JoinVisibility => !IsJoined ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Indicates whether the game underway screen in visible in the UI.
        /// </summary>
        public Visibility GameUnderwayVisibility => IsJoined ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Indicates whether to show progress bar or not
        /// </summary>
        public Visibility ProgressBarVisibility => AvailableGames.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        /// <summary>
        /// Indicates whether the question screen is visible in the UI.
        /// </summary>
        //public Visibility QuestionAvailableVisibility => IsJoined && CurrentQuestion != null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Start listening for available games and messages from a game host.
        /// </summary>
        public async Task StartListeningAsync()
        {
            // Start listening for UDP advertisers.
            await _participant.StartListeningAsync();

            // Start listening for TCP messages.
            await _participantCommunicationChannel.StartListeningAsync();
        }

        /// <summary>
        /// Stop listening for available games and messages from a game host.
        /// </summary>
        public async Task StopListeningAsync()
        {
            _participant.StopListening();
            await _participantCommunicationChannel.StopListening();
        }

        /// <summary>
        /// Helper method to join a host game.
        /// </summary>
        private async Task JoinGameAsync(string playerName, Guid host)
        {
            _participant.ListenerMessage = playerName;
            await _participant.ConnectToManagerAsync(host);
            _managerCommunicationChannel = _participant.CreateCommunicationChannel(host);

            // Alert the ViewModel that the player has joined the game successfully.
            await callOnUiThread(() =>
            {
                IsJoined = true;
            });
        }

        /// <summary>
        /// Helper method to leave a game by sending a leave message to the joined game host.
        /// </summary>
        private async Task LeaveGameAsync(string playerName)
        {
            PlayerMessage command = new PlayerMessage()
            {
                Command = PlayerMessageType.Leave,
                PlayerName = playerName
            };

            await _managerCommunicationChannel
                .SendRemoteMessageAsync(command);
        }
    }
}