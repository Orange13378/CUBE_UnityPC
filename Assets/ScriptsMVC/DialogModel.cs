namespace CubeMVC
{
    public class DialogModel
    {
        public delegate void OnDialogStarted(string text);
        public OnDialogStarted OnDialogStart;
        public float TextSpeed;

        public DialogModel(float textSpeed)
        {
            TextSpeed = textSpeed;
        }
    }
}