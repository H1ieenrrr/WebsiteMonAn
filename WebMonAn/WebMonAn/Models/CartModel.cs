using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonAn.Models
{
    public enum TrangThaiDonHang
    {
        [Display(Name = "Mới Đặt")]
        Moidat = 1,
        [Display(Name = "Đang Giao")]
        Danggiao = 2,
        [Display(Name = "Đã giao")]
        Dagiao = 3
    }
    [Table("Carts")]
    public class CartModel
    {

        [Key]
        public int CartID { get; set; }

        [ForeignKey("UserModel")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày"), Display(Name = "Ngày Đặt")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayDat { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập giá")]
        [Display(Name = "Tổng Tiền")]
        public double Tongtien { get; set; }

        [Display(Name = "Trạng Thái")]
        public TrangThaiDonHang TrangthaiDonhang { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi Chú")]
        public string GhiChu { get; set; }

        public UserModel UserModel { get; set; }
        public List<CartDetailsModel> cartDetailsModels { get; set; }
    }
}
