using System.Drawing;

namespace TrafficGameCore
{
    /// <summary>
    /// Class which represents cars Images from the sprite sheet
    /// </summary>
    public class CarsImages
    {
        /// <summary>
        /// List of cars images
        /// </summary>
        public Image [] CarsImagesArray { get; set; }

        /// <summary>
        /// Constructor which is filling up the list
        /// </summary>
        public CarsImages(Image spriteSheet)
        {
            CarsImagesArray = new Image[12];
            for (int i = 0; i < 12; i++)
            {
                CarsImagesArray[i] = new Bitmap(70, 145);
                var graphics = Graphics.FromImage(CarsImagesArray[i]);
                int srcY;
                if (i < 4)
                    srcY = 0;
                else if (i < 8)
                    srcY = 145;
                else
                    srcY = 290;
                graphics.DrawImage(spriteSheet, new Rectangle(0, 0, 70, 145), new Rectangle((i % 4) * 70, srcY, 70, 145), GraphicsUnit.Pixel);
                graphics.Dispose();
            }
        }
    }
}
