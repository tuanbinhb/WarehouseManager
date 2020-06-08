using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Diagnostics.Tracing;
using jQueryAjaxInAsp.NETMVC.Models;

namespace jQueryAjaxInAsp.NETMVC.Utility
{
    public class ExcelReader
    {
        public bool IsUserInRole(string loginName)
        {
            bool inRole = false;
            string connString = ConfigurationManager.ConnectionStrings["login"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", oledb);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i].ItemArray[0].Equals(loginName))
                    {
                        inRole = Int32.Parse(ds.Tables[0].Rows[i].ItemArray[2].ToString()) != 0;
                        break;
                    }
                }

                return inRole;
            }
            catch (Exception ex)
            {
                return inRole;
            }
            finally
            {
                oledb.Close();
            }
        }

        public string GetUserPassword(string loginName)
        {
            string password = "";
            string connString = ConfigurationManager.ConnectionStrings["login"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", oledb);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i].ItemArray[0].Equals(loginName)) {
                        password = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                        break;
                    }
                }

                return password;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                oledb.Close();
            }
        }

        public bool editConsumable(ConsumableMaterialModel item) {
            string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("UPDATE [Sheet1$] SET [耗材名稱] = '" + item.materialName+ "',[ECS系統料號] = '" + item.materialECSCode + "',[ECS名稱] = '" + item.materialECSName + "',[型號] = '" + item.materialSpecification + "',[單位] = '" + item.materialUnit + "',[存放位置] = '" + item.materialStorageLocation + "',[適用機種] = '" + item.materialKind + "',[廠家] = '" + item.materialSupplier + "',[參考價值] = '" + item.materialReferenceValue + "',[購買] = '" + item.materialTimeCanGet + "',[請購數量] = '" + item.materialQuantity + "',[安全] = '" + item.materialSafety + "',[領用數量] = '" + item.materialNumberRecipient + "',[退庫良品數量] = '" + item.materialGoodNumberReturn + "',[退庫不良數量] = '" + item.materialBadNumberReturn + "',[庫存] = '" + (Int32.Parse(item.materialQuantity) - Int32.Parse(item.materialNumberRecipient) + Int32.Parse(item.materialGoodNumberReturn)).ToString() + "',[備註] = '" + item.materialRemark + "',[ImagePath] = '" + item.materialImagePath + "' WHERE [耗材料號] = '" + item.materialCode + "'", oledb);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                oledb.Close();
            }
        }

        public bool checkCodeExistOrNot(String id) {
            var result = false;
            string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", oledb);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    if (id.Equals(ds.Tables[0].Rows[i].ItemArray[0].ToString().Trim())) {
                        result = true;
                        break;
                    }
                }
               
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                oledb.Close();
            }
        }
        public bool addNewConsumable(ConsumableMaterialModel prod) {
           string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("INSERT INTO [Sheet1$]([耗材料號], [耗材名稱], [ECS系統料號], [ECS名稱], [型號], [單位], [存放位置], [適用機種], [廠家], [參考價值], [購買], [請購數量], [安全], [領用數量], [退庫良品數量], [退庫不良數量], [庫存], [備註], [ImagePath]) VALUES('" + prod.materialCode+"', '"+prod.materialName+ "', '" + prod.materialECSCode + "', '" + prod.materialECSName + "', '" + prod.materialSpecification + "', '" + prod.materialUnit + "', '" + prod.materialStorageLocation + "', '" + prod.materialKind + "', '" + prod.materialSupplier + "', '" + prod.materialReferenceValue + "','" + prod.materialTimeCanGet + "','" + prod.materialQuantity + "','" + prod.materialSafety + "','" + prod.materialNumberRecipient + "','" + prod.materialGoodNumberReturn + "','" + prod.materialBadNumberReturn + "','" + (Int32.Parse(prod.materialQuantity) - Int32.Parse(prod.materialNumberRecipient) + Int32.Parse(prod.materialGoodNumberReturn)).ToString() + "','" + prod.materialRemark + "','" + prod.materialImagePath + "')", oledb);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                oledb.Close();
            }
        }

        public ConsumableMaterialModel getConsumableById(string id) {
            ConsumableMaterialModel model = new ConsumableMaterialModel();
            string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$] WHERE [耗材料號] = '" + id + "'", oledb);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                model.materialCode = ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim();
                model.materialName = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                model.materialECSCode = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                model.materialECSName = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                model.materialSpecification = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                model.materialUnit = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                model.materialStorageLocation = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                model.materialKind = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                model.materialSupplier = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                model.materialReferenceValue = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                model.materialTimeCanGet = ds.Tables[0].Rows[0].ItemArray[10].ToString();
                model.materialQuantity = ds.Tables[0].Rows[0].ItemArray[11].ToString();
                model.materialSafety = ds.Tables[0].Rows[0].ItemArray[12].ToString();
                model.materialNumberRecipient = ds.Tables[0].Rows[0].ItemArray[13].ToString();
                model.materialGoodNumberReturn = ds.Tables[0].Rows[0].ItemArray[14].ToString();
                model.materialBadNumberReturn = ds.Tables[0].Rows[0].ItemArray[15].ToString();
                model.materialRemainQuantity = ds.Tables[0].Rows[0].ItemArray[16].ToString();
                model.materialRemark = ds.Tables[0].Rows[0].ItemArray[17].ToString();
                model.materialImagePath = String.IsNullOrEmpty(ds.Tables[0].Rows[0].ItemArray[18].ToString()) ? "~/AppFiles/Images/default.png" : ds.Tables[0].Rows[0].ItemArray[18].ToString();
                model.needToBuyMore = (bool)(Int32.Parse(ds.Tables[0].Rows[0].ItemArray[16].ToString()) < Int32.Parse(ds.Tables[0].Rows[0].ItemArray[12].ToString()));
                return model;
            }
            catch (Exception ex)
            {
                return model;
            }
            finally
            {
                oledb.Close();
            }
        }
        public bool deteleMaterialById(string id) {
            string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("UPDATE [Sheet1$] SET [Deleted] = 'TRUE' WHERE [耗材料號] = '" + id+"'", oledb);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                oledb.Close();
            }
        }

        private int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }

        public List<ConsumableMaterialModel> returnListConsumable() {
            string connString = ConfigurationManager.ConnectionStrings["consumable"].ConnectionString;
            OleDbConnection oledb = new OleDbConnection(connString);
            try
            {
                oledb.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", oledb);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                var product = new List<ConsumableMaterialModel>();
                product = (from rows in ds.Tables[0].AsEnumerable()
                           select new ConsumableMaterialModel
                           {
                               materialCode = rows[0].ToString().Trim(),
                               materialName = rows[1].ToString(),
                               materialECSCode = rows[2].ToString(),
                               materialECSName = rows[3].ToString(),
                               materialSpecification = rows[4].ToString(),
                               materialUnit = rows[5].ToString(),
                               materialStorageLocation = rows[6].ToString(),
                               materialKind = rows[7].ToString(),
                               materialSupplier = rows[8].ToString(),
                               materialReferenceValue = rows[9].ToString(),
                               materialTimeCanGet = rows[10].ToString(),
                               materialQuantity = rows[11].ToString(),
                               materialSafety = rows[12].ToString(),
                               materialNumberRecipient = rows[13].ToString(),
                               materialGoodNumberReturn = rows[14].ToString(),
                               materialBadNumberReturn = rows[15].ToString(),
                               materialRemainQuantity = rows[16].ToString(),
                               materialRemark = rows[17].ToString(),
                               materialImagePath = String.IsNullOrEmpty(rows[18].ToString()) ? "~/AppFiles/Images/default.png" : rows[18].ToString(),
                               isDeleted = String.IsNullOrEmpty(rows[19].ToString()) ? false : Convert.ToBoolean(rows[19].ToString()),
                               needToBuyMore = (bool)(ConvertStringToInt(rows[16].ToString()) < ConvertStringToInt(rows[12].ToString()))

            }).ToList();
                return product;
            }
            catch (Exception ex)
            {
                return new List<ConsumableMaterialModel>();
            }
            finally
            {
                oledb.Close();
            }
        }
        
    }
}
