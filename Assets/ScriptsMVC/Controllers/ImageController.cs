using UnityEngine;
using UnityEngine.UI;

namespace CubeMVC
{
    public class ImageController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private ImageModel _imageModel;
        private PedestalModel _pedestalModel;
        private Image _image;

        public void Start()
        {
            _imageModel = _contextProvider.GetContext().ImageModel;
            _pedestalModel = _contextProvider.GetContext().PedestalModel;
            _image = _imageModel.WhiteImage;
        }

        public void Update()
        {
            if (_pedestalModel.CurrentWorld == PedestalWorld.Black)
            {
                _image = _imageModel.BlackImage;
            }

            if (_imageModel.LightUp)
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + 0.3f * Time.deltaTime);

            if (_imageModel.LightDown)
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - 0.3f * Time.deltaTime);
        }
    }
}