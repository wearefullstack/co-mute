using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DL
{
    [System.ComponentModel.DataObject]
    public abstract class GenericRepository<TEntity, TContext> : BaseRepository<TContext>
          where TEntity : class
          where TContext : DataContext
    {

        public GenericRepository() : base() { }

        public GenericRepository(bool ObjectTrackingEnabled)
            : this()
        {
            DataContext.ObjectTrackingEnabled = ObjectTrackingEnabled;
        }

        #region Properties
        /// <summary>
        /// DataContext should never be accessed to get data from tables
        /// </summary>
        internal TContext DataContext
        {
            get
            {
                return db;
            }
        }


        /// <summary>
        /// Removes the current DataContext from the Request/Thread and calls dispose on wt
        /// </summary>
        protected void DiscardDataContext()
        {
            db.Dispose();
        }

        public new void SubmitChanges()
        {
            base.SubmitChanges();
        }

        public DataLoadOptions LoadOptions
        {
            get
            {
                return DataContext.LoadOptions;
            }
            set
            {
                DataContext.LoadOptions = value;
            }
        }

        #endregion

        #region Helper methods

        private List<PropertyInfo> _primaryKey;
        public List<PropertyInfo> PrimaryKey
        {
            get
            {
                if (_primaryKey == null)
                {
                    _primaryKey =
                        PrimaryKeyName.ConvertAll<PropertyInfo>(
                            a =>
                            {
                                return typeof(TEntity).GetProperty(a.MappedName);
                            });

                }
                return _primaryKey;
            }
        }

        private List<MetaDataMember> _primaryKeyName;
        public List<MetaDataMember> PrimaryKeyName
        {
            get
            {

                MetaTable mapping = DataContext.Mapping.GetTable(typeof(TEntity));

                int count = mapping.RowType.DataMembers.Count(d => d.IsPrimaryKey);
                if (count < 1) throw new Exception(String.Format("Table {0} does not contain a Primary Key field", mapping.TableName));
                //if (count > 1) throw new Exception(String.Format("Table {0} contains a composite primary key field", mapping.TableName));

                List<MetaDataMember> primaryKeys = mapping.RowType.DataMembers.Where(d => d.IsPrimaryKey).ToList();

                _primaryKeyName = primaryKeys;

                return _primaryKeyName;
            }
        }

        private PropertyInfo _timestamp;
        public PropertyInfo TimeStamp
        {
            get
            {
                _timestamp = typeof(TEntity).GetProperty(TimeStampName);
                return _timestamp;
            }
        }

        private string _timestampname;
        public string TimeStampName
        {
            get
            {
                if (String.IsNullOrEmpty(_timestampname))
                {
                    MetaTable mapping = DataContext.Mapping.GetTable(typeof(TEntity));

                    int count = mapping.RowType.DataMembers.Count(d => d.IsVersion);

                    if (count < 1) return String.Empty;
                    if (count > 1) throw new Exception(String.Format("Table {0} contains more than one TimeStamp field", mapping.TableName));
                    MetaDataMember TimeStampKey = mapping.RowType.DataMembers.Where(d => d.IsVersion).SingleOrDefault();
                    _timestampname = TimeStampKey.MappedName;

                }
                return _timestampname;
            }
        }

        protected List<object> GetPrimaryKeyValue(TEntity entity)
        {
            return PrimaryKey.ConvertAll(a => a.GetValue(entity, null));
        }

        protected Binary GetTimeStampValue(TEntity entity)
        {
            return (Binary)TimeStamp.GetValue(entity, null);
        }


        public TEntity GetEntity(TEntity entity)
        {
            return GetEntity(GetPrimaryKeyValue(entity));
        }

        private List<PropertyInfo> _databaseProperties;
        protected List<PropertyInfo> DatabaseProperties
        {
            get
            {
                if (_databaseProperties == null)
                {
                    System.Type entityType = typeof(TEntity);
                    MetaTable mapping = DataContext.Mapping.GetTable(entityType);
                    IEnumerable<MetaDataMember> properties = mapping.RowType.DataMembers.Where(x => x.DbType != null);

                    _databaseProperties = new List<PropertyInfo>();
                    foreach (MetaDataMember dm in properties)
                    {
                        _databaseProperties.Add(entityType.GetProperty(dm.MappedName));
                    }
                }
                return _databaseProperties;
            }
        }

        /// <summary>
        /// Update all properties of a linq object that are not associations with the values in another linq object.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        protected void UpdateOriginalFromChanged(ref TEntity destination, TEntity source)
        {
            foreach (PropertyInfo pi in DatabaseProperties)
            {
                pi.SetValue(destination, pi.GetValue(source, null), null);
            }
        }

        #endregion

        #region Generic CRUD methods
        //---------------------Selects----------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public TEntity GetEntity(object id)
        {
            return GetEntity(new List<object>() { id });
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public TEntity GetEntity(List<object> id)
        {
            int count = 0;
            MetaTable mapping = DataContext.Mapping.GetTable(typeof(TEntity));
            List<MetaDataMember> pkfields = mapping.RowType.DataMembers.Where(d => d.IsPrimaryKey).ToList();
            if (pkfields == null)
                throw new Exception(String.Format("Table {0} does not contain a Primary Key field", mapping.TableName));

            ParameterExpression param = Expression.Parameter(typeof(TEntity), pkfields[0].MappedName);
            MemberExpression property = Expression.Property(param, pkfields[0].Name);
            ConstantExpression value = Expression.Constant(Convert.ChangeType(id[count], pkfields[0].Type));
            Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(property, value), param);

            pkfields.Skip(1).ToList().ForEach(pkfield =>
            {
                count++;
                param = Expression.Parameter(typeof(TEntity), pkfield.MappedName);
                property = Expression.Property(param, pkfield.Name);
                value = Expression.Constant(Convert.ChangeType(id[count], pkfield.Type));
                Expression<Func<TEntity, bool>> MiniPredicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(property, value), param);
                predicate = predicate.And(MiniPredicate);

            });

            return DataContext.GetTable<TEntity>().SingleOrDefault(predicate);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public IQueryable<TEntity> SelectAll()
        {
            return DataContext.GetTable<TEntity>();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> pred)
        {
            return DataContext.GetTable<TEntity>().Where(pred).AsQueryable();
        }

        public int Count()
        {
            return SelectAll().Count();
        }

        //----------------------Insert------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public TEntity Insert(TEntity entity)
        {
            return Insert(entity, string.Empty, true);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        private TEntity Insert(TEntity entity, String Comment)
        {
            return Insert(entity, Comment, true);
        }

        private TEntity Insert(TEntity entity, string Comment, bool submitChanges)
        {
            DataContext.GetTable<TEntity>().InsertOnSubmit(entity);

            if (submitChanges)
                DataContext.SubmitChanges();

            return entity;
        }

        //-----------------------Update-----------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public TEntity Update(TEntity entity)
        {
            return Update(entity, string.Empty, true, false);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public TEntity Update(TEntity entity, bool IgnoreAttach)
        {
            return Update(entity, string.Empty, true, IgnoreAttach);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        private TEntity Update(TEntity entity, string Comment)
        {
            return Update(entity, Comment, true, false);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        private TEntity Update(TEntity entity, bool IgnoreAttach, string Comment)
        {
            return Update(entity, Comment, true, IgnoreAttach);
        }


        private TEntity Update(TEntity entity, string Comment, bool submitChanges, bool IgnoreAttach)
        {
            //ITable itbl = DataContext.GetTable(typeof(TEntity));
            //TEntity original;
            //if (!string.IsNullOrEmpty(TimeStampName))
            //{
            //    using (GenericRepository<TEntity> TGeneric = new GenericRepository<TEntity>())
            //    {
            //        original = TGeneric.GetEntity(entity);
            //        Binary origStamp = GetTimeStampValue(original);
            //        Binary modStamp = GetTimeStampValue(entity);
            //        if (!origStamp.ToString().Equals(modStamp.ToString()))
            //        {
            //            throw new DBConcurrencyException("Concurrency not valid, TimeStamp value invalid");
            //        }
            //    }
            //}

            //Detach<TEntity>(entity);        
            var itbl = DataContext.GetTable<TEntity>();
            if (!IgnoreAttach)
            {
                itbl.Attach(entity);
                itbl.Context.Refresh(RefreshMode.KeepCurrentValues, entity);
            }
            if (submitChanges)
            {
                try
                {
                    DataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
                }
                catch (ChangeConflictException e)
                {
                    //foreach (ObjectChangeConflict occ in DataContext.ChangeConflicts)
                    //{
                    //    ////Keep current values that have changed, 
                    //    ////updates other values with database values

                    //    //occ.Resolve(RefreshMode.KeepChanges);
                    //}
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine(string.Format("Optimistic concurrency error: {0}", e.Message));

                    foreach (ObjectChangeConflict occ in db.ChangeConflicts)
                    {
                        MetaTable metatable = db.Mapping.GetTable(occ.Object.GetType());
                        TEntity entityInConflict = (TEntity)occ.Object;
                        sb.AppendLine(string.Format("Table name: {0}", metatable.TableName));
                        sb.AppendLine(string.Format("Primary Key: {0}", GetPrimaryKeyValue(entityInConflict).FirstOrDefault()));
                        foreach (MemberChangeConflict mcc in occ.MemberConflicts)
                        {
                            object currVal = mcc.CurrentValue;
                            object origVal = mcc.OriginalValue;
                            object databaseVal = mcc.DatabaseValue;
                            MemberInfo mi = mcc.Member;
                            sb.AppendLine(string.Format("Member: {0}", mi.Name));
                            sb.AppendLine(string.Format("current value: {0}", currVal));
                            sb.AppendLine(string.Format("original value: {0}", origVal));
                            sb.AppendLine(string.Format("database value: {0}", databaseVal));
                        }
                    }

                    throw new DBConcurrencyException(sb.ToString());
                }

            }

            return entity;
        }

        //----------------------Delete-------------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public void Delete(TEntity entity)
        {
            Delete(entity, string.Empty, true);
        }

        //----------------------Delete-------------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public void Delete(TEntity entity, string Comment)
        {
            Delete(entity, Comment, true);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public void DeleteAll(IQueryable<TEntity> entity)
        {
            DataContext.GetTable<TEntity>().DeleteAllOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        private void Delete(TEntity entity, string Comment, bool submitChanges)
        {
            TEntity delete = GetEntity(entity);
            DataContext.GetTable<TEntity>().DeleteOnSubmit(delete);

            if (submitChanges)
                DataContext.SubmitChanges();


        }
        #endregion

        #region Detach
        public static void Detach<T>(T entity)
        {

            Type t = entity.GetType();

            System.Reflection.PropertyInfo[] properties = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var property in properties)
            {

                string name = property.Name;

                if (property.PropertyType.IsGenericType &&

                property.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                {

                    property.SetValue(entity, null, null);

                }

            }

            System.Reflection.FieldInfo[] fields = t.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {

                string name = field.Name;

                if (field.FieldType.IsGenericType &&

                field.FieldType.GetGenericTypeDefinition() == typeof(EntityRef<>))
                {

                    field.SetValue(entity, null);

                }

            }

            System.Reflection.EventInfo eventPropertyChanged = t.GetEvent("PropertyChanged");

            System.Reflection.EventInfo eventPropertyChanging = t.GetEvent("PropertyChanging");

            if (eventPropertyChanged != null)
            {

                eventPropertyChanged.RemoveEventHandler(entity, null);

            }

            if (eventPropertyChanging != null)
            {

                eventPropertyChanging.RemoveEventHandler(entity, null);

            }

        }
        #endregion Detach

    }
}
