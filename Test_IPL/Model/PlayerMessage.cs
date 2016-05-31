namespace Test_IPL.Model
{
    /// <summary>
    /// A message that is from a player to the game host.
    /// </summary>
    public class PlayerMessage
    {
        /// <summary>
        /// The command requested by the player.
        /// </summary>
        public PlayerMessageType Command { get; set; }
        /// <summary>
        /// The name of the player that sent the command.
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// The index of the Question.Options list that the player chose.
        /// </summary>
        public int QuestionAnswer { get; set; }
    }

    /// <summary>
    /// An enumeration representing the available commands that players can send to the game host.
    /// </summary>
    public enum PlayerMessageType
    {
        Join = 0,
        Leave,
        Answer
    };
}