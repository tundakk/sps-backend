namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class PlaceService : BaseService<PlaceModel, Place, IPlaceRepo>, IPlaceService
    {
        public PlaceService(IPlaceRepo placeRepo, ILogger<PlaceService> logger)
            : base(placeRepo, logger)
        {
        }

        // Implement any additional methods specific to Place here
    }
}