namespace ServicesLayer.Common
{
    public interface IModelDataAnnotationsCheck
    {
        void ValidateModelDataAnnotations<TDomainModel>(TDomainModel domainModel);
        bool IsValidEmail(string email);
    }
}