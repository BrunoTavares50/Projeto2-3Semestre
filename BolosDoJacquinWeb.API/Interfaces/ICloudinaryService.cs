namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface ICloudinaryService
    {
        // IFormFile: arquivo binário que chaega no multipart/form-data
        //É a imagem!
        Task<string> UploadImagem(IFormFile arquivo);
    }
}
