using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors.VisitorOnline
{
    public interface IVisitorOnlineService
    {
        void ConnectUser(string ClientId);
        void DisConnectUser(string ClientId);
        int GetCount();
    }

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
            
        }

        public void DisConnectUser(string ClientId)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }
    }

}
