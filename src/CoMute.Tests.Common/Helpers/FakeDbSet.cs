using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;


namespace CoMute.Tests.Common.Helpers
{
    public class FakeDbSet<T> : EnumerableQuery<T>, IDbSet<T> where T : class, new()
    {
        readonly ObservableCollection<T> _collection;

        private static ObservableCollection<T> CreateDefaultCollection(IEnumerable<T> enumerable)
        {
            return enumerable != null ? new ObservableCollection<T>(enumerable) : new ObservableCollection<T>();
        }

        public FakeDbSet(IEnumerable<T> enumerable = null) : this(CreateDefaultCollection(enumerable))
        {
        }

        private FakeDbSet(ObservableCollection<T> collection) : base(collection)
        {
            _collection = collection;
        }

        public ObservableCollection<T> Local
        {
            get { return _collection; }
        }

        public T Add(T entity)
        {
            _collection.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            _collection.Add(entity);
            return entity;
        }

        public T Create()
        {
            return new T();
        }

        public TDerivedEntity Create<TDerivedEntity>()
            where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Remove(T entity)
        {
            return _collection.Remove(entity) ? entity : null;
        }
    }

}