namespace CubeECS
{
    public struct DialogComponent
    {
        public bool IsActive;
        public DialogItemComponent DialogItem;
        public DialogSystem DialogSystem;
    }

    public struct DialogItemComponent
    {
        public string InputText;
    }
}