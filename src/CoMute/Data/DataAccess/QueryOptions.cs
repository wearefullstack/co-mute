﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CoMute.Web.Data.DataAccess
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc";
        public Expression<Func<T, bool>> Where { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }
}