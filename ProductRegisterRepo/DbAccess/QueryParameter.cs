using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRegister.DbAccess
{
    public class QueryParameter
    {
        public QueryParameter(string strParameterName, object objParameterValue)
        {
            this.strParameterName = strParameterName;
            this.objParameterValue = objParameterValue;
        }
        private string _strParameterName;
        private object _objParameterValue;

        public string strParameterName { get { return this._strParameterName; } set { this._strParameterName = value; } }
        public object objParameterValue { get { return this._objParameterValue; } set { this._objParameterValue = value; } }
    }
}
