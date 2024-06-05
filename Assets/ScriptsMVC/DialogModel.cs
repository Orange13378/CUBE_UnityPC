namespace CubeMVC
{
    public class DialogModel
    {
        public string InputText;
        public delegate void OnDialogStarted();
        public OnDialogStarted OnDialogStart;
        public float TextSpeed;

        public DialogModel(float textSpeed)
        {
            TextSpeed = textSpeed;
        }
    }
}