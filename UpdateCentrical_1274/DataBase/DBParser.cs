using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateCentrical_1274.DataModel;


namespace UpdateCentrical_1274.DataBase
{
    public class DBParser
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static DBParser m_ins;
        /// <summary>
        /// select command
        /// </summary>
        private SelectCmd m_selsctCmd;
       
        /// <summary>
        /// update command
        /// </summary>
        private UpdateCmd m_updateCmd;
       

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DBParser));

        readonly string m_connectionString = ConfigurationManager.ConnectionStrings["tnuott"].ConnectionString;

        /// <summary>
        /// constructor
        /// </summary>
        private DBParser()
        {
            m_selsctCmd = new SelectCmd();
            m_updateCmd = new UpdateCmd();
        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static DBParser Instance()
        {
            if (m_ins == null)
            {
                m_ins = new DBParser();
            }
            return m_ins;
        }



        /// <summary>
        /// read string that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the string</returns>
        private string ReadString(OracleDataReader curReader, string clmName)
        {
            string str = ReadNotValidString(curReader, clmName);
            return str;
        }


        /// <summary>
        /// read string that can be empty
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the string</returns>
        private string ReadNotValidString(OracleDataReader curReader, string clmName)
        {
            string val = null;
            try
            {
                if (curReader[clmName] != null)
                {
                    val = curReader[clmName].ToString();
                }
            }
            catch
            {
                string errMsg = "Error: Invalid colum name: " + clmName;
                throw new Exception(errMsg);

            }
            return val;
        }
        /// <summary>
        /// read int that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the int</returns>
        private int ReadInt(OracleDataReader curReader, string clmName)
        {
            int val = 0;
            string str = ReadString(curReader, clmName);
            val = Convert.ToInt32(str);
            return val;
        }

        /// <summary>
        /// read date time that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the int</returns>
        private DateTime ReadDateTime(OracleDataReader curReader, string clmName)
        {
            DateTime val = DateTime.Now;
            string str = ReadString(curReader, clmName);
            val = Convert.ToDateTime(str);
            return val;
        }

        /// <summary>
        /// read boolean
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the boolean</returns>
        private bool ReadBool(OracleDataReader curReader, string clmName)
        {
            return Convert.ToBoolean(ReadInt(curReader, clmName));
        }

        [Obsolete("Message")]
        public IEnumerable<Seler> GetAllSelers()
        {
            Seler seler = null;
            List<Seler> selers = new List<Seler>();
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_selsctCmd.TblName = "TBL_CNTRCL_MB_RECRUIT_1274";
                    m_selsctCmd.AddIntKeyVal("STATUS", 3);
                    using (OracleCommand command = new OracleCommand(m_selsctCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            seler = new Seler
                            {
                               IdNum = ReadInt(reader, "ID_NUM"),
                               MPhone = ReadInt(reader, "M_PHONE"),
                               SelerSsn = ReadString(reader, "SALER_SSN"),
                               Status = ReadInt(reader, "STATUS"),
                               Station = ReadInt(reader, "STATUS"),
                               StampTar = ReadDateTime(reader, "STAMP_TAR"),
                               SmsTar = ReadDateTime(reader, "SMS_TAR"),
                            };
                            selers.Add(seler);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    //insert log error message
                    log.Error($"Exception when try to get all selers, error message: {ex.Message}");   
                }
                finally
                {
                    connection.Close();
                }
            }
            return selers;
        }

        [Obsolete("Message")]
        public bool UpdateSelerStatus(int idnum)
        {
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_updateCmd.TblName = "TBL_CNTRCL_MB_RECRUIT_1274";
                    m_updateCmd.AddIntVal("STATUS",4);
                    m_updateCmd.AddDateTimeVal("STAMP_TAR", "sysdate");
                    m_updateCmd.AddIntKeyVal("ID_NUM", idnum);
                    using (OracleCommand command = new OracleCommand(m_updateCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        int result = command.ExecuteNonQuery();
                        if (result > 0) return true;
                    }
                }
                catch (Exception ex)
                {                    
                    log.Error($"Exception when try to update seler status, error message: {ex.Message}");
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return false;
        }
    }
}
