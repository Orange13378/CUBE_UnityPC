using UnityEngine.UI;

namespace CubeMVC
{
    public class ImageModel
    {
        public Image WhiteImage;
        public Image BlackImage;
        public bool LightUp;
        public bool LightDown;

        public ImageModel(Image whiteImage, Image blackImage)
        {
            WhiteImage = whiteImage;
            BlackImage = blackImage;
        }
    }
}