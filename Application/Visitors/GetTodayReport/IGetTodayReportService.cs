using Application.Interfaces.Contexts;
using Domain.Visitors;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors.GetTodayReport
{
    public interface IGetTodayReportService
    {
        ResultTodayReportDto Execute();
    }

    public class GetTodayReportService : IGetTodayReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _visitorMongoCollection;
        public GetTodayReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _visitorMongoCollection = _mongoDbContext.GetCollection();
        }
        public ResultTodayReportDto Execute()
        {
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.AddDays(1);
            var todayPageViewCount = _visitorMongoCollection.AsQueryable().Where(x => x.Time > start && x.Time < end).LongCount();
            var todayVisitorCount = _visitorMongoCollection.AsQueryable().Where(x => x.Time > start && x.Time < end).GroupBy(x => x.VisitorId).LongCount();
            var AllPageViewCount = _visitorMongoCollection.AsQueryable().LongCount();
            var AllVisitorCount = _visitorMongoCollection.AsQueryable().GroupBy(x => x.VisitorId).LongCount();

            VisitCountDto visitPerHour = GetVisitPerHour(start, end);

            VisitCountDto visitPerDay = GetVisitPerDay();

            var visitors = _visitorMongoCollection.AsQueryable().OrderByDescending(x => x.Time).Take(10).Select(x => new VisitorDto()
            {
                Id= x.Id,
                Browser = x.Browser.Family,
                CurrentLink = x.CurrentLink,
                IP = x.IP,
                OperationSystem = x.OperationSystem.Family,
                IsSpider = x.Device.IsSpider,
                ReferrerLink = x.ReferrerLink,
                Time = x.Time 

            }).ToList();

            return new ResultTodayReportDto()
            {
                GeneralStats = new GeneralStatsDto()
                {
                    TotalPageViews = AllPageViewCount,
                    TotalVisitors = AllVisitorCount,
                    PageViewsPerVisit = GetAvg(AllPageViewCount, AllVisitorCount),
                    VisitPerDay = visitPerDay
                },
                Today = new TodayDto()
                {
                    PageViews = todayPageViewCount,
                    ViewsPerVisitor = GetAvg(todayPageViewCount, todayVisitorCount),
                    Visitors = todayVisitorCount,
                    VisitPerHour = visitPerHour,
                },
                Visitors = visitors
            };

        }

        private VisitCountDto GetVisitPerHour(DateTime start, DateTime end)
        {
            var TodayPageViewList = _visitorMongoCollection.AsQueryable().Where(x => x.Time >= start && x.Time < end).Select(x => x.Time).ToList();

            VisitCountDto visitPerHour = new VisitCountDto()
            {
                Display = new string[24],
                Value = new int[24]
            };

            for (int i = 0; i <= 23; i++)
            {
                visitPerHour.Display[i] = i.ToString();
                visitPerHour.Value[i] = TodayPageViewList.Where(x => x.Hour == i).Count();
            }

            return visitPerHour;
        }

        private VisitCountDto GetVisitPerDay()
        {
            DateTime MonthStart = DateTime.Now.Date.AddDays(-30);
            DateTime MonthEnds = DateTime.Now.Date.AddDays(1);

            var Month_PageViewList = _visitorMongoCollection.AsQueryable().Where(x => x.Time >= MonthStart && x.Time < MonthEnds)
                .Select(x => x.Time).ToList();

            VisitCountDto visitPerDay = new VisitCountDto()
            {
                Display = new string[31],
                Value = new int[31]
            };

            for (int i = 0; i <= 30; i++)
            {
                var currentday = DateTime.Now.AddDays(i * (-1));
                visitPerDay.Display[i] = i.ToString();
                visitPerDay.Value[i] = Month_PageViewList.Where(x => x.Date == currentday.Date).Count();
            }

            return visitPerDay;
        }

        private float GetAvg(long VisitPage, long Visitor)
        {
            if (Visitor == 0)
            {
                return 0;
            }
            else {
                return VisitPage / Visitor;
            }  
        }
        

    }
    public class ResultTodayReportDto
    {
        public GeneralStatsDto GeneralStats { get; set; }
        public TodayDto Today { get; set; }    
        public List<VisitorDto> Visitors { get; set; }
    }

    public class GeneralStatsDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewsPerVisit { get; set; }
        public VisitCountDto VisitPerDay { get; set; }


    }

    public class TodayDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float ViewsPerVisitor { get; set; }
        public VisitCountDto VisitPerHour { get; set; }
    }

    public class VisitCountDto
    {
        public string[] Display { get; set; }   
        public int[] Value { get; set; }    
    }

    public class VisitorDto
    {
        public string Id { get; set; }  
        public string IP { get; set; }  
        public string CurrentLink { get; set; }  
        public string ReferrerLink { get; set; }  
        public string Browser { get; set; }  
        public string OperationSystem { get; set; }  
        public bool IsSpider { get; set; }  
        public DateTime Time { get; set; }  
        
    }


}
