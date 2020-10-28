using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateCentrical_1274.DataBase
{
    public abstract class DbCmd
    {
        /// <summary>
        /// the table name
        /// </summary>
        protected string m_tblName;

        /// <summary>
        /// Init the members
        /// </summary>
        abstract protected void Init();

        /// <summary>
        /// Get the command
        /// </summary>
        /// <returns>the command</returns>
        abstract public string GetCmd();

        /// <summary>
        /// Constructor
        /// </summary>
        public DbCmd()
        {
            Init();
        }
        public string TblName
        {
            set { m_tblName = value; }
        }

        /// <summary>
        /// Get text value
        /// </summary>
        /// <param name="val">the value</param>
        /// <returns>return value</returns>
        protected string GetTextVal(string val)
        {
            val = $@"'{val}'";
            return val;
        }

        /// <summary>
        /// Get boolian value
        /// </summary>
        /// <param name="val">the value</param>
        /// <returns>return value</returns>
        protected int GetBoolVal(bool val)
        {
            if (val) return 1;
            return 0;
        }
    }
}
