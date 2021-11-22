using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Zadatak.Models;
using Zadatak.Utils;

namespace Zadatak.Dal
{
    class SqlKlubRepository : IRepository<Klub>
    {

        private const string IdKlubParam = "@idKlub";
        private const string NameParam = "@name";

        private const string ProcUpdate = "UpdateKlub";
        private const string ProcAdd = "AddKlub";
        private const string ProcDelete = "DeleteKlub";
        private const string ProcGetAll = "GetKlubs";
        private const string ProcGet = "GetKlub";

        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "fru1tc@k3");

        public void Add(Klub klub)
        {
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcAdd;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NameParam, klub.Name);

                    SqlParameter idKlub = new SqlParameter(IdKlubParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idKlub);
                    cmd.ExecuteNonQuery();
                    klub.IDKlub = (int)idKlub.Value;
                }
            }
        }

        public void Delete(Klub klub)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcDelete;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdKlubParam, klub.IDKlub);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Klub> GetAll()
        {
            IList<Klub> klubs = new List<Klub>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcGetAll;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            klubs.Add(ReadKlub(dr));
                        }
                    }
                }
            }
            return klubs;
        }

        public Klub Get(int idKlub)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcGet;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdKlubParam, idKlub);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadKlub(dr);
                        }
                    }
                }
            }
            throw new Exception("Klub does not exist");
        }
        private Klub ReadKlub(SqlDataReader dr) => new Klub
        {
            IDKlub = (int)dr[nameof(Klub.IDKlub)],
            Name = dr[nameof(Klub.Name)].ToString()


        };

        public void Update(Klub klub)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ProcUpdate;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NameParam, klub.Name);

                    cmd.Parameters.AddWithValue(IdKlubParam, klub.IDKlub);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
