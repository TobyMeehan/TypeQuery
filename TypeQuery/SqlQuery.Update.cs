using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public partial class SqlQuery<T>
    {
        private UpdateSqlStatement SetUpdate()
        {
            if (!(GetStatement() is UpdateSqlStatement updateStatement))
            {
                updateStatement = new UpdateSqlStatement(typeof(T));
                SetStatement(updateStatement);
            }

            return updateStatement;
        }

        private SqlQuery<T> AddUpdate(object values)
        {
            SetUpdate().AddValues(values);

            return this;
        }

        public SqlQuery<T> Update(object values)
        {
            return CloneQuery().AddUpdate(values);
        }
    }
}
