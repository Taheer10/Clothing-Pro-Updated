using System.Collections.Generic;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.Models;

namespace ClothingPro.BusinessLayer.Interface
{
    public interface IColorImagesService
    {
        List<ColorImagesDTO> GetAllColorImages();
        string GetValueByName(string ColorImagesName);
        ColorImagesDTO GetColorImagesById(string ColorImagesName);
        bool DeleteColorImages(int mnId);
        ColorImagesDTO GetColorImagesByIdd(int MnId);
        int CreateColorImages(ColorImagesDTO ColorImagesUI);
    }
}