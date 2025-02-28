namespace sps.BLL.Infrastructure.Services.Implementations
{
    using sps.BLL.Infrastructure.Interfaces;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using Mapster;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class PlaceService : BaseService<PlaceModel, Place, IPlaceRepo>, IPlaceService
    {
        public PlaceService(IPlaceRepo placeRepo, ILogger<PlaceService> logger)
            : base(place, logger)
        {
        }

        // Implement any additional methods specific to Place here
    }
}
