using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public partial class SqlQuery<T>
    {
        private DeleteSqlStatement SetDelete()
        {
            if (!(GetStatement() is DeleteSqlStatement deleteStatement))
            {
                deleteStatement = new DeleteSqlStatement(typeof(T));
                SetStatement(deleteStatement);
            }

            return deleteStatement;
        }

        private SqlQuery<T> AddDelete()
        {
            SetDelete();

            return this;
        }

        public SqlQuery<T> Delete()
        {
            return CloneQuery().AddDelete();
        }
    }
}
