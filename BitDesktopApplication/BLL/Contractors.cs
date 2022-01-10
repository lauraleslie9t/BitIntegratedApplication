using BitDesktopApplication.DAL;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.BLL
{
    class Contractors : List<Contractor>
    {
        public Contractors()
        {
            SQLHelper helper = new SQLHelper();
        }
        public void GetAllContractors()
        {
            string sql = "SELECT  s.staffId, s.title, s.firstName, s.lastName, s.dob, s.email, s.mobile, s.phone, s.street, s.suburb, s.state, " +
                        " s.postcode, s.country, c.hourlyRate, c.rating, c.region " +
                        " FROM STAFF s, CONTRACTOR c " +
                        " WHERE s.staffID = c.contractorId AND s.archived = 0";
            SQLHelper helper = new SQLHelper();
            DataTable allContractors = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allContractors.Rows)
            {
                Contractor contractorRow = new Contractor(dr);
                this.Add(contractorRow);
            }
        }
        public void GetContractorsById(int staffId)
        {
            string sql = "SELECT  s.staffId, s.title, s.firstName, s.lastName, s.dob, s.email, s.mobile, s.phone, s.street, s.suburb, s.state, " +
                        " s.postcode, s.country, c.hourlyRate, c.rating, c.region " +
                        " FROM STAFF s, CONTRACTOR c " +
                        " WHERE s.staffID = c.contractorId AND s.archived = 0 " +
                        " AND staffId = " + staffId; ;
            SQLHelper helper = new SQLHelper();
            DataTable allContractors = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allContractors.Rows)
            {
                Contractor contractorRow = new Contractor(dr);
                this.Add(contractorRow);
            }
        }
        public void GetContractorsByFirstName(string firstName)
        {
            string sql = "SELECT  s.staffId, s.title, s.firstName, s.lastName, s.dob, s.email, s.mobile, s.phone, s.street, s.suburb, s.state, " +
                        " s.postcode, s.country, c.hourlyRate, c.rating, c.region " +
                        " FROM STAFF s, CONTRACTOR c " +
                        " WHERE s.staffID = c.contractorId AND s.archived = 0" +
                        " AND firstName LIKE '%' + '" + firstName + "' + '%'";
            SQLHelper helper = new SQLHelper();
            DataTable allContractors = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allContractors.Rows)
            {
                Contractor contractorRow = new Contractor(dr);
                this.Add(contractorRow);
            }
        }
        public void GetContractorsByLastName(string lastName)
        {
            string sql = "SELECT  s.staffId, s.title, s.firstName, s.lastName, s.dob, s.email, s.mobile, s.phone, s.street, s.suburb, s.state, " +
                        " s.postcode, s.country, c.hourlyRate, c.rating, c.region " +
                        " FROM STAFF s, CONTRACTOR c " +
                        " WHERE s.staffID = c.contractorId AND s.archived = 0" +
                        " AND lastName LIKE '%' + '" + lastName + "' + '%'";
            SQLHelper helper = new SQLHelper();
            DataTable allContractors = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allContractors.Rows)
            {
                Contractor contractorRow = new Contractor(dr);
                this.Add(contractorRow);
            }
        }
        public void GetContractorsByDob(DateTime dob)
        {
            string sql = "SELECT  s.staffId, s.title, s.firstName, s.lastName, s.dob, s.email, s.mobile, s.phone, s.street, s.suburb, s.state, " +
                        " s.postcode, s.country, c.hourlyRate, c.rating, c.region " +
                        " FROM STAFF s, CONTRACTOR c " +
                        " WHERE s.staffID = c.contractorId AND s.archived = 0" +
                        " and dob = '" + dob.ToString("yyyy-MM-dd") + "'";
            SQLHelper helper = new SQLHelper();
            DataTable allContractors = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allContractors.Rows)
            {
                Contractor contractorRow = new Contractor(dr);
                this.Add(contractorRow);
            }
        }
    }
}
