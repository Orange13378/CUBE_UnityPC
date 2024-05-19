namespace CubeECS
{
    public struct DialogComponent
    {
        public DialogItemComponent DialogItem;
        public DialogSystem DialogSystem;
        public float TextSpeed;
    }

    public struct DialogItemComponent
    {
        public string InputText;
    }
}