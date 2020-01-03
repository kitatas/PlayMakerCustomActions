namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnityRoom")]
    public class UnityRoomTweet : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The game id on the unityroom.")]
        public string gameId;

        [RequiredField]
        [Tooltip("Set Tweet text.")]
        public FsmString tweetText;

        [Tooltip("Set hash tag name.")]
        public string[] hashTags;

        public override void Reset()
        {
            gameId = "";
            tweetText = "";
            hashTags = null;
        }

        public override void OnEnter()
        {
            naichilab.UnityRoomTweet.Tweet(gameId, tweetText.Value, hashTags);
        }
    }
}