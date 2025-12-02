using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.DynamicData;

namespace FashionShop.Helper
{
    public class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["FashionShopDB"].ConnectionString;

        /// <summary>
        /// Lấy connection tới database
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Thwucj thi câu lệnh query SQL và trả về DataTable
        /// </summary>

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            using(SqlConnection conn = GetConnection())
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if(parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Thực thi Stored Procedure và trả về DataTable
        /// </summary>

        public static DataTable ExecuteStoredProcedure(string procName, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Thực thi Stored Procedure không trả về kết quả dùng cho INSERT, UPDATE, DELETE
        /// </summary>

        public static int ExecuteStoredProcNonQuery(string procName, SqlParameter[] parameters = null)
        {
            int result = 0;

            using(SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        /// <summary>
        /// Thực thi Stored Procedure với Output Parameter
        /// </summary>

        public static object ExecuteStoredProcWithOutput(string procName, SqlParameter[] parameters, string ouputParamName)
        {
            object result = null;

            using(SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters[ouputParamName].Value;
                }
            }
            return result;
        }

        ///<summary>
        /// Thực thi Stored Procedure về giá trị scalar
        /// </summary>
        
        public static object ExecuteStoredProdScalar(string procName, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }

        ///<summary>
        /// Thực thi câu query trả về một giá trị đơn 
        /// </summary>

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }

        ///<summary>
        /// Thực thi câu lệnh query không trả về kết quả dùng với INSERT, UPDATE, DELETE
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int result = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        ///<summary>
        /// Thực thi nhiều câu lệnh trong một transaction
        /// </summary>
        public static bool ExecuteTransaction(List<string> queries, List<SqlParameter[]> parametersList = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    for (int i = 0; i < queries.Count; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(queries[i], conn, transaction))
                        {
                            if (parametersList != null && i < parametersList.Count)
                            {
                                cmd.Parameters.AddRange(parametersList[i]);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("Transaction error: " + ex.Message);
                    return false;
                }
            }
        }

    }
}