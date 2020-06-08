using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using jQueryAjaxInAsp.NETMVC.Attribute;
using jQueryAjaxInAsp.NETMVC.Utility;
using Newtonsoft.Json;

namespace jQueryAjaxInAsp.NETMVC.Models
{
    public class ConsumableMaterialModel
    {
      
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "耗材料號")]
        [Display(Name = "Mã SP")]
        public string materialCode { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "耗材名稱")]
        [Display(Name = "Tên SP")]
        public string materialName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Tên ECS")]
        public string materialECSName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Mã ECS")]
        public string materialECSCode { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "型號")]
        [Display(Name = "Chi tiết")]
        public string materialSpecification { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "單位")]
        [Display(Name = "Đơn Vị")]
        public string materialUnit { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "存放位置")]
        [Display(Name = "Vị trí")]
        public string materialStorageLocation { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "適用機種")]
        [Display(Name = "Loại")]
        public string materialKind { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        // [Display(Name = "廠家")]
        [Display(Name = "Nhà cung ứng")]
        public string materialSupplier { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "參考價值")]
        [Display(Name = "Giá")]
        public string materialReferenceValue { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "購買")]
        [Display(Name = "Thời gian")]
        public string materialTimeCanGet { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "庫存")]
        [Display(Name = "Số lượng")]
        public string materialQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "安全")]
        [Display(Name = "Mức an toàn")]
        public string materialSafety { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "領用數量")]
        [Display(Name = "Lấy ra")]
        public string materialNumberRecipient { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "退庫良品數量")]
        [Display(Name = "Số lượng trả lại Good")]
        public string materialGoodNumberReturn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "This field is required.")]
        //[Display(Name = "退庫不良數量")]
        [Display(Name = "Số lượng trả lại Fail")]
        public string materialBadNumberReturn { get; set; }

        //[Display(Name = "庫存")]
        [Display(Name = "Số lượng còn lại")]
        public string materialRemainQuantity { get; set; }

        //[Display(Name = "備註")]
        [Display(Name = "Ghi chú")]
        [StringLength(30, ErrorMessage = "Max 20 digits")]
        public string materialRemark { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Material Photo")]
        public string materialImagePath { get; set; }

        public bool isDeleted { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "This field is required.")]
        public HttpPostedFileBase ImageUpload { get; set; }

        public bool needToBuyMore { get; set; }
        public ConsumableMaterialModel()
        {
            materialImagePath = "~/AppFiles/Images/default.png";
        }
    }
}