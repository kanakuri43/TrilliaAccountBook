using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrilliaAccountBookf.Models
{
    public class CommonFunctions
    {
        public int OperatorCodeToId(string operatorCode)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " id "
                    + "FROM operators "
                    + "WHERE "
                    + " state = 0 "
                    + " AND operator_code = '" + operatorCode + "'";
            var dr = dc.ReadAsDataReader();
            if (dr == null)
            {
                return -1;
            }
            else
            {
                dr.Read();
                return (int)dr[0];
            }
        }

        public int CustomerCodeToId(string customerCode)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " id "
                    + "FROM customers "
                    + "WHERE "
                    + " state = 0 "
                    + " AND customer_code = '" + customerCode + "'";
            var dr = dc.ReadAsDataReader();
            if (dr == null)
            {
                return -1;
            }
            else
            {
                dr.Read();
                return (int)dr[0];
            }
        }

        public int StaffCodeToId(string staffCode)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " id "
                    + "FROM staffs "
                    + "WHERE "
                    + " state = 0 "
                    + " AND staff_code = '" + staffCode + "'";
            var dr = dc.ReadAsDataReader();
            if (dr == null)
            {
                return -1;
            }
            else
            {
                dr.Read();
                return (int)dr[0];
            }
        }

        public int ProductCodeToId(string productCode)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + " id "
                    + "FROM products "
                    + "WHERE "
                    + " state = 0 "
                    + " AND product_code = '" + productCode + "'";
            var dr = dc.ReadAsDataReader();
            if (dr == null)
            {
                return -1;
            }
            else
            {
                dr.Read();
                return (int)dr[0];
            }
        }

    }
}
