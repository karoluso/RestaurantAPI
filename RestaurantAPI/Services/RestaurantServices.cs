using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using System.Linq.Dynamic.Core;


namespace RestaurantAPI.Services
{
    public class RestaurantServices : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantServices> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");


            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
                new ResourceOperationRequirement(ResourceOperationEnum.Update));

            if (!authorizationResult.Succeeded)
            {
                throw new NotAuthorizedException();
            }


            restaurant = _mapper.Map(dto, restaurant);

            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();
        }


        public void Delete(int id)
        {
            _logger.LogError($"Restaurnt with id: {id} DELETE action invoked");

            var restaurant = _dbContext.Restaurants
                .Include(r => r.Address)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var authorisationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
                new ResourceOperationRequirement(ResourceOperationEnum.Delete)).Result;

            if (!authorisationResult.Succeeded)
            {
                throw new NotAuthorizedException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.Addresses.Remove(restaurant.Address);

            _dbContext.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(_r => _r.Address)
                .Include(_r => _r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return result;
        }


        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(r => query.SearchPhrase == null ||
                 r.Name.ToLower().Contains(query.SearchPhrase.ToLower()) ||
                 r.Description.ToLower().Contains(query.SearchPhrase.ToLower()));

            #region AsEnumerable 
            ////remove dynamic linq core from using 

            //var restaurantProperty = typeof(Restaurant).GetProperty(query.SortBy);

            //IOrderedEnumerable<Restaurant> sortingQuery = null;

            //if (!string.IsNullOrEmpty(query.SortBy))
            //{
            //    sortingQuery = query.SortOrder == SortOrderEnum.ASC ?
            //   baseQuery.AsEnumerable().OrderBy(r => restaurantProperty.GetValue(r, null)) :
            //   baseQuery.AsEnumerable().OrderByDescending(r => restaurantProperty.GetValue(r, null));
            //}
            #endregion


            #region dynamicLINQ
            //add dynamic linq core in  usings

            //if (!string.IsNullOrEmpty(query.SortBy))
            //{
            //    baseQuery = query.SortOrder == SortOrderEnum.ASC ?
            //   baseQuery.OrderBy(query.SortBy) :
            //    baseQuery.OrderBy($"{query.SortBy} desc");
            //}

            #endregion


            #region ExpressionTreeMethod
            //var selectedColumn = query.SortBy;

            //if (!string.IsNullOrEmpty(query.SortBy))
            //{
            //    var sortOrder = query.SortOrder != SortOrderEnum.ASC;
            //    baseQuery = baseQuery.OrderBy2(query.SortBy, sortOrder);
            //}

            #endregion


            #region ExprTreeManual

            //if (!string.IsNullOrEmpty(query.SortBy))
            //{
            //var selectedColumn = query.SortBy;
            //var parameter = Expression.Parameter(typeof(Restaurant));
            //var property = Expression.Property(parameter, selectedColumn);
            // //// var propAsObj = Expression.Convert(property, typeof(object));
            //var expression = Expression.Lambda<Func<Restaurant, object>>(property, parameter); //propertyasObj

            //    baseQuery = query.SortOrder == SortOrderEnum.ASC ?
            //    baseQuery.OrderBy(expression) :
            //    baseQuery.OrderByDescending(expression);
            //}

            #endregion


            #region FromCourse

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columns = new Dictionary<string, Expression<Func<Restaurant, object>>>()
            {
                {nameof(Restaurant.Name) ,r=>r.Name },
                {nameof(Restaurant.Description) ,r=>r.Description},
                {nameof(Restaurant.Category), r=>r.Category },
                {nameof(Restaurant.Address.City), r=>r.Address.City},
            };

                var selectedColum = columns[query.SortBy];
                baseQuery = query.SortOrder == SortOrderEnum.ASC ?
                baseQuery.OrderBy(selectedColum) :
                baseQuery.OrderByDescending(selectedColum);
            }

            #endregion

            var restaurants = baseQuery                                    //sortingQuery.AsQueryable()
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItems = baseQuery.Count();

            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            var result = new PageResult<RestaurantDto>(restaurantDtos, totalItems, query.PageSize, query.PageNumber);

            return result;
        }


        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedByUserId = _userContextService.GetUserId;

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }


    }

    static class Order  //for methodExpressionTree
    {
        public static IQueryable<TEntity> OrderBy2<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                             bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}



