﻿using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Infrastructure.MappingProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest1.Builders;
using Xunit;

namespace UnitTest1.Core.Application.CatalogTypeTest
{
    public class GetListTest
    {
        [Fact]
        public void List_should_return_list_of_catalogtypes()
        {
            //Arrange
            var dataBasebuilder = new DatabasedBuilder();
            var dbContext = dataBasebuilder.GetDbContext();

            var mockMapper = new MapperConfiguration(ctg =>
            {
                ctg.AddProfile(new CatalogMappingProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var service = new CatalogTypeService(dbContext, mapper);

            service.Add(new CatalogTypeDto()
            {
                Id = 1,
                Type = "t1"
            });

            service.Add(new CatalogTypeDto()
            {
                Id = 2,
                Type = "t2"
            });

            var result = service.GetList(null, 1, 20);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);


        }
    }
}
