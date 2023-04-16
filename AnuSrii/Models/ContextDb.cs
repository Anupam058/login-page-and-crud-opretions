using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnuSrii.Models
{
    public class ContextDb
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public bool LoginEmployee(ForLogin obj)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select Email,Password from Person_Login where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("Email", obj.Email);
            cmd.Parameters.AddWithValue("Password", obj.Password);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<EmployeeDetails> GetEmployees()
        {
            List<EmployeeDetails> EmployeesList = new List<EmployeeDetails>();

            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("SpGetEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EmployeeDetails emp = new EmployeeDetails();
                emp.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Name = dr.GetValue(1).ToString();
                emp.Email = dr.GetValue(2).ToString();
                emp.Address = dr.GetValue(3).ToString();
                emp.Age = Convert.ToInt32(dr.GetValue(4).ToString());
                emp.Salary = Convert.ToInt32(dr.GetValue(5).ToString());
                EmployeesList.Add(emp);
            }
            con.Close();

            return EmployeesList;
        }



        public bool AddEmployee(EmployeeDetails emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpInEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Address", emp.Address);
            cmd.Parameters.AddWithValue("@Age", emp.Age);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateEmployee(EmployeeDetails emp)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Address", emp.Address);
            cmd.Parameters.AddWithValue("@Age", emp.Age);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool DeleteEmployee(int Id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





    }
}