using Buku_Absen.Context;
using Buku_Absen.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Buku_Absen.Repository.Data
{
    public class VisitorRepository2 : GeneralRepository<MyContext, Visitor, string>
    {
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        public VisitorRepository(IConfiguration configuration,MyContext myContext) : base(myContext)
        {
            _configuration = configuration;
            this.myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();

        public Visitor GetId(int key)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API_BukuAbsen"]))
            {
                var spGetId = "SP_DATAVISITOR_GETID";
                parameters.Add("@Id", key);
                var getId = connection.QuerySingleOrDefault<Visitor>(spGetId, parameters, commandType: CommandType.StoredProcedure);
                return getId;
            }
        }

        public IEnumerable<Visitor> GetData()
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API_BukuAbsen"]))
            {
                var spAllData = "SP_DATAVISIOR_CREATE";
                var bukuAbsen = connection.Query<Visitor>(spAllData, commandType: CommandType.StoredProcedure);
                return bukuAbsen;
            }
        }
        public int Insert(Visitor guestBook)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API_BukuAbsen"]))
            {
                var spInsertData = "SP_DATAVISITOR_INSERT";
                parameters.Add("@Nama", guestBook.Nama);
                parameters.Add("@No_Hp", guestBook.No_hp);
                parameters.Add("@Email", guestBook.Email);
                parameters.Add("@Alamat", guestBook.Alamat);
                parameters.Add("@Provinsi", guestBook.Provinsi);
                parameters.Add("@Kota_Kabupaten", guestBook.Kota_Kabupaten);
                parameters.Add("@Kecamatan", guestBook.Kecamatan);
                parameters.Add("@Kelurahan", guestBook.Kelurahan);
                parameters.Add("@KodePos", guestBook.KodePos);
                parameters.Add("@Kehadiran", DateTime.Now);
                var insert = connection.Execute(spInsertData, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }

        }
        public int Update(Visitor guestBook)
        {
           using(SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API_BukuAbsen"]))
            {
                var spUpdate = "SP_DATAVISITOR_UPDATE";
                parameters.Add("@Id", guestBook.Id);
                parameters.Add("@Nama", guestBook.Nama);
                parameters.Add("@No_Hp", guestBook.No_hp);
                parameters.Add("@Email", guestBook.Email);
                parameters.Add("@Alamat", guestBook.Alamat);
                parameters.Add("@Provinsi", guestBook.Provinsi);
                parameters.Add("@Kota_Kabupaten", guestBook.Kota_Kabupaten);
                parameters.Add("@Kecamatan", guestBook.Kecamatan);
                parameters.Add("@Kelurahan", guestBook.Kelurahan);
                parameters.Add("@KodePos", guestBook.KodePos);
                parameters.Add("@Kehadiran", DateTime.Now);
                var update = connection.Execute(spUpdate, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
        public int Delete(int key)
        {
            var delete = myContext.Visitors.Find(key);
            if(delete != null)
            {
                myContext.Visitors.Remove(delete);
                return myContext.SaveChanges();
            }
            return 0;
            //using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:API_BukuAbsen"]))
            //{
            //    var spDelete = "SP_DATAVISITOR_DELETE";
            //    parameters.Add("@Id", key);
            //    var delete = connection.Execute(spDelete, parameters, commandType: CommandType.StoredProcedure);
            //    return delete;
            //}
        }
    }
}
