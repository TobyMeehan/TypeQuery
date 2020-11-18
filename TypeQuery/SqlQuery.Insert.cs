using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public partial class SqlQuery<T>
    {
        private InsertSqlStatement SetInsert()
        {
            if (!(GetStatement() is InsertSqlStatement insertStatement))
            {
                insertStatement = new InsertSqlStatement(typeof(T));
                SetStatement(insertStatement);
            }

            return insertStatement;
        }

        private SqlQuery<T> AddInsert(object values)
        {
            SetInsert().AddValues(values);

            return this;
        }

        public SqlQuery<T> Insert(object values)
        {
            return CloneQuery().AddInsert(values);
        }
    }
}
