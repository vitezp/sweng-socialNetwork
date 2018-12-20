﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Query.Predicates;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly IUnitOfWorkProvider Provider;

        protected QueryBase(IUnitOfWorkProvider uowProvider)
        {
            Provider = uowProvider;
        }

        private const int DefaultPageSize = 10;

        public int PageSize { get; private set; } = DefaultPageSize;

        public int? DesiredPage { get; private set; }

        private string sortAccordingTo;
        public string SortAccordingTo
        {
            get => sortAccordingTo;
            protected set
            {
                var properties = typeof(TEntity).GetProperties().Select(prop => prop.Name).Except(new[] { nameof(IEntity.TableName) });
                var matchedName = properties
                    .FirstOrDefault(name => name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                sortAccordingTo = matchedName;
            }
        }

        public bool UseAscendingOrder { get; protected set; }

        public IPredicate Predicate { get; private set; }

        public IQuery<TEntity> Where(IPredicate rootPredicate)
        {
            Predicate = rootPredicate ?? throw new ArgumentException("Root predicate must be defined!");
            return this;
        }

        public IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder = true)
        {
            SortAccordingTo = !string.IsNullOrWhiteSpace(sortAccordingTo) ? sortAccordingTo : throw new ArgumentException($"{nameof(sortAccordingTo)} must be defined!");
            UseAscendingOrder = ascendingOrder;
            return this;
        }

        public IQuery<TEntity> Page(int pageToFetch, int pageSize = DefaultPageSize)
        {
            if (pageToFetch < 1)
            {
                throw new ArgumentException("Desired page number must be greater than zero!");
            }
            if (pageSize < 1)
            {
                throw new ArgumentException("Page size must be greater than zero!");
            }
            DesiredPage = pageToFetch;
            PageSize = pageSize;
            return this;
        }

        public abstract Task<QueryResult<TEntity>> ExecuteAsync();
    }
}