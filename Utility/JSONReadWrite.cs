using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using jQueryAjaxInAsp.NETMVC.Models;
using Newtonsoft.Json.Linq;


namespace jQueryAjaxInAsp.NETMVC.Utility
{
    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        private int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }
        public List<ConsumableOutModel> getListComsumableOut()
        {

            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableOutput.json"));
            string jsonResult = Read(path);
            List<ConsumableOutModel> list = JsonConvert.DeserializeObject<List<ConsumableOutModel>>(jsonResult);
            return list;

        }

        public bool addNewConsumable(ConsumableMaterialModel prod)
        {
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
            string jsonResult = Read(path);
            List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
            list.Add(prod);
            string jSONString = JsonConvert.SerializeObject(list, Formatting.Indented);
            Write(path, jSONString);
            return true;
        }

        public bool editConsumable(ConsumableMaterialModel item)
        {
            try
            {
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
                string jsonResult = Read(path);
                List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
                int index = list.FindIndex(x => x.materialCode.Equals(item.materialCode));
                if (index == -1) { return false; }
                list[index] = item;
                string jSONString = JsonConvert.SerializeObject(list, Formatting.Indented);
                Write(path, jSONString);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public bool checkCodeExistOrNot(String id)
        {
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
            string jsonResult = Read(path);
            List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
            bool has = list.Any(x => x.materialCode.Equals(id));
            return has;
        }

        public List<ConsumableMaterialModel> returnListConsumable()
        {

            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
            string jsonResult = Read(path);
            List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
            list.Where(w => String.IsNullOrEmpty(w.materialImagePath)).ToList().ForEach(s => s.materialImagePath = "~/AppFiles/Images/default.png");
            list.ForEach(x => x.needToBuyMore = (ConvertStringToInt(x.materialRemainQuantity) < ConvertStringToInt(x.materialSafety)));
            return list;

        }

        public ConsumableMaterialModel getConsumableById(string id)
        {
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {

                jsonResult = streamReader.ReadToEnd();
            }
            List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
            list.Where(w => String.IsNullOrEmpty(w.materialImagePath)).ToList().ForEach(s => s.materialImagePath = "~/AppFiles/Images/default.png");

            int index = list.FindIndex(x => x.materialCode.Equals(id));
            if (index == -1) { return new ConsumableMaterialModel(); }
            return list[index];
        }
        public bool deteleMaterialById(string id)
        {
            try
            {
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/ComsumableItem.json"));
                string jsonResult = Read(path);
                List<ConsumableMaterialModel> list = JsonConvert.DeserializeObject<List<ConsumableMaterialModel>>(jsonResult);
                int index = list.FindIndex(x => x.materialCode.Equals(id));
                if (index == -1) { return false; }
                list[index].isDeleted = true;
                string jSONString = JsonConvert.SerializeObject(list, Formatting.Indented);
                Write(path, jSONString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsUserInRole(string loginName)
        {
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/Account.json"));
            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {

                jsonResult = streamReader.ReadToEnd();
            }
            List<UserLogin> user = JsonConvert.DeserializeObject<List<UserLogin>>(jsonResult);
            int index = user.FindIndex(x => x.UserName.Equals(loginName));
            if (index == -1) { return false; }
            return user[index].Role == 1 ? true : false;
        }

        public string GetUserPassword(string loginName)
        {
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/AppFiles/Database/Account.json"));
            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            List<UserLogin> user = JsonConvert.DeserializeObject<List<UserLogin>>(jsonResult);
            int index = user.FindIndex(x => x.UserName.Equals(loginName));
            if (index == -1) { return ""; }
            return user[index].PassWord;
        }
        public string Read(string path)
        {
            //"~/AppFiles/Database/consumableoutput.json"
            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string path, string jSONString)
        {
            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}