using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors.VisitorOnline
{
    public class VisitorOnlineService : IVisitorOnlineService
    {
        private readonly IMongoDbContext<OnlineVisitor> _mongoDbContext;
        private readonly IMongoCollection<OnlineVisitor> _mongoCollection;
        public VisitorOnlineService(IMongoDbContext<OnlineVisitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _mongoCollection = _mongoDbContext.GetCollection();
        }
        public void ConnectUser(string ClientId)
        {
            var exist = _mongoCollection.AsQueryable().FirstOrDefault(x=>x.ClientId==ClientId);
            if (exist == null)
            {
                _mongoCollection.InsertOne(new OnlineVisitor()
                {
                    ClientId = ClientId,
                    Time = DateTime.Now,
                });
            }
        }

        public void DisConnectUser(string ClientId)
        {
            _mongoCollection.FindOneAndDelete(p=>p.ClientId== ClientId);
        }

        public int GetCount()
        {
            return _mongoCollection.AsQueryable().Count();
        }
    }

}
