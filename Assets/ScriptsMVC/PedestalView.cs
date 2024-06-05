using UnityEngine;

namespace CubeMVC
{
    public class PedestalView : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private PedestalModel _pedestalModel;
        private PlayerInputModel _inputModel;
        private SpriteRenderer _pedestalSprite;
        private SpriteRenderer _pedestalCubeSprite;

        private void Start()
        {
            _pedestalModel = _contextProvider.GetContext().PedestalModel;
            _inputModel = _contextProvider.GetContext().PlayerInputModel;
            _pedestalModel.OnInteractedCallback += ChangePedestalView;
            _pedestalSprite = _pedestalModel.PedestalGO.GetComponent<SpriteRenderer>();
            _pedestalCubeSprite = _pedestalModel.PedestalCubeGO.GetComponent<SpriteRenderer>();
        }

        private void ChangePedestalView()
        {
            _pedestalCubeSprite.sprite = _pedestalModel.PedestalItems[(int)_pedestalModel.CurrentWorld].CubeSprite;
            SetCurrentWorld(_pedestalModel.CurrentWorld);
        }

        private void Interact()
        {
            var currentWorldIndex = (int)_pedestalModel.CurrentWorld;
            var currentUIIndex = (int)_pedestalModel.CurrentUI;
            _pedestalSprite.sprite = _pedestalModel.PedestalItems[currentWorldIndex].Sprite;

            foreach (var world in _pedestalModel.Worlds)
            {
                world.SetActive(false);
            }

            _pedestalModel.Worlds[currentWorldIndex].SetActive(true);
            _pedestalModel.PedestalsUI[currentUIIndex].SetActive(false);

            _inputModel.IsPlayerActive.Value = true;
        }

        private void SetCurrentWorld(PedestalWorld currentWorld)
        {
            _pedestalModel.CurrentWorld = currentWorld;
            Interact();
        }

        public void PressedWhite()
        {
            SetCurrentWorld(PedestalWorld.White);
        }

        public void PressedBlue()
        {
            SetCurrentWorld(PedestalWorld.Blue);
        }

        public void PressedOrange()
        {
            SetCurrentWorld(PedestalWorld.Orange);
        }

        public void PressedGreen()
        {
            SetCurrentWorld(PedestalWorld.Green);
        }

        public void PressedPurple()
        {
            SetCurrentWorld(PedestalWorld.Purple);
        }

        public void PressedBlack()
        {
            SetCurrentWorld(PedestalWorld.Black);
        }
    }
}