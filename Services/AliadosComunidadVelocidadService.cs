using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using CASSAWS.Data;
using CASSAWS.Models;

namespace CASSAWS.Services
{
    public class AliadosComunidadVelocidadService
    {
        public async Task<IEnumerable<AliadosComunidadVelocidadModel>> getAliadosVel()
        {
            SqlCnUrl SqlCnUrl = new SqlCnUrl();
            string conexion = SqlCnUrl.getCon();

            IEnumerable<AliadosComunidadVelocidadModel> data;
            using (var conn = new SqlConnection(conexion))
            {
                string query = @"exec SP_SyncAliadosComunidad_Velocidad";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<AliadosComunidadVelocidadModel>(query, new {  }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }
    }
}
