using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, Guid activityId)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext
             .Activities
             .FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            dbContext.Activities.Remove(activity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
            };
        }
    }
}
